namespace JetKings.Models.DTOs;

public class UpdateBuyerProductPriceRequestDto
{
    public int BuyerId { get; set; }
    public int ProductId { get; set; }
    public decimal Rate { get; set; }
}
