namespace JetKings.Models.DTOs;

public class DashboardSummaryDto
{
    public int TotalBuyers { get; set; }
    public int TotalProducts { get; set; }
    public int BillsGenerated { get; set; }
    public decimal TodayRevenue { get; set; }
}
