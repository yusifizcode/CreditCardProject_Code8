using MediatR;
using Microsoft.Extensions.Configuration;
using TechnestHackhaton.Application.Common.Interfaces;

namespace TechnestHackhaton.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;
        readonly IConfiguration _configuration;

        public LoginUserCommandHandler(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _authService.LoginAsync(request.UsernameOrEmail, request.Password, Convert.ToInt32(_configuration["Token:AccessTokenLifeTime"]));
            return new LoginUserSuccessCommandResponse();
        }
    }
}
