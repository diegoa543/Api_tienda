using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios
{
    public class GuardarProducto : IProducto
    {
        private ApplicationContext _context;

        public GuardarProducto(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Producto> SaveProducto(string nombre, string descripcion, decimal precio, int cantidad, string categoria, int estado)
        {
            var categoriaProducto = await _context.Categorias.FirstOrDefaultAsync(x => x.Nombre == categoria);
            if (categoriaProducto == null)
                throw new ArgumentNullException(nameof(categoriaProducto));
            Producto prod = new Producto
            {
                Nombre = nombre,
                Descripción = descripcion,
                Precio = precio,
                Cantidad = cantidad,
                Categoria = new List<Categoria> { categoriaProducto },
                Estado = estado
            };
            await _context.Productos.AddAsync(prod);
            await _context.SaveChangesAsync();
            return prod;
        }
    }
}
