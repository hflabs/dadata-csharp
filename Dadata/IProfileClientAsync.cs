using System;
using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public interface IProfileClientAsync
    {
        Task<GetBalanceResponse> GetBalance();
        Task<GetDailyStatsResponse> GetDailyStats();
        Task<GetDailyStatsResponse> GetDailyStats(DateTime date);
        Task<GetVersionsResponse> GetVersions();
    }
}
