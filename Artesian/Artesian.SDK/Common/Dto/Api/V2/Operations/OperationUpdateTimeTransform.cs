using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace Artesian.SDK.Common.Dto.Api.V2.Operations
{
    [MessagePackObject]
    public class OperationUpdateTimeTransform : IOperationParamsPayload
    {
        [Required]
        [MessagePack.Key(0)]
        public int? Value { get; set; }
    }
}
