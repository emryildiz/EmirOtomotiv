using EmirOtomotiv.Core.Application.Repositories.Contacts;

namespace EmirOtomotiv.Persistence.Repositories.Contacts;

public class ContactReadRepository : ReadRepository<Contact>, IContactReadRepository
{
    public ContactReadRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}