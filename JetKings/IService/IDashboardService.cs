using JetKings.Models.DTOs;

namespace JetKings.IService;

public interface IDashboardService
{
    Task<ApiResponseDto<DashboardSummaryDto>> GetDashboardSummaryAsync(CancellationToken ct = default);
    Task<ApiResponseDto<List<RecentActivityDto>>> GetRecentActivitiesAsync(CancellationToken ct = default);
}
