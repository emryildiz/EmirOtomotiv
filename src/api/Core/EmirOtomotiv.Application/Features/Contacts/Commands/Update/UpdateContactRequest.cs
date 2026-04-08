using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Contacts.Commands.Update;

public class UpdateContactRequest : IRequest
{
    public string Description { get; set; }
    public string Adress { get; set; }
    public string PhoneNumber { get; set; }
    public string WorkingHours { get; set; }
    public string Mail { get; set; }
}
