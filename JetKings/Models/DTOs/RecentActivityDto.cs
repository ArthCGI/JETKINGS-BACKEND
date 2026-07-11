namespace JetKings.Models.DTOs;

public class RecentActivityDto
{
    public int InvoiceId { get; set; }
    public string InvoiceNo { get; set; } = null!;
    public string BuyerName { get; set; } = null!;
    public DateOnly InvoiceDate { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
}
