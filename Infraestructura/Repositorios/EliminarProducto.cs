using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class EliminarProducto : IEliminarProducto
{
    private ApplicationContext _context;

    public EliminarProducto(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Producto> DeleteProducto(int id)
    {
        var producto = await _context.Productos.FirstOrDefaultAsync(x => x.Id == id);
        if(producto == null)
            throw new ArgumentNullException(nameof(producto));
        producto.Estado = 0;
        await _context.SaveChangesAsync();

        return producto;
    }
}
