using System.Security.Claims;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface ITokenSesion
{
    string CrearToken(Usuario userInfo, UsuariosPerfile usuariosPerfile);
    IEnumerable<Claim> ObtenerClaimsDeToken(string token);
    string ObtenerToken();
}
