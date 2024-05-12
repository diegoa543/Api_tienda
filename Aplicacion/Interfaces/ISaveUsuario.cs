using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface ISaveUsuario
{
    Task<UsuarioDTO> SaveUsuario(string nombre, string mail, string contra, int? perfil);
}
