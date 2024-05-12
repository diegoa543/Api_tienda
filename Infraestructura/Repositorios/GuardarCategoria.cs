using System.Threading;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class GuardarCategoria : IInsertarCategoria
{
    private ApplicationContext _context;

    public GuardarCategoria(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Categoria> InsertarCategoria(string? nombre)
    {
        Categoria categoria = new Categoria { Nombre = nombre };
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
        if(categoria == null)
            throw new ArgumentNullException(nameof(categoria));
        return categoria;
    }
}
