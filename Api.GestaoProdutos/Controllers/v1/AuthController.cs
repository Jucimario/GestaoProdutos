using Application.GestaoProdutos.Services.v1.Interfaces;
using Domain.GestaoProdutos.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.GestaoProdutos.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService ??
                throw new ArgumentNullException(nameof(authService));
    }

    [HttpPost("/Register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserTokenDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody] UserAuthDto userInfo)
    {
        var result = await _authService.RegisterAsync(userInfo);

        if (!result.Authenticated)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost("/Auth")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserTokenDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Authentication([FromBody] UserAuthDto userInfo)
    {
        //verifica as credenciais do usuário e retorna um valor
        var result = await _authService.AuthenticationAsync(userInfo);

        if (!result.Authenticated)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }
}
