using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace Artesian.SDK.Common.Dto.Api.V2.Operations
{
    [MessagePackObject]
    public class OperationUpdateOriginalTimeZone : IOperationParamsPayload
    {
        [Required]
        [MessagePack.Key(0)]
        public string Value { get; set; }
    }
}
