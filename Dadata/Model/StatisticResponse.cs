using System;

namespace Dadata.Model
{
    public class StatisticResponse : IDadataResponse {
        public DateTime date { get; set; }
        public ServicesStatistic services { get; set; }
    }
}
