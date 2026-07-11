namespace JetKings.Models.DTOs;

public class ProductResponseDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string ModelName { get; set; } = null!;
    public decimal DefaultPrice { get; set; }
}
