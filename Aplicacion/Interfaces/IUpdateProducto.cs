using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces
{
    public interface IUpdateProducto
    {
        Task<Producto> UpdateProducto(int id, string nombre, string descripcion, decimal precio, int cantidad);

    }
}
