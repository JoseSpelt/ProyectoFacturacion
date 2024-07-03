using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenOptativoP.Modelos
{
    public class ProveedorModel
    {
        public int id { get; set; }
        public string razonSocial { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string direccion { get; set; }
        public string mail { get; set; }
        public string celular { get; set; }
        public string estado { get; set; }
    }
}
