using System;

namespace Dadata.Model
{
    public class GetDailyStatsResponse : IDadataResponse
    {
        public DateTime date { get; set; }
        public ServicesStats services { get; set; }
    }

    public class ServicesStats
    {
        public int merging { get; set; }
        public int suggestions { get; set; }
        public int clean { get; set; }
    }
}
