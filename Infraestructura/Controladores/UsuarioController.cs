using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Tienda_API.Aplicacion.Mediadores.Queries.GetUsuario;
using Tienda_API.Infraestructura.Repositorios;
using static Tienda_API.Aplicacion.Mediadores.Commands.SaveUsuario;
using Tienda_API.Aplicacion.Dtos;

namespace Tienda_API.Infraestructura.Controladores;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-by-id/{id}")]
    [Authorize]
    public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
    {
        try
        {
            return Ok(await _mediator.Send(new GetUsuarioQuery { Id = id }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }

    [HttpPost]
    [Route("save")]
    [Authorize]
    public async Task<ActionResult<UsuarioDTO>> SaveUsuario([FromBody] SaveUsuarioCommand saveUsuarioCommand)
    {
        try
        {
            return Ok(await _mediator.Send(saveUsuarioCommand));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Ocurrió un error al procesar la solicitud. ", message = ex.Message });
        }
    }
            

}
