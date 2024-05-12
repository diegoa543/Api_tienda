using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tienda_API.Infraestructura.Repositorios;
using static Tienda_API.Aplicacion.Mediadores.Commands.InsertarCategoria;
using static Tienda_API.Aplicacion.Mediadores.Queries.GetCategoria;

namespace Tienda_API.Infraestructura.Controladores;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [Route("getCategoria")]
    public async Task<ActionResult<Categoria>> GetCategoriaById(int id)
    {
        try
        {
            return Ok(await _mediator.Send(new GetCategoriaQuery { Id = id }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    [Route("saveCategoria")]
    public async Task<ActionResult<Categoria>> InsertarCategoria([FromBody] InsertarCategoriaCommand insertarCategoriaCommand)
    {
        try
        {
            return Ok(await _mediator.Send(insertarCategoriaCommand)); return Ok(await _mediator.Send(insertarCategoriaCommand));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }           
}
