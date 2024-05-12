using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Queries;

public class GetUsuario
{
    public class GetUsuarioQuery : IRequest<Usuario>
    {
        public int Id { get; set; }
    }
    public class GetUsuarioQueryValidation : AbstractValidator<GetUsuarioQuery>
    {
        public GetUsuarioQueryValidation()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    public class GetUsuarioHandler : IRequestHandler<GetUsuarioQuery, Usuario>
    {
        private GetUsuarioQueryValidation _validation;
        private readonly IGetUsuario _getUsuario;

        public GetUsuarioHandler(GetUsuarioQueryValidation validation, IGetUsuario getUsuario)
        {
            _validation = validation;
            _getUsuario = getUsuario;
        }

        public async Task<Usuario> Handle(GetUsuarioQuery request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var id = request.Id;
            var usu = _getUsuario.GetUsuarioAsync(id);
            return await usu;
        }
    }
}
