using examenOptativoP.Modelos;
using Repository.Data.Facturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Logica
{
    public class FacturaService
    {
        private FacturaRepository facturaRepository;
        public FacturaService(string connectionString)
        {
            facturaRepository = new FacturaRepository(connectionString);
        }

        public bool add(FacturaModel factura)
        {
            return validarDatos(factura) ? facturaRepository.add(factura) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<FacturaModel> listar()
        {
            return facturaRepository.listar();
        }

        public bool delete(int id)
        {
            return id > 0 ? facturaRepository.delete(id) : false;
        }


        public bool update(FacturaModel facturaModel)
        {
            return validarDatos(facturaModel) ? facturaRepository.update(facturaModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(FacturaModel factura)
        {
            if (!Regex.IsMatch(factura.nro_factura, @"^\d{3}-\d{3}-\d{3}-\d{6}$"))
            {
                return false;
            }

            if (factura.total <= 0 || (factura.total_iva5 <= 0 && factura.total_iva10 <= 0) || factura.total_iva <= 0)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(factura.total_letras) || factura.total_letras.Length < 6)
            {
                return false;
            }

            return true;
        }

        public FacturaModel get(int id)
        {
            return facturaRepository.get(id);
        }
    }
}
