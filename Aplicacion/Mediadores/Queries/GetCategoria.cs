using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Atributos;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Queries;

[Perfil("1")]
public class GetCategoria
{
    public class GetCategoriaQuery : DataSesion, IRequest<Categoria>
    {
        public int? Id { get; set; }

    }

    public class GetCategoriaQueryValidation : AbstractValidator<GetCategoriaQuery>
    {
        public GetCategoriaQueryValidation()
        {

            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }

    public class GetCategoriaHandler : IRequestHandler<GetCategoriaQuery, Categoria>
    {
        private GetCategoriaQueryValidation _validator;
        private readonly IConfiguration _configuration;
        private readonly IValidarToken _validarToken;
        private readonly IGetCategoria _getCategoria;

        public GetCategoriaHandler(GetCategoriaQueryValidation validator, IConfiguration configuration, IValidarToken validarToken, IGetCategoria getCategoria)
        {
            _validator = validator;
            _configuration = configuration;
            _validarToken = validarToken;
            _getCategoria = getCategoria;
        }

        public async Task<Categoria> Handle(GetCategoriaQuery request, CancellationToken cancellationToken)
        {
            _validator.Validate(request);
            var attributes = typeof(GetCategoria).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var idCategoria = request.Id;
            var categoria = _getCategoria.GetCategoriaByIdAsync(idCategoria);

            return await categoria;
        }
    }
}
