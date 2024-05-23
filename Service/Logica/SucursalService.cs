using examenOptativoP.Modelos;
using Repository.Data.Sucursal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Logica
{
    public class SucursalService
    {
        private SucursalRepository sucursalRepository;
        public SucursalService(string connectionString)
        {
            sucursalRepository = new SucursalRepository(connectionString);
        }

        public bool add(SucursalModel sucursal)
        {
            return validarDatos(sucursal) ? sucursalRepository.add(sucursal) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<SucursalModel> listar()
        {
            return sucursalRepository.listar();
        }

        public bool delete(int id)
        {
            return id > 0 ? sucursalRepository.delete(id) : false;
        }


        public bool update(SucursalModel sucursalModel)
        {
            return validarDatos(sucursalModel) ? sucursalRepository.update(sucursalModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(SucursalModel sucursal)
        {
            if (string.IsNullOrEmpty(sucursal.direccion) || sucursal.direccion.Length < 10)
            {
                return false;
            }

            if (string.IsNullOrEmpty(sucursal.email))
            {
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(sucursal.email, emailPattern))
            {
                return false;
            }
            return true;
        }

        public SucursalModel get(int id)
        {
            return sucursalRepository.get(id);
        }
    }
}
