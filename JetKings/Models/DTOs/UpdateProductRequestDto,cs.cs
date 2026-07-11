namespace JetKings.Models.DTOs;

public class UpdateProductRequestDto
{
    public int CategoryId { get; set; }
    public string ModelName { get; set; } = null!;
    public decimal DefaultPrice { get; set; }
}
