namespace PriceNegotiationApp.Models;

public class UpdateProposalStatusDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal ProductPrice { get; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProposedPrice { get;  }
    public bool Accepted { get; set; } = false;
    public int AttemptsLeft { get; }
    public string Message { get; set; } = string.Empty;
}

