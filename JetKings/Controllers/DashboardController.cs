using JetKings.IService;
using Microsoft.AspNetCore.Mvc;

namespace JetKings.Controllers;

public class DashboardController : BaseApiController
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetDashboardSummary(CancellationToken ct = default)
    {
        var result = await _dashboardService.GetDashboardSummaryAsync(ct);
        return OkResponse(result);
    }

    [HttpGet("recent-activities")]
    public async Task<IActionResult> GetRecentActivities(CancellationToken ct = default)
    {
        var result = await _dashboardService.GetRecentActivitiesAsync(ct);
        return OkResponse(result);
    }
}
