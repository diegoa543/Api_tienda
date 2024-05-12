using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Dtos;

public class EnviarCorreoDTO
{
    public int ClienteId { get; set; }
    public int PedidoId { get; set; }
    public decimal Total { get; set; }

}
