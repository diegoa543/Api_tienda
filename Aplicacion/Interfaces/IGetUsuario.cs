using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IGetUsuario
{
    Task<Usuario> GetUsuarioAsync(int? id);
}
