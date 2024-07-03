using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenOptativoP.Modelos
{
    public class PedidoCompraModel
    {
        public int id { get; set; }
        public int id_proveedor { get; set; }
        public int id_sucursal { get; set; }
        public DateTime fechaHora { get; set; }
        public decimal total { get; set; }

        public List<DetallePedidoCompra> detallePedido { get; set; }
    }
}
