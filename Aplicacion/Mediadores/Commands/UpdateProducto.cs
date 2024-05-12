using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Atributos;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Commands;

[Perfil("1")]
public class UpdateProducto
{
    public class UpdateProductoCommand : IRequest<Producto>
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public string? Descripción { get; set; }

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

    }

    public class UpdateProductoCommandValidation : AbstractValidator<UpdateProductoCommand>
    {
        public UpdateProductoCommandValidation()
        {
            RuleFor(x => x.Id).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Nombre).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Descripción).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Precio).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Cantidad).Cascade(CascadeMode.Stop).NotEmpty();

        }
    }

    public class UpdateProductoHandler : IRequestHandler<UpdateProductoCommand, Producto>
    {
        private readonly UpdateProductoCommandValidation _validation;
        private readonly IValidarToken _validarToken;
        private readonly IUpdateProducto _updateProducto;

        public UpdateProductoHandler(UpdateProductoCommandValidation validation, IValidarToken validarToken, IUpdateProducto updateProducto)
        {
            _validation = validation;
            _validarToken = validarToken;
            _updateProducto = updateProducto;
        }
        public async Task<Producto> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var attributes = typeof(UpdateProducto).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var producto = _updateProducto.UpdateProducto(request.Id,request.Nombre,request.Descripción,request.Precio,request.Cantidad);
            return await producto;
        }
    }
}
