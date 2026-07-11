using AutoMapper;
using JetKings.Entity;
using JetKings.Models.DTOs;

namespace JetKings.Services;

public class BuyerProductPriceMappingProfile : Profile
{
    public BuyerProductPriceMappingProfile()
    {
        CreateMap<CreateBuyerProductPriceRequestDto, BuyerProductPrice>();
        CreateMap<UpdateBuyerProductPriceRequestDto, BuyerProductPrice>();
        CreateMap<BuyerProductPrice, BuyerProductPriceResponseDto>();
    }
}