using EmirOtomotiv.Core.Application.Repositories.About;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmirOtomotiv.Core.Application.Features.About.Commands.Update;

public class UpdateAboutHandler : IRequestHandler<UpdateAboutRequest>
{
    private readonly IAboutUsReadRepository _readRepository;
    private readonly IAboutUsWriteRepository _writeRepository;

    public UpdateAboutHandler(IAboutUsReadRepository readRepository, IAboutUsWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(UpdateAboutRequest request, CancellationToken cancellationToken)
    {
        var about = await _readRepository.GetAll().FirstOrDefaultAsync();

        if (about is null)
        {
            about = new AboutUs { Id = Guid.NewGuid(), CreatedAt = DateTime.UtcNow, Description = string.Empty, ImageUrl = string.Empty };
            await _writeRepository.AddAsync(about);
        }

        about.Description = request.Description;
        about.ImageUrl = request.ImageUrl;
        await _writeRepository.SaveChangesAsync();
    }
}
