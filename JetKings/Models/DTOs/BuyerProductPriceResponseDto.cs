namespace JetKings.Models.DTOs;

public class BuyerProductPriceResponseDto
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public int ProductId { get; set; }
    public decimal Rate { get; set; }
}