using AutoMapper;
using JetKings.Entity;
using JetKings.Models.DTOs;

namespace JetKings.Services
{
    public class BuyerMappingProfile : Profile
    {
        public BuyerMappingProfile()
        {
            CreateMap<CreateBuyerRequestDto, Buyer>();
            CreateMap<UpdateBuyerRequestDto, Buyer>();
            CreateMap<Buyer, BuyerResponseDto>();
        }
    }
}
