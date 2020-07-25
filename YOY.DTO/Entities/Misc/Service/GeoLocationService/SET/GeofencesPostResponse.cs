using YOY.DTO.Entities.Misc.Service.GeoLocationService.POCO;
using System.Collections.Generic;

namespace YOY.DTO.Entities.Misc.Service.GeoLocationService.SET
{
    public class GeofencesPostResponse
    {
        public List<GeofencePostResponse> FencesUpdated { set; get; }
        public string Message { set; get; }
        public int MessageCode { set; get; }
    }
}
