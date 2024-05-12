using FluentValidation;
using MediatR;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;
using Tienda_API.Infraestructura.Repositorios;

namespace Tienda_API.Aplicacion.Mediadores.Commands;

public class SaveUsuario
{
    public class SaveUsuarioCommand : IRequest<UsuarioDTO>
    {
        public string Nombre { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Contraseña { get; set; } = null!;
        public int? Perfil { get; set; } = null;
    }
    public class SaveUsuarioCommandValidation : AbstractValidator<SaveUsuarioCommand>
    {
        public SaveUsuarioCommandValidation()
        {
            RuleFor(x => x.Nombre).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Contraseña).Cascade(CascadeMode.Stop).NotEmpty();
            RuleFor(x => x.Perfil).Cascade(CascadeMode.Stop).NotEmpty();

        }
    }
    public class SaveUsuarioHandler : IRequestHandler<SaveUsuarioCommand, UsuarioDTO>
    {
        private readonly SaveUsuarioCommandValidation _validation;
        private readonly ISaveUsuario _saveUsuario;

        public SaveUsuarioHandler(SaveUsuarioCommandValidation validation, ISaveUsuario saveUsuario)
        {
            _validation = validation;
            _saveUsuario = saveUsuario;
        }
        public async Task<UsuarioDTO> Handle(SaveUsuarioCommand request, CancellationToken cancellationToken)
        {
            _validation.Validate(request);
            var nombre = request.Nombre;
            var emial = request.Email;
            var contra = request.Contraseña;
            var perfil = request.Perfil;
            var usu = _saveUsuario.SaveUsuario(nombre,emial,contra,perfil);
            return await usu;
        }
    }

}
