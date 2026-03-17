using AutoMapper;
using EmirOtomotiv.Core.Application.Features.Auth.Queries.GetMe;
using EmirOtomotiv.Core.Application.Features.Categories.Commands.Create;
using EmirOtomotiv.Core.Application.Features.Products.Commands.Create;
using EmirOtomotiv.Core.Application.Features.Products.Queries.Get;
using EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;
using EmirOtomotiv.Core.Application.Features.Vehicles.Commands.Create;
using EmirOtomotiv.Core.Domain.DTOs;
using EmirOtomotiv.Core.Domain.Entities;

namespace EmirOtomotiv.Core.Application.Common.Mappings;
public class Mapping : Profile
{
    public Mapping()
    {
        //Products
        this.CreateMap<Product, GetProductResponse>();
        this.CreateMap<Product, GetProductByIdResponse>();
        this.CreateMap<CreateProductRequest, Product>();

        this.CreateMap<Vehicle, VehicleDto>();
        this.CreateMap<CreateVehicleRequest, Vehicle>();

        this.CreateMap<Category, CategoryDto>();
        this.CreateMap<CreateCategoryRequest, Category>();

        this.CreateMap<User, GetMeResponse>();
    }
}