using Microsoft.EntityFrameworkCore;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class ObtenerCategoria : IGetCategoria
{
    private readonly ApplicationContext _context;

    public ObtenerCategoria(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Categoria> GetCategoriaByIdAsync(int? id)
    {
        var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
        if (categoria == null)
            throw new ArgumentNullException(nameof(categoria));
        return categoria;
    }
}
