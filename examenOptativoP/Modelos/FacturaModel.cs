using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenOptativoP.Modelos
{
    public class FacturaModel
    {
        public int id_factura { get; set; }
        public int id_cliente { get; set; }
        public int id_sucursal { get; set; }
        public string nro_factura { get; set; }
        public DateTime fecha_hora { get; set; }
        public float total { get; set; }
        public float total_iva5 { get; set; }
        public float total_iva10 { get; set; }
        public float total_iva { get; set; }
        public string total_letras { get; set; }
        public string sucursal { get; set; }
    }
}
