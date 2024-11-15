using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PIM_Fazenda_Urbana.Models;
using System.Security.Claims;

namespace PIM_Fazenda_Urbana.Services
{
    public static class AutenticacaoService
    {

        public static async Task Login(HttpContext context , Usuario usuario, int idPessoal)
        {
            var claims = new List<Claim>
            {
                new Claim("IdPessoal", idPessoal.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Funcao)
            };

            var claimsIdentity = new ClaimsPrincipal(
                new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                    )
                );

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                IssuedUtc = DateTime.UtcNow
            };

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsIdentity, authProperties );
        }

        public static async Task Logout(HttpContext context)
        {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
