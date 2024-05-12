using MediatR;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Mediadores.Commands;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Interfaces;

public interface IInsertarPedido 
{
    Task<Pedido> InsertarPedido(int clienteId, List<DetallesPedido> detalles);
}
