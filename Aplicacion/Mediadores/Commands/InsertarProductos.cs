using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Atributos;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Commands;

[Perfil("1")]
public class InsertarProductos
{
    public class InsertProductoCommand : IRequest<Producto>
    {
        public string? Nombre { get; set; }

        public string? Descripción { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public string? Categoria { get; set; }

        public int Estado { get; set; }
    }


    public class InsertProductoCommandValidation : AbstractValidator<InsertProductoCommand>
    {
        public InsertProductoCommandValidation()
        {
            RuleFor(x => x.Nombre).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Descripción).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Precio).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Cantidad).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Categoria).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Estado).Cascade(CascadeMode.Stop).NotEmpty();

        }
    }

    public class InsertProductoHandler : IRequestHandler<InsertProductoCommand, Producto>
    {
        private readonly InsertProductoCommandValidation _validation;
        private readonly IProducto _producto;
        private readonly IValidarToken _validarToken;

        public InsertProductoHandler(InsertProductoCommandValidation validation, IProducto producto, IValidarToken validarToken)
        {
            _validation = validation;
            _producto = producto;
            _validarToken = validarToken;
        }
        public async Task<Producto> Handle(InsertProductoCommand request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var attributes = typeof(InsertarProductos).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var producto = _producto
                .SaveProducto(request.Nombre, request.Descripción, request.Precio, request.Cantidad, request.Categoria, request.Estado);
            return await producto;
        }
    }
}
