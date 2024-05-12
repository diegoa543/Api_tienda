using Microsoft.AspNetCore.Authorization;

namespace Tienda_API.Aplicacion.Atributos;

public class AutorizarAttribute : AuthorizeAttribute
{
    public string Perfil { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="perfil"></param>
    public AutorizarAttribute(string perfil)
    {
        Perfil = perfil;
    }
}
