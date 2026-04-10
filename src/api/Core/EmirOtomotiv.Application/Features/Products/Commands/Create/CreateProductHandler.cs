using Core.EmirOtomotiv.Application.Repositories.Categories;
using EmirOtomotiv.Core.Application.Common.Helpers;
using EmirOtomotiv.Core.Application.Repositories.Products;
using EmirOtomotiv.Core.Application.Repositories.Vehicles;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Products.Commands.Create;

public class CreateProductHandler : IRequestHandler<CreateProductRequest>
{
    private readonly IProductWriteRepository _writeRepository;
    private readonly IVehicleReadRepository _vehicleReadRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;

    public CreateProductHandler(
        IProductWriteRepository writeRepository,
        IVehicleReadRepository vehicleReadRepository,
        ICategoryReadRepository categoryReadRepository)
    {
        _writeRepository = writeRepository;
        _vehicleReadRepository = vehicleReadRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async Task Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var vehicle = await _vehicleReadRepository.GetByIdAsync(request.VehicleId);
        var category = await _categoryReadRepository.GetByIdAsync(request.CategoryId);

        var product = new Product
        {
            Name = request.Name,
            Slug = SlugHelper.Generate(request.Name),
            Description = request.Description,
            Vehicle = vehicle,
            Category = category,
        };

        await _writeRepository.AddAsync(product);
        await _writeRepository.SaveChangesAsync();
    }
}