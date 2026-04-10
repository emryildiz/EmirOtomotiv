using AutoMapper;
using EmirOtomotiv.Core.Application.Common.Interfaces;
using EmirOtomotiv.Core.Application.Exceptions;
using EmirOtomotiv.Core.Application.Repositories.Users;
using EmirOtomotiv.Core.Domain.Entities;
using MediatR;

namespace EmirOtomotiv.Core.Application.Features.Auth.Commands.Login;

public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginHandler(IPasswordHasher passwordHasher, IUserReadRepository readRepository, IJwtTokenGenerator jwtTokenGenerator, IUserWriteRepository writeRepository)
    {
        _passwordHasher = passwordHasher;
        _readRepository = readRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _writeRepository = writeRepository;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        User user = await this._readRepository.GetByUsername(request.Username);

        bool passResult = this._passwordHasher.Verify(request.Password, user.PasswordHash);

        if(passResult == false) throw new NotFoundUserException();

        DateTime refreshTokenExpiry = request.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(7);

        LoginResponse response = new LoginResponse
        {
            Token = this._jwtTokenGenerator.GenerateToken(user),
            RefreshToken = this._jwtTokenGenerator.GenerateRefreshToken(),
            Role = user.Role,
            RefreshTokenExpiry = refreshTokenExpiry,
            MustChangePassword = user.MustChangePassword
        };

        user.RefreshToken = response.RefreshToken;
        user.RefreshTokenExpiryTime = refreshTokenExpiry;

        await this._writeRepository.SaveChangesAsync();

        return response;
    }
}