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

    [HttpGet]
    public async Task<IActionResult> GetDashboard()
    {
        var result = await _dashboardService.GetDashboardDataAsync();
        return Ok(result);
    }
}
