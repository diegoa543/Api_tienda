using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IEliminarProducto
{
    Task<Producto> DeleteProducto(int id);
}
