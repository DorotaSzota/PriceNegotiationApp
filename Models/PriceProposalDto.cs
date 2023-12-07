namespace PriceNegotiationApp.Models;

public class PriceProposalDto
{
    public int ProductId { get; set; }
    public decimal ProposedPrice { get; set; }
    public bool Accepted { get; set; } = false;
    public int AttemptsLeft { get; set; }
    public int UserId { get; set; }



}