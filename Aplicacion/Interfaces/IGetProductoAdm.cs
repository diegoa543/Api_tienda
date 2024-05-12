using Tienda_API.Aplicacion.Dtos;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IGetProductoAdm
{
    Task<List<ProductoAdmDTO>> GetProductoAdmDTOAsync(int? estados);
}
