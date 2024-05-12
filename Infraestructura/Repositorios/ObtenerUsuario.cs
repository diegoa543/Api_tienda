using Microsoft.EntityFrameworkCore;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class ObtenerUsuario : IGetUsuario
{
    private ApplicationContext _context;

    public ObtenerUsuario(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Usuario> GetUsuarioAsync(int? id)
    {
        var usu = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        if(usu == null)
            throw new ArgumentNullException(nameof(usu));
        return usu;
    }
}
