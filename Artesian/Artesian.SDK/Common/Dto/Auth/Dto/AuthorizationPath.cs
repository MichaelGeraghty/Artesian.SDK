using MessagePack;
using System.Collections.Generic;

namespace Artesian.SDK.API.Dto.Auth.Dto
{
    public static class AuthorizationPath
    {
        [MessagePackObject]
        public class Input
        {
            [Key(0)]
            public string Path { get; set; }
            [Key(1)]
            public IEnumerable<AuthorizationPrincipalRole> Roles { get; set; }
        }

        [MessagePackObject]
        public class Output : Input
        {
        }
    }
}
