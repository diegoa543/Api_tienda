using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Atributos;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Commands;

[Perfil("1")]
public class DeleteProducto
{
    public class DeleteProductoCommand : IRequest<Producto>
    {
        public int Id { get; set; }
    }

    public class DeleteProductoCommandValidation : AbstractValidator<DeleteProductoCommand>
    {
        public DeleteProductoCommandValidation()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    public class DeleteProductoHandler : IRequestHandler<DeleteProductoCommand, Producto>
    {
        private readonly DeleteProductoCommandValidation _validation;
        private readonly IEliminarProducto _producto;
        private readonly IValidarToken _validarToken;

        public DeleteProductoHandler(DeleteProductoCommandValidation validation, IEliminarProducto producto, IValidarToken validarToken)
        {
            _validation = validation;
            _producto = producto;
            _validarToken = validarToken;
        }
        public async Task<Producto> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var attributes = typeof(DeleteProducto).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var producto = _producto.DeleteProducto(request.Id);
            return await producto;
        }
    }
}
