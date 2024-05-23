using examenOptativoP.Modelos;
using Repository.Data.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logica
{
    public class ClienteService
    {
        private ClienteRepository clienteRepository;
        public ClienteService(string connectionString)
        {
            clienteRepository = new ClienteRepository(connectionString);
        }

        public bool add(ClienteModel cliente)
        {
            return validarDatos(cliente) ? clienteRepository.add(cliente) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<ClienteModel> listar()
        {
            return clienteRepository.listar();
        }

        public bool delete(int id)
        {
            return id > 0 ? clienteRepository.delete(id) : false;
        }


        public bool update(ClienteModel clienteModel)
        {
            return validarDatos(clienteModel) ? clienteRepository.update(clienteModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(ClienteModel cliente)
        {
            if (cliente == null)
                return false;
            if (string.IsNullOrEmpty(cliente.nombre) || string.IsNullOrEmpty(cliente.apellido) || string.IsNullOrEmpty(cliente.documento))
                return false;
            if (cliente.nombre.Length < 3 || cliente.apellido.Length < 3 || cliente.documento.Length < 3)
                return false;
            if (cliente.celular.Length < 10)
                return false;
            return true;
        }
        public ClienteModel get(int id)
        {
            return clienteRepository.get(id);
        }
    }
}
