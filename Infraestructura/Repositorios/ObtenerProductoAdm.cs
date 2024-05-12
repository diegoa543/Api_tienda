using Microsoft.EntityFrameworkCore;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class ObtenerProductoAdm : IGetProductoAdm
{
    private readonly ApplicationContext _context;

    public ObtenerProductoAdm(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<List<ProductoAdmDTO>> GetProductoAdmDTOAsync(int? estado)
    {
        var producto = await _context.Productos
                        .Where(x => x.Estado == estado)
                        .Include(x => x.Categoria)
                        .Select(x => new ProductoAdmDTO
                        {
                            ProductoId = x.Id,
                            NombreProducto = x.Nombre,
                            Descripcion = x.Descripción,
                            Precio = x.Precio,
                            Cantidad = x.Cantidad,
                            NombreCategoria = string.Join(", ", x.Categoria.Select(c => c.Nombre))
                        })
                        .ToListAsync();
        if(producto == null)
            throw new ArgumentNullException(nameof(producto));

        return producto;
    }
}
