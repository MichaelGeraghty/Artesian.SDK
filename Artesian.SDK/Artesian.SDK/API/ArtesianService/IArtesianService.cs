using System.Threading.Tasks;
using Artesian.SDK.API.ArtesianService.Data;
using Artesian.SDK.API.ArtesianService.MetaData;
using Artesian.SDK.API.Dto;
using Artesian.SDK.Common.Dto.Api.V2;

namespace Artesian.SDK.API
{
    public static class IArtesianService
    {
        public interface Latest : V2_1 { }
        public interface Deprecated : V2_0 { }


        public interface V2_0 : IArtesianMetaDataService.V2_0, IArtesianDataService.V2_0 { }
        public interface V2_1 : IArtesianMetaDataService.V2_1, IArtesianDataService.V2_1 { }
    }
}
