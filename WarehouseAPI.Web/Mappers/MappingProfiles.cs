using AutoMapper;
using WarehouseAPI.ApplicationCore.Entities;
using WarehouseAPI.Web.Dtos;

namespace WarehouseAPI.Web.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<Sale, SaleDto>();
            CreateMap<SaleDto, Sale>();
        }
    }
}
