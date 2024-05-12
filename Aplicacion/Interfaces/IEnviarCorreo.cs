using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IEnviarCorreo
{
    Task<EnviarCorreoDTO> EnviarCorreoCliente(int clienteId, int pedidoId, decimal total);
}
