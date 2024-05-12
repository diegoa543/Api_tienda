﻿using System;
using System.Collections.Generic;

namespace Tienda_API.Infraestructura.Repositorios;

public partial class DetallesPedido
{
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual Pedido? Pedido { get; set; }

    public virtual Producto? Producto { get; set; }
}
