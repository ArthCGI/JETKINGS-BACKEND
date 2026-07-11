using JetKings.Models.DTOs;

namespace JetKings.IService;

public interface IDashboardService
{
    Task<DashboardResponseDto> GetDashboardDataAsync();
}
