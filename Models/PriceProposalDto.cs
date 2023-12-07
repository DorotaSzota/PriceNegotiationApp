namespace PriceNegotiationApp.Models;

public class PriceProposalDto
{
    public int ProductId { get; set; }
    public decimal ProposedPrice { get; set; }
    public bool Accepted { get; }
    public int AttemptsLeft { get; }
    public string Message { get;  }


}