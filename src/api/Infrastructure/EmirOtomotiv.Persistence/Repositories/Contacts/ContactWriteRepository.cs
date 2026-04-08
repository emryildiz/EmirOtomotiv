using EmirOtomotiv.Core.Application.Repositories.Contacts;

namespace EmirOtomotiv.Persistence.Repositories.Contacts;

public class ContactWriteRepository : WriteRepository<Contact>, IContactWriteRepository
{
    public ContactWriteRepository(EmirOtomotivDbContext context) : base(context)
    {
    }
}