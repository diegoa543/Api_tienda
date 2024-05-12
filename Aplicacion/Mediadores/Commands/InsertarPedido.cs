using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Commands;

public class InsertarPedido 
{
    public class InsertarPedidoCommand : IRequest<Pedido>
    {
        public int ClienteId { get; set; }
        public List<DetallesPedido> Detalles { get; set; }
    }
    
    public class InsertarPedidoCommandValidation : AbstractValidator<InsertarPedidoCommand>
    {
        public InsertarPedidoCommandValidation()
        {
            RuleFor(x => x.ClienteId).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Detalles).Cascade(CascadeMode.Stop).NotEmpty();
        }
    }

    public class InsertarPedidoCommandHandler : IRequestHandler<InsertarPedidoCommand, Pedido>
    {
        private readonly InsertarPedidoCommandValidation _validation;
        private readonly IInsertarPedido _insertarPedido;

        public InsertarPedidoCommandHandler(InsertarPedidoCommandValidation validation, IInsertarPedido insertarPedido)
        {
            _validation = validation;
            _insertarPedido = insertarPedido;
        }

        public async Task<Pedido> Handle(InsertarPedidoCommand request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var pedido = _insertarPedido.InsertarPedido(request.ClienteId,request.Detalles);
            return await pedido;
            
        }
    }
}
