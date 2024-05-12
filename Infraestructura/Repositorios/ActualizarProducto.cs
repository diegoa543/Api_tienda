using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class ActualizarProducto : IUpdateProducto
{
    private ApplicationContext _context;

    public ActualizarProducto(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<Producto> UpdateProducto(int id, string nombre, string descripcion, decimal precio, int cantidad)
    {
        var producto = await _context.Productos.FirstOrDefaultAsync(x => x.Id == id);
        if (producto == null)
            throw new ArgumentNullException(nameof(producto));

        producto.Nombre = nombre;
        producto.Descripción = descripcion;
        producto.Precio = precio;
        producto.Cantidad = cantidad;
        await _context.SaveChangesAsync();

        return producto;
    }
}
