using Tienda_API.Aplicacion.Atributos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Aplicacion.Configuraciones;

public class ValidarToken : IValidarToken
{
    private readonly IConfiguration _configuration;
    private readonly ITokenSesion _token;

    public ValidarToken(IConfiguration configuration, ITokenSesion token)
    {
        _configuration = configuration;
        _token = token;
    }

    public void Validar(object[] attributes)
    {
        var token = _token.ObtenerToken();
        var claims = _token.ObtenerClaimsDeToken(token);
        var perfilClaim = claims.First(x => x.Type == "perfil").Value;
        var secret = _configuration["Jwt:Key"] ?? "";
        if (attributes.Length > 0)
        {
            // Obtener el claim
            var myDecoratorAttribute = (PerfilAttribute)attributes[0];
            var perfilAtri = myDecoratorAttribute.Perfil;
            if (perfilAtri != perfilClaim)
                throw new UnauthorizedAccessException("El ususario no tiene acceso a la opción solicitada.");

        }
    }
}
