using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Tienda_API.Aplicacion.Mediadores.Queries.GetProducto;
using Tienda_API.Aplicacion.Dtos;
using static Tienda_API.Aplicacion.Mediadores.Queries.GetProductoAdm;
using Tienda_API.Infraestructura.Repositorios;
using static Tienda_API.Aplicacion.Mediadores.Commands.InsertarProductos;
using static Tienda_API.Aplicacion.Mediadores.Commands.DeleteProducto;
using static Tienda_API.Aplicacion.Mediadores.Commands.UpdateProducto;

namespace Tienda_API.Infraestructura.Controladores;
[Route("api/[controller]")]
[ApiController]
public class ProductoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("Get")]
    [Authorize]
    public async Task<ActionResult<List<ProductoDTO>>> GetProductos()
    {
        try
        {
            return Ok(await _mediator.Send(new GetProductoQuery { Estado = 1 }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpGet]
    [Route("GetAdm")]
    [Authorize]
    public async Task<ActionResult<List<Producto>>> GetProductosAdm()
    {
        try
        {
            var productoAdm = await _mediator.Send(new GetProductoAdmQuery { Estado = 1 });
            return Ok(productoAdm);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }        
    }

    [HttpPost]
    [Route("save")]
    [Authorize]
    public async Task<ActionResult<Producto>> InsertProducto([FromBody] InsertProductoCommand insertProductoCommand)
    {
        try
        {
            return Ok(await _mediator.Send(insertProductoCommand));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPut]
    [Route("Update")]
    [Authorize]
    public async Task<ActionResult<Producto>> UpdateProducto([FromBody] UpdateProductoCommand updateProductoCommand)
    {
        try
        {
            return Ok(await _mediator.Send(updateProductoCommand));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }
            

    [HttpDelete]
    [Route("Delete")]
    [Authorize]
    public async Task<ActionResult<Producto>> DeleteProducto(int id)
    {
        try
        {
            return Ok(await _mediator.Send(new DeleteProductoCommand { Id = id }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

}
