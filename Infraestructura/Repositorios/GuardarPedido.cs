using Microsoft.EntityFrameworkCore;
using System.Threading;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class GuardarPedido : IInsertarPedido
{
    private ApplicationContext _context;
    private IEnviarCorreo _enviarCorreo;

    public GuardarPedido(ApplicationContext context, IEnviarCorreo enviarCorreo)
    {
        _context = context;
        _enviarCorreo = enviarCorreo;
    }

    public async Task<Pedido> InsertarPedido(int clienteId, List<DetallesPedido> detalles)
    {
        var pedido = new Pedido
        {
            ClienteId = clienteId,
            Fecha = DateTime.Now,
            Total = 0, //Iniciamos el total con 0
            Estado = "Activo"
        };
        await _context.Pedidos.AddAsync(pedido);
        await _context.SaveChangesAsync();

        foreach (var detalle in detalles)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(x => x.Id == detalle.ProductoId);
            if (producto == null || producto.Cantidad < detalle.Cantidad)
                throw new Exception("Producto no disponible o cantidad insuficiente");

            producto.Cantidad -= detalle.Cantidad;

            var detallePedido = new DetallesPedido
            {
                PedidoId = pedido.Id,
                ProductoId = detalle.ProductoId,
                Cantidad = detalle.Cantidad,
                Precio = producto.Precio * detalle.Cantidad
            };

            pedido.Total += detallePedido.Precio;

            _ = _context.DetallesPedidos.AddAsync(detallePedido);
        }

        await _context.SaveChangesAsync();
        await _enviarCorreo.EnviarCorreoCliente(pedido.ClienteId,pedido.Id,pedido.Total);

        return pedido;
    }
}
