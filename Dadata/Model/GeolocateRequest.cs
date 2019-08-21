using System;
namespace Dadata.Model
{
    public class GeolocateRequest : IDadataRequest
    {
        public double lat { get; set; }
        public double lon { get; set; }

        public GeolocateRequest(double lat, double lon)
        {
            this.lat = lat;
            this.lon = lon;
        }
    }
}
