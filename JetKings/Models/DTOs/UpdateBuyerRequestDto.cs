namespace JetKings.Models.DTOs
{

    public class UpdateBuyerRequestDto
    {
        public string PartyName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string Gstin { get; set; } = null!;
        public string BillingAddress { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
    }

}
