using EmirOtomotiv.Core.Application.Repositories.Visits;
using EmirOtomotiv.Core.Domain.Entities;
using EmirOtomotiv.Persistence.Contexts;

namespace EmirOtomotiv.Persistence.Repositories.Visits;

public class VisitWriteRepository : WriteRepository<Visit>, IVisitWriteRepository
{
    public VisitWriteRepository(EmirOtomotivDbContext context) : base(context) { }
}
