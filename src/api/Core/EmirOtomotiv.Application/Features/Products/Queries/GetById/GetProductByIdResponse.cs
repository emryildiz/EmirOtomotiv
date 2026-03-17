using EmirOtomotiv.Core.Domain.DTOs;

namespace EmirOtomotiv.Core.Application.Features.Products.Queries.GetById;

public class GetProductByIdResponse
{
    public string Id { get; set; }

    public string Name {get; set;}

    public string Description { get; set; }

    public string ProductNumber { get; set; }

    public VehicleDto Vehicle { get; set; }

    public CategoryDto Category { get; set; }
}