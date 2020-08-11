using System;
using Dadata.Model;

namespace Dadata
{
    public interface IProfileClientSync
    {
        GetBalanceResponse GetBalance();
        GetDailyStatsResponse GetDailyStats();
        GetDailyStatsResponse GetDailyStats(DateTime date);
        GetVersionsResponse GetVersions();
    }
}
