using System;
using System.Collections.Generic;

namespace Tienda_API.Infraestructura.Repositorios;

public partial class Perfile
{
    public int Id { get; set; }

    public string NombrePerfil { get; set; } = null!;

    public virtual ICollection<UsuariosPerfile> UsuariosPerfiles { get; set; } = new List<UsuariosPerfile>();
}
