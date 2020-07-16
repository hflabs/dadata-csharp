using System;
namespace Dadata.Model
{
    public class GeolocateRequest : LocationGeo, IDadataRequest
    {
        public int count { get; set; }
        public string language { get; set; }

        public GeolocateRequest(double lat, double lon, int radius_meters = 100, int count = 5)
        {
            this.lat = lat;
            this.lon = lon;
            this.radius_meters = radius_meters;
            this.count = count;
        }
    }
}
