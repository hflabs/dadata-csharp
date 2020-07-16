using System;
namespace Dadata.Model
{
    public class GeolocateRequest : LocationGeo, IDadataRequest
    {
        public int count { get; set; }
        public string language { get; set; }

        public GeolocateRequest(double lat, double lon, int count = 5)
        {
            this.lat = lat;
            this.lon = lon;
            this.count = count;
        }
    }
}
