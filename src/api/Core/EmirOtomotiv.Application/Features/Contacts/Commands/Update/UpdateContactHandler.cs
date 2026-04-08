using EmirOtomotiv.Core.Application.Repositories.Contacts;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Core.Application.Features.Contacts.Commands.Update;

public class UpdateContactHandler : IRequestHandler<UpdateContactRequest>
{
    private readonly IContactReadRepository _readRepository;
    private readonly IContactWriteRepository _writeRepository;

    public UpdateContactHandler(IContactReadRepository readRepository, IContactWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdateContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await _readRepository.GetAll().FirstOrDefaultAsync();

        if (contact is null)
        {
            contact = new Contact { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, PhoneNumber = string.Empty, WorkingHours = string.Empty, Mail = string.Empty };
            await _writeRepository.AddAsync(contact);
        }

        contact.Description = request.Description;
        contact.Adress = request.Adress;
        contact.PhoneNumber = request.PhoneNumber;
        contact.WorkingHours = request.WorkingHours;
        contact.Mail = request.Mail;
        await _writeRepository.SaveChangesAsync();
    }
}
