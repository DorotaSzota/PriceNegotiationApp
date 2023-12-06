namespace PriceNegotiationApp.Models;

public class PriceProposalDto
{
    public int ProductId { get; set; }
    public decimal ProposedPrice1 { get; set; }
    public decimal ProposedPrice2 { get; set; }
    public decimal ProposedPrice3 { get; set; }
    public bool Accepted { get; set; }
    public int AttemptsLeft { get; set; } = 3;
    public string Message { get; set; } = string.Empty;

    public List<GetProductDto> Products { get; set; } 

}