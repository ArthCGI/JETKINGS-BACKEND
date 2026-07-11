using JetKings.Entity;
using JetKings.IService;
using JetKings.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JetKings.Services;

public class DashboardService : IDashboardService
{
    private readonly JetKingsDbContext _dbContext;
    private readonly ILogger<DashboardService> _logger;

    public DashboardService(
        JetKingsDbContext dbContext,
        ILogger<DashboardService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<ApiResponseDto<DashboardSummaryDto>> GetDashboardSummaryAsync(
        CancellationToken ct = default)
    {
        try
        {
            var totalBuyers = await _dbContext.Buyers.CountAsync(ct);
            var totalProducts = await _dbContext.Products.CountAsync(ct);
            var billsGenerated = await _dbContext.Invoices.CountAsync(ct);

            var today = DateOnly.FromDateTime(DateTime.Today);
            var todayRevenue = await _dbContext.Invoices
                .Where(i => i.InvoiceDate == today)
                .SumAsync(i => (decimal?)i.TotalAmount, ct) ?? 0;

            var summary = new DashboardSummaryDto
            {
                TotalBuyers = totalBuyers,
                TotalProducts = totalProducts,
                BillsGenerated = billsGenerated,
                TodayRevenue = todayRevenue
            };

            _logger.LogInformation("Dashboard summary retrieved successfully");

            return ApiResponseDto<DashboardSummaryDto>.Ok(
                summary,
                "Dashboard summary retrieved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving dashboard summary");
            return ApiResponseDto<DashboardSummaryDto>.Fail(
                "Failed to retrieve dashboard summary.");
        }
    }

    public async Task<ApiResponseDto<List<RecentActivityDto>>> GetRecentActivitiesAsync(
        CancellationToken ct = default)
    {
        try
        {
            var recentActivities = await _dbContext.Invoices
                .Include(i => i.Buyer)
                .OrderByDescending(i => i.InvoiceDate)
                .ThenByDescending(i => i.CreatedAt)
                .Take(10)
                .Select(i => new RecentActivityDto
                {
                    InvoiceId = i.Id,
                    InvoiceNo = i.InvoiceNo,
                    BuyerName = i.Buyer.PartyName,
                    InvoiceDate = i.InvoiceDate,
                    Amount = i.TotalAmount,
                    Status = i.Status
                })
                .ToListAsync(ct);

            _logger.LogInformation("Retrieved {Count} recent activities", recentActivities.Count);

            return ApiResponseDto<List<RecentActivityDto>>.Ok(
                recentActivities,
                "Recent activities retrieved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving recent activities");
            return ApiResponseDto<List<RecentActivityDto>>.Fail(
                "Failed to retrieve recent activities.");
        }
    }
}
