using System.Collections.Generic;
using Tienda_API.Aplicacion.Dtos;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IGetProducto
{
    Task<List<ProductoDTO>> GetProductoAsync(int? estados);
}
