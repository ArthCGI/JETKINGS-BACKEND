using AutoMapper;
using JetKings.Entity;
using JetKings.Models.DTOs;

namespace JetKings.Services;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<CreateProductRequestDto, Product>();
        CreateMap<UpdateProductRequestDto, Product>();
        CreateMap<Product, ProductResponseDto>();
    }
}