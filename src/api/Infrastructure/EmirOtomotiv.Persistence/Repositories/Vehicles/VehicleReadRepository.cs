using EmirOtomotiv.Core.Application.Repositories.Vehicles;

namespace EmirOtomotiv.Persistence.Repositories.Vehicles;

public class VehicleReadRepository : ReadRepository<Vehicle>, IVehicleReadRepository
{
    public VehicleReadRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}