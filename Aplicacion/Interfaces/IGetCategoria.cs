using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IGetCategoria
{
    Task<Categoria> GetCategoriaByIdAsync(int? id);
}
