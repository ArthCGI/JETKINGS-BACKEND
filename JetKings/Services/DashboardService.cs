using JetKings.Entity;
using JetKings.IService;
using JetKings.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace JetKings.Services;

public class DashboardService : IDashboardService
{
    private readonly JetKingsDbContext _context;

    public DashboardService(JetKingsDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardResponseDto> GetDashboardDataAsync()
    {
        return new DashboardResponseDto
        {
            TotalBuyers = await _context.Buyers.CountAsync(),

            GstActive = await _context.Buyers
                .CountAsync(x => !string.IsNullOrEmpty(x.Gstin)),

            RecentOrders = await _context.Invoices.CountAsync(),

            PaymentDue = await _context.Invoices
                .CountAsync(x => x.Status == "PENDING")
        };
    }
}
