using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Tienda_API.Aplicacion.Mediadores.Commands.InsertarPedido;

namespace Tienda_API.Infraestructura.Controladores;

[Route("api/[controller]")]
[ApiController]
public class PedidoController : ControllerBase
{
    private readonly IMediator _mediator;

    public PedidoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    [Route("save")]
    public async Task<IActionResult> CrearPedido([FromBody] InsertarPedidoCommand insertarPedidoCommand)
    {
        var pedido = await _mediator.Send(insertarPedidoCommand);
        return Ok(pedido);
    }
}
