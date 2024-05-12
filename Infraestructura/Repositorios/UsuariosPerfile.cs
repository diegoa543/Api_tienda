using System;
using System.Collections.Generic;

namespace Tienda_API.Infraestructura.Repositorios;

public partial class UsuariosPerfile
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int? PerfilId { get; set; }

    public virtual Perfile? Perfil { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
