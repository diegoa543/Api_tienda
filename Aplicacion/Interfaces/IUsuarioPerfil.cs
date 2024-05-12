using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IUsuarioPerfil
{
    Task<Usuario?> GetUsuarioByEmailAndContraAsync(string email, string contra);
    Task<UsuariosPerfile?> GetUsuarioPerfilByUsuarioIdAsync(int usuarioId);
    Task<TokenDto> ValidarUsuarioYPerfil(string email, string contra);
}
