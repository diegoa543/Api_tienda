using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Atributos;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Aplicacion.Mediadores.Queries;

[Perfil("1")]
public class GetProductoAdm
{
    public class GetProductoAdmQuery : IRequest<List<ProductoAdmDTO>>
    {
        public int Estado { get; set; }
    }

    public class GetProductoAdmQueryValidation : AbstractValidator<GetProductoAdmQuery>
    {
        public GetProductoAdmQueryValidation()
        {
            RuleFor(x => x.Estado).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }

    public class GetProducAdmtoHandle : IRequestHandler<GetProductoAdmQuery, List<ProductoAdmDTO>>
    {
        private readonly GetProductoAdmQueryValidation _validation;
        private readonly IGetProductoAdm _getProductoAdm;
        private readonly IValidarToken _validarToken;

        public GetProducAdmtoHandle(GetProductoAdmQueryValidation validation, IGetProductoAdm getProductoAdm, IValidarToken validarToken)
        {
            _validation = validation;
            _getProductoAdm = getProductoAdm;
            _validarToken = validarToken;
        }
        public async Task<List<ProductoAdmDTO>> Handle(GetProductoAdmQuery request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var attributes = typeof(GetProductoAdm).GetCustomAttributes(typeof(PerfilAttribute), false);
            _validarToken.Validar(attributes);
            var estado = request.Estado;
            var producto = _getProductoAdm.GetProductoAdmDTOAsync(estado);
            return await producto;
        }
    }
}
