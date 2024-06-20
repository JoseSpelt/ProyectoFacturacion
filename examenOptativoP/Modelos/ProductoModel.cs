using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenOptativoP.Modelos
{
    public class ProductoModel
    {
        public int id_producto { get; set; }
        public string descripcion { get; set; }
        public int cantidad_minima { get; set; }
        public int cantidad_stock { get; set; }
        public float precio_compra { get; set; }
        public float precio_venta { get; set; }
        public string categoria { get; set; }
        public string marca { get; set; }
        public string estado { get; set; }
    }
}
