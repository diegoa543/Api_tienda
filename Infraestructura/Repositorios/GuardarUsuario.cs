using System.Threading;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class GuardarUsuario : ISaveUsuario
{
    private ApplicationContext _context;

    public GuardarUsuario(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<UsuarioDTO> SaveUsuario(string nombre, string mail, string contra, int? perfil)
    {
        Usuario usu = new Usuario
        {
            Nombre = nombre,
            Email = mail,
            Contraseña = contra
        };
        await _context.Usuarios.AddAsync(usu);
        await _context.SaveChangesAsync();
        if(usu == null)
            throw new ArgumentNullException(nameof(usu));

        UsuariosPerfile perfilUsu = new UsuariosPerfile
        {
            UsuarioId = usu.Id,
            PerfilId = perfil
        };
        await _context.UsuariosPerfiles.AddAsync(perfilUsu);
        await _context.SaveChangesAsync();
        if(perfil == null)
            throw new ArgumentNullException(nameof(perfil));

        UsuarioDTO usuarioPerfil = new UsuarioDTO
        {
            Nombre = usu.Nombre,
            Email = usu.Email,
            Contraseña = usu.Contraseña,
            Perfil = perfilUsu.PerfilId
        };

        return usuarioPerfil;
    }
}
