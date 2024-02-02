namespace PriceNegotiationApp.Models;

public class PriceProposal
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal? ProductPrice { get; set; }
    public bool ProductPriceVisible { get; set; }
    public string? ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public decimal ProposedPrice { get; set; }
    public bool Accepted { get; set; } = false;
    public int AttemptsLeft { get; set; }
    public string Message { get; set; } = string.Empty;
    public int UserId { get; set; }
    public virtual User? User { get; set; }


}