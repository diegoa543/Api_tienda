using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IProducto
{
    Task<Producto> SaveProducto(string nombre, string descripcion, decimal precio, int cantidad, string categoria, int estado);    
}
