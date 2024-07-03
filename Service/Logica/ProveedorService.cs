using examenOptativoP.Modelos;
using Repository.Data.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logica
{
    public class ProveedorService
    {
        private ProveedorRepository proveedorRepository;

        public ProveedorService(string connectionString)
        {
            proveedorRepository = new ProveedorRepository(connectionString);
        }

        public bool add(ProveedorModel proveedor)
        {
            return validarDatos(proveedor) ? proveedorRepository.add(proveedor) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<ProveedorModel> listar()
        {
            return proveedorRepository.listar();
        }

        public bool delete(int id)
        {
            return id > 0 ? proveedorRepository.delete(id) : false;
        }

        public bool update(ProveedorModel proveedorModel)
        {
            return validarDatos(proveedorModel) ? proveedorRepository.update(proveedorModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(ProveedorModel proveedor)
        {
            if (proveedor == null)
                return false;

            if (string.IsNullOrWhiteSpace(proveedor.razonSocial) ||
                string.IsNullOrWhiteSpace(proveedor.tipoDocumento) ||
                string.IsNullOrWhiteSpace(proveedor.numeroDocumento))
            {
                return false;
            }

            if (proveedor.razonSocial.Length < 3 ||
                proveedor.numeroDocumento.Length < 3 || 
                proveedor.tipoDocumento.Length < 3)
            {
                return false;
            }

            if (!EsNumero(proveedor.celular) || proveedor.celular.Length != 10)
            {
                return false;
            }

            return true;   
        }
        private bool EsNumero(string input)
        {
            return long.TryParse(input, out _);
        }

        public ProveedorModel get(int id)
        {
            return proveedorRepository.get(id);
        }
    }
}
