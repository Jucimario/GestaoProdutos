using Domain.GestaoProdutos.Dtos.UserDtos;

namespace Application.GestaoProdutos.Services.v1.Interfaces;

public interface IAuthService 
{

    public Task<UserTokenDto> RegisterAsync(UserAuthDto userInfo);

    public Task<UserTokenDto> AuthenticationAsync(UserAuthDto userInfo);
}
