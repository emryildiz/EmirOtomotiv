using EmirOtomotiv.Core.Application.Repositories.Vehicles;

namespace EmirOtomotiv.Persistence.Repositories.Vehicles;

public class VehicleWriteRepository : WriteRepository<Vehicle>, IVehicleWriteRepository
{
    public VehicleWriteRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}