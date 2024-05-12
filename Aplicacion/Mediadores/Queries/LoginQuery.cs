using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Queries;

public class LoginQuery : IRequest<TokenDto>
{
    public string? Email { get; set; }
    public string? Contra { get; set; }
}

public class LoginQueryValidation : AbstractValidator<LoginQuery>
{
    public LoginQueryValidation()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Contra).NotEmpty();
    }
}

public class LoginHandler : IRequestHandler<LoginQuery, TokenDto>
{
    private ApplicationContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<LoginHandler> _logger;
    private readonly ITokenSesion _tokenSesion;
    private readonly IUsuarioPerfil _usarioPerfil;

    public LoginHandler(ApplicationContext context, IConfiguration configuration, ILogger<LoginHandler> logger, ITokenSesion tokenSesion, IUsuarioPerfil usarioPerfil)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
        _tokenSesion = tokenSesion;
        _usarioPerfil = usarioPerfil;
    }

    public async Task<TokenDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Contra))
            throw new ArgumentException("Correo electrónico y contraseña son obligatorios");

        try
        {
            var email = request.Email;
            var contra = request.Contra;
            return await _usarioPerfil.ValidarUsuarioYPerfil(email,contra);
        }
        catch (Exception ex)
        {
            // Registrar detalles del error
            _logger.LogError(ex, "Ocurrió un error al procesar la solicitud de inicio de sesión. No se pudo conectar al servidor de base de datos: {ServerName}", _context);
            throw;
        }
    }
}
