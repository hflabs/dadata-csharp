using System;
using System.Threading;
using System.Threading.Tasks;
using Dadata.Model;

namespace Dadata
{
    public interface IProfileClientAsync
    {
        Task<GetBalanceResponse> GetBalance(CancellationToken cancellationToken = default);
        Task<GetDailyStatsResponse> GetDailyStats(CancellationToken cancellationToken = default);
        Task<GetDailyStatsResponse> GetDailyStats(DateTime date, CancellationToken cancellationToken = default);
        Task<GetVersionsResponse> GetVersions(CancellationToken cancellationToken = default);
    }
}
