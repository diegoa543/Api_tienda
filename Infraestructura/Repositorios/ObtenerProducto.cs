using Microsoft.EntityFrameworkCore;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class ObtenerProducto : IGetProducto
{
    private readonly ApplicationContext _context;

    public ObtenerProducto(ApplicationContext context)
    {
        _context = context;
    }
     public async Task<List<ProductoDTO>> GetProductoAsync(int? estado)
    {
        var producto = await _context.Productos
                    .Where(x => x.Estado == estado)
                    .Select(x => new ProductoDTO { Nombre = x.Nombre, Descripcion = x.Descripción, Precio = x.Precio, Cantidad = x.Cantidad })
                    .ToListAsync();
        if(producto == null)
            throw new ArgumentNullException(nameof(producto));

        return producto;
    }
}
