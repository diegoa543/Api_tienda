﻿namespace Tienda_API.Aplicacion.Dtos;

public class ProductoDTO
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
}
