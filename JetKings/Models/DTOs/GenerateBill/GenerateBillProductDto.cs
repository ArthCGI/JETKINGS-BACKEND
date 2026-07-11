namespace JetKings.Models.DTOs.GenerateBill;

public class GenerateBillProductDto
{
    public int Id { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Buyer-specific rate if set, otherwise the product's default price.
    /// </summary>
    public decimal EffectivePrice { get; set; }

    /// <summary>
    /// Product's default price — used to show strikethrough when buyer has a special rate.
    /// </summary>
    public decimal DefaultPrice { get; set; }

    /// <summary>
    /// True when a buyer-specific rate exists and is different from the default price.
    /// </summary>
    public bool HasSpecialPrice { get; set; }
}
