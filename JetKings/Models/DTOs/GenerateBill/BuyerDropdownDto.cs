namespace JetKings.Models.DTOs.GenerateBill;

public class BuyerDropdownDto
{
    public int Id { get; set; }
    public string PartyName { get; set; } = string.Empty;
    public string? Gstin { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
}
