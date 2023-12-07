namespace PriceNegotiationApp.Models;

public class PriceProposalDto
{
    public int ProductId { get; set; }
    public decimal ProposedPrice { get; set; }
    public bool Accepted { get; set; }
    public int AttemptsLeft { get; set; }
    public string Message { get; set; }


}