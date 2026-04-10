using EmirOtomotiv.Core.Application.Common.Interfaces;
using EmirOtomotiv.Core.Application.Exceptions;
using EmirOtomotiv.Core.Application.Repositories.Users;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public ChangePasswordHandler(IPasswordHasher passwordHasher, IUserReadRepository readRepository, IUserWriteRepository writeRepository)
    {
        _passwordHasher = passwordHasher;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out Guid userId))
            throw new NotFoundUserException();

        var user = await _readRepository.GetByIdAsync(userId.ToString())
            ?? throw new NotFoundUserException();

        if (!_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash))
            throw new AuthenticationFailedException();

        user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
        user.MustChangePassword = false;

        await _writeRepository.SaveChangesAsync();
    }
}
