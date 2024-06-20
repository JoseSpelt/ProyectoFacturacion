using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenOptativoP.Modelos
{
    public class DetalleFacturaModel
    {
        public int id_detalle { get; set; }
        public int id_factura { get; set; }
        public int id_producto { get; set; }
        public int cantidad_producto { get; set; }
        public float subtotal { get; set; }
    }
}
