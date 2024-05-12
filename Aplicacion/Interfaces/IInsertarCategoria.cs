using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IInsertarCategoria
{
    Task<Categoria> InsertarCategoria(string? nombre);
}
