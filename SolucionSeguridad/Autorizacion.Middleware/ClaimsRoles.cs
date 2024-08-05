using Autorizacion.Abstracciones.BW;
using Microsoft.AspNetCore.Http;
using Autorizacion.Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Autorizacion.Middleware
{
    public class ClaimsRoles
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private IAutorizacionBW _autorizacionBW;

        public ClaimsRoles(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAutorizacionBW autorizacionBW)
        {
            _autorizacionBW = autorizacionBW;
            ClaimsIdentity appIdentity = await verificarAutorizacion(httpContext);
            httpContext.User.AddIdentity(appIdentity);
            await _next(httpContext);
        }

        private async Task<ClaimsIdentity> verificarAutorizacion(HttpContext httpContext)
        {
            var claims = new List<Claim>();
            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
            {
                await ObtenerUsuario(httpContext, claims);
                await ObtenerRoles(httpContext, claims);
            }
            var appIdentity = new ClaimsIdentity(claims);
            return appIdentity;
        }

        private async Task ObtenerRoles(HttpContext httpContext, List<Claim> claims)
        {
           var roles = await ObtenerInformacionRoles(httpContext);
            if (roles  != null && roles.Any())
            {
                foreach(var rol in roles){
                    claims.Add(new Claim(ClaimTypes.Role, rol.RolID.ToString())); 
                }
            }
        }

        private async Task<IEnumerable<Roles>> ObtenerInformacionRoles(HttpContext httpContext)
        {
            return await _autorizacionBW.ObtenerPerfilesxUsuario(new Abstracciones.Modelos.Usuario { NombreUsuario = httpContext.User.Claims.Where(c => c.Type == "usuario").FirstOrDefault().Value });
        }

        private async Task ObtenerUsuario(HttpContext httpContext, List<Claim> claims)
        {
            var usuario = await ObtenerInformacionUsuario(httpContext);
            if (usuario is not null && !string.IsNullOrEmpty(usuario.UsuarioID.ToString()) && !string.IsNullOrEmpty(usuario.NombreUsuario.ToString()) && !string.IsNullOrEmpty(usuario.Email.ToString()))
            {
                claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
                claims.Add(new Claim(ClaimTypes.Name, usuario.NombreUsuario));
                claims.Add(new Claim("UsuarioID", usuario.UsuarioID.ToString()));
            }
        }

        private async Task<Usuario> ObtenerInformacionUsuario(HttpContext httpContext)
        {
            return await _autorizacionBW.ObtenerUsuario(new Abstracciones.Modelos.Usuario { NombreUsuario = httpContext.User.Claims.Where(c => c.Type == "usuario").FirstOrDefault().Value });
        }
    }
}
