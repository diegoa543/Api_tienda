using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Aplicacion.Mediadores.Queries;

public class GetProducto
{
    public class GetProductoQuery : IRequest<List<ProductoDTO>>
    {
        public int Estado { get; set; }
    }

    public class GetProductoQueryValidation : AbstractValidator<GetProductoQuery>
    {
        public GetProductoQueryValidation()
        {
            RuleFor(x => x.Estado).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }

    public class GetProductoHandle : IRequestHandler<GetProductoQuery, List<ProductoDTO>>
    {
        private readonly GetProductoQueryValidation _validation;
        private readonly IGetProducto _getProducto;

        public GetProductoHandle(GetProductoQueryValidation validation, IGetProducto getProducto)
        {
            _validation = validation;
            _getProducto = getProducto;
        }
        public async Task<List<ProductoDTO>> Handle(GetProductoQuery request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var estado = request.Estado;
            var producto = _getProducto.GetProductoAsync(estado);
            return await producto;
        }
    }
}
