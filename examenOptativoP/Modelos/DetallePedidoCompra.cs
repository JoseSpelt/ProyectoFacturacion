﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenOptativoP.Modelos
{
    public class DetallePedidoCompra
    {
        public int id { get; set; }
        public int id_pedido { get; set; }
        public int id_producto { get; set; }
        public int cantidadProducto { get; set; }
        public decimal subtotal { get; set; }

    }
}
