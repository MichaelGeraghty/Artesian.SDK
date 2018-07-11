using System.ComponentModel.DataAnnotations;
using EnsureThat;
using MessagePack;

namespace Artesian.SDK.Dto.Api.V2
{
    /// <summary>
    /// The MarketData id with Etag
    /// </summary>
    [MessagePackObject]
    public class MarketDataETag
    {
        public MarketDataETag(int id, string eTag)
        {
            //  Ensure.Bool.IsTrue(id >= ArtesianConstants.CurveIDMin, "id out of accepted Range");
            //  Ensure.Bool.IsTrue(id <= ArtesianConstants.CurveIDMax, "id out of accepted Range");
            EnsureArg.IsNotNull(eTag);

            ID = id;
            ETag = eTag;
        }

        /// <summary>
        /// The Market Data Identifier
        /// </summary>
        [Required]
        [MessagePack.Key(0)]
        public int ID { get; protected set; }

        /// <summary>
        /// The Market Data ETag
        /// </summary>
        [Required]
        [MessagePack.Key(1)]
        public string ETag { get; protected set; }


        public override bool Equals(object obj)
        {
            var item = obj as MarketDataETag;
            if (item == null)
            {
                return false;
            }

            return this.ID.Equals(item.ID)
                && this.ETag.Equals(item.ETag);
        }
        public override int GetHashCode()
        {
            return ID.GetHashCode() ^ ETag.GetHashCode();
        }


        public static bool operator ==(MarketDataETag x, MarketDataETag y)
        {
            if (!Equals(x, null))
                return x.Equals(y);
            else if (Equals(y, null))
                return true;
            else
                return false;

        }

        public static bool operator !=(MarketDataETag x, MarketDataETag y)
        {
            if (!Equals(x, null))
                return !x.Equals(y);
            else if (Equals(y, null))
                return false;
            else
                return true;
        }
    }
}
