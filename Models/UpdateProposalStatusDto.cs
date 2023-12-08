namespace PriceNegotiationApp.Models;

public class UpdateProposalStatusDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public bool Accepted { get; set; } = false;
    public string Message { get; set; } = string.Empty;
}

