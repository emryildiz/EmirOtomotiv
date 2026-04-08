using EmirOtomotiv.Core.Application.Features.Contacts.Commands.Update;
using EmirOtomotiv.Core.Application.Features.Contacts.Queries.GetContact;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmirOtomotiv.Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _mediator.Send(new GetContactRequest()));

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContactRequest request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}
