﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Artesian.SDK.Common
{
    public class PathString : IEquatable<PathString>
    {
        private static Regex _regexSplit = new Regex(@"(?<!\\)\/");
        public static int MaxLenghtPaths = 11;
        public static int MaxTokenLen = 50;

        private string[] _tokens { get; set; }

        public static string Star = "*";
        public static string NotSet = "*-";

        public PathString(string[] tokens)
        {
            var t = tokens.Where(w => w != PathString.NotSet).ToArray();

            if (t.Length > PathString.MaxLenghtPaths)
                throw new ArgumentException($@"Max allowed tokens: {PathString.MaxLenghtPaths}");

            if (t.Length == 0)
                throw new ArgumentException($@"At least 1 token is needed");

            _tokens = t;

            var len = _tokens.Length;
            if (IsResource())
                len--;

            for (int i = 0; i < len; i++)
            {
                if (_tokens[i].Length > MaxTokenLen)
                    throw new ArgumentException($@"Tokens have to be less than {MaxTokenLen} chars lenght");
            }

        }

        public static PathString Parse(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                throw new ArgumentException("path should not be null or whitespace");

            if (!path.StartsWith(@"/"))
                throw new ArgumentException(@"path should start with '/' character");

            var tokens = _regexSplit.Split(path.Substring(1))
                //DelimitedString.Split(path.Substring(1))
                .Select(s => s.Replace(@"\/", @"/"))
                .ToArray();

            return new PathString(tokens);
        }

        public static bool TryParse(string path, out PathString retVal)
        {
            try
            {
                retVal = PathString.Parse(path);

                return true;
            }
            catch (Exception)
            {
                retVal = null;

                return false;
            }
        }

        public bool IsRoot()
        {
            return _tokens.Length == 1 && _tokens[0] == "";
        }

        public bool IsResource()
        {
            return _tokens[_tokens.Length - 1] != "";
        }

        public string GetPath()
        {
            if (IsResource())
                return $@"{_toString(_tokens.Take(_tokens.Length - 1).ToArray())}/";

            return ToString();
        }

        private string _toString(string[] tokens)
        {
            return $@"/{String.Join(@"/", tokens.Select(s => s.Replace(@"/", @"\/")))}";
            //   return $@"/{DelimitedString.Join(tokens)}";
        }

        public string GetToken(int i)
        {
            var len = _tokens.Length;
            if (IsResource())
                len--;

            if (i >= 0 && i < len)
            {
                return _tokens[i];
            }
            return PathString.NotSet;
        }

        public string GetResource()
        {
            if (IsResource())
                return _tokens[_tokens.Length - 1];

            return PathString.NotSet;
        }

        public override string ToString()
        {
            return _toString(_tokens);
        }

        public static implicit operator string(PathString url) { return url.ToString(); }

        public static implicit operator PathString(string url) { return PathString.Parse(url); }

        public bool IsAncestorOf(PathString currPath)
        {
            if (this._tokens.Length > currPath._tokens.Length)
                return false;

            for (int i = 0; i < this._tokens.Length; i++)
            {
                if (i == this._tokens.Length - 1 && this._tokens[i] == String.Empty)
                    break;

                if (
                       !(this._tokens[i] == PathString.Star)
                    && !(this._tokens[i] == currPath._tokens[i])
                    && !(this._tokens[i].Length > 1 && this._tokens[i].EndsWith(PathString.Star) && currPath._tokens[i].StartsWith(this._tokens[i].Substring(0, this._tokens[i].Length - 1)))
                    )
                    return false;
            }
            return true;
        }

        public bool Equals(PathString other)
        {
            return Enumerable.SequenceEqual(_tokens, other._tokens);
        }

        public override bool Equals(object obj)
        {
            return obj is PathString p && this.Equals(p);
        }

        public override int GetHashCode()
        {
            var hashCode = 2064342430;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ToString());
            return hashCode;
        }
    }

    public static class Ex
    {
        public static bool IsAncestorOf(this string anc, string path)
        {
            PathString ancestor;
            PathString currPath;

            if (PathString.TryParse(anc, out ancestor) && PathString.TryParse(path, out currPath))
                return ancestor.IsAncestorOf(currPath);

            return false;
        }

        public static bool IsAncestorOf(this string anc, PathString currPath)
        {
            PathString ancestor;

            if (PathString.TryParse(anc, out ancestor))
                return ancestor.IsAncestorOf(currPath);

            return false;
        }

        public static bool IsAncestorOf(this PathString ancestor, string path)
        {
            PathString currPath;

            if (PathString.TryParse(path, out currPath))
                return ancestor.IsAncestorOf(currPath);

            return false;
        }
    }



    /// <summary>
    /// Helpers for delimited string, with support for escaping the delimiter
    /// character.
    /// </summary>
    public static class DelimitedString
    {
        const string DelimiterString = "/";
        const char DelimiterChar = '/';

        // Use a single / as escape char, avoid \ as that would require
        // all escape chars to be escaped in the source code...
        const char EscapeChar = '\\';
        const string EscapeString = "\\";

        /// <summary>
        /// Join strings with a delimiter and escape any occurence of the
        /// delimiter and the escape character in the string.
        /// </summary>
        /// <param name="strings">Strings to join</param>
        /// <returns>Joined string</returns>
        public static string Join(params string[] strings)
        {
            return string.Join(
              DelimiterString,
              strings.Select(
                s => s
                .Replace(EscapeString, EscapeString + EscapeString)
                .Replace(DelimiterString, EscapeString + DelimiterString)));
        }

        /// <summary>
        /// Split strings delimited strings, respecting if the delimiter
        /// characters is escaped.
        /// </summary>
        /// <param name="source">Joined string from <see cref="Join(string[])"/></param>
        /// <returns>Unescaped, split strings</returns>
        public static string[] Split(string source)
        {
            if (source.Length == 0)
                return new[] { "" };

            var result = new List<string>();

            int segmentStart = 0;
            for (int i = 0; i < source.Length; i++)
            {
                bool readEscapeChar = false;
                if (source[i] == EscapeChar)
                {
                    readEscapeChar = true;
                    i++;
                }

                if (!readEscapeChar && source[i] == DelimiterChar)
                {
                    result.Add(UnEscapeString(
                      source.Substring(segmentStart, i - segmentStart)));
                    segmentStart = i + 1;
                }

                if (i == source.Length - 1)
                {
                    result.Add(UnEscapeString(source.Substring(segmentStart)));
                }
            }

            return result.ToArray();
        }

        static string UnEscapeString(string src)
        {
            return src.Replace(EscapeString + DelimiterString, DelimiterString)
              .Replace(EscapeString + EscapeString, EscapeString);
        }
    }
}
