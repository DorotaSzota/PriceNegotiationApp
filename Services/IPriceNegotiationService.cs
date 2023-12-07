﻿using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public interface IPriceNegotiationService
{
    List<GetProductDto> GetAllProducts();
    PriceProposalDto AddPriceProposal(PriceProposalDto priceProposal);
    List<GetPriceProposalDto> GetAllPriceProposals();
    GetPriceProposalDto GetPriceProposalById(int id);
    void UpdateProposalStatus(UpdateProposalStatusDto dto);

}