using Application.GestaoProdutos.Services.v1.Interfaces;
using Domain.GestaoProdutos.Dtos.UserDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.GestaoProdutos.Services.v1;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly IConfiguration _configuration;        

    public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;

        _configuration = configuration;            
    }

    public async Task<UserTokenDto> RegisterAsync(UserAuthDto userInfo)
    {
        var result = new UserTokenDto();

        var user = new IdentityUser
        {
            UserName = userInfo.Email,
            Email = userInfo.Email,
            EmailConfirmed = true
        };

        var hasUserWithEmail = await _userManager.FindByEmailAsync(userInfo.Email) != null;
        if (hasUserWithEmail)
        {
            result.Message = "Já existe um usuário com esse email cadastrado";
        }

        var createUser = await _userManager.CreateAsync(user, userInfo.Password);

        if (!createUser.Succeeded)
        {
            result.Message = $"{createUser.Errors.FirstOrDefault().Description}, {createUser.Errors.LastOrDefault().Description}";
            return result;
        }

        await _signInManager.SignInAsync(user, false);

        return await GenerateToken(userInfo);
    }
    public async Task<UserTokenDto> AuthenticationAsync(UserAuthDto userInfo)
    {
        var userToken = new UserTokenDto();
        //verifica as credenciais do usuário e retorna um valor
        var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
            userInfo.Password, isPersistent: false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            userToken.Message = "invalid auth....";

            return userToken;
        }

        return await GenerateToken(userInfo);
    }

    private async Task<UserTokenDto> GenerateToken(UserAuthDto userInfo)
    {

        //define declarações do usuário
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim("company", "AutoGlass"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


        //Gerenado algoritimo com base no Key fornecido no appsettrings.json
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

        //Usando o algoritmo Hmac e a chave privada para gera a assinatura digital do token.
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //definir tempo de validção do token
        var expirationTime = _configuration["ConfigurationToken:ExpireHours"];
        var expiration = DateTime.UtcNow.AddHours(double.Parse(expirationTime));

        // classe token JWT
        JwtSecurityToken token = new JwtSecurityToken(
          issuer: _configuration["ConfigurationToken:Issuer"],
          audience: _configuration["ConfigurationToken:Audience"],
          claims: claims,
          expires: expiration,
          signingCredentials: credentials);

        //retorna os dados do token
        return new UserTokenDto()
        {
            Authenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Message = "Successfully generated token"
        };
    }

}
