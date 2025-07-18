using Shared.DTOs;
using Shared.DTOs.Admin;

namespace ServicesAbstractions.Interfaces
{
    public interface IAdminService
    {
        Task<APIResponse<StatsResponse>> GetStatsAsync();
    }
}
