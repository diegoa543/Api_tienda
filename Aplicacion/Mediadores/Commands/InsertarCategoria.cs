using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Atributos;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Commands;

[Perfil("1")]
public class InsertarCategoria
{
    public class InsertarCategoriaCommand : IRequest<Categoria>
    {
        public string? Nombre { get; set; }
    }

    public class InsertarCategoriaCommandValidation : AbstractValidator<InsertarCategoriaCommand>
    {
        public InsertarCategoriaCommandValidation()
        {
            RuleFor(x => x.Nombre).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }
    public class InsertarCategoriaHandler : IRequestHandler<InsertarCategoriaCommand, Categoria>
    {
        private readonly InsertarCategoriaCommandValidation _validation;
        private readonly IInsertarCategoria _insertarCategoria;
        private readonly IValidarToken _validarToken;

        public InsertarCategoriaHandler(InsertarCategoriaCommandValidation validation, IInsertarCategoria insertarCategoria, IValidarToken validarToken)
        {
            _validation = validation;
            _insertarCategoria = insertarCategoria;
            _validarToken = validarToken;
        }
        public async Task<Categoria> Handle(InsertarCategoriaCommand request, CancellationToken cancellationToken)
        {
           _validation.Validate(request);
            var attributes = typeof(InsertarCategoria).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var categotia = _insertarCategoria.InsertarCategoria(request.Nombre);
            return await categotia;
        }
    }
}
