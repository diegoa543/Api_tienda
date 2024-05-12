using Microsoft.EntityFrameworkCore;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class ValidarUsuarioPerfil : IUsuarioPerfil
{
    private ApplicationContext _context;
    private readonly ITokenSesion _token;

    public ValidarUsuarioPerfil(ApplicationContext context, ITokenSesion token)
    {
        _context = context;
        _token = token;
    }

    public async Task<Usuario?> GetUsuarioByEmailAndContraAsync(string email, string contra)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Contraseña == contra);
    }

    public async Task<UsuariosPerfile?> GetUsuarioPerfilByUsuarioIdAsync(int usuarioId)
    {
        return await _context.UsuariosPerfiles.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId);
    }

    public async Task<TokenDto> ValidarUsuarioYPerfil(string email, string contra)
    {
        var usuario = await GetUsuarioByEmailAndContraAsync(email, contra);

        if (usuario != null)
        {
            var perfil = await GetUsuarioPerfilByUsuarioIdAsync(usuario.Id);

            if (perfil != null)
            {
                var tokenString = _token.CrearToken(usuario, perfil);
                TokenDto tk = new() { Token = tokenString };
                return tk;
            }
            else
            {
                throw new UnauthorizedAccessException("Perfil de usuario no encontrado");
            }
        }
        else
        {
            throw new UnauthorizedAccessException("Usuario no encontrado");
        }
    }
}
