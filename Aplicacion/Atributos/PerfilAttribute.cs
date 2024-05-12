namespace Tienda_API.Aplicacion.Atributos;

public class PerfilAttribute : Attribute
{
    public string Perfil { get; set; }

    public PerfilAttribute(string perfil)
    {
        Perfil = perfil;
    }
}
