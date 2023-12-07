namespace PriceNegotiationApp.Models;

public class GetPriceProposalDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProposedPrice { get; set; }
    public bool Accepted { get; set; } = false;
    public int AttemptsLeft { get; set; } = 3;
    public string Message { get; set; } = string.Empty;
}