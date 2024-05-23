using examenOptativoP.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Clientes
{
    public interface IClienteRepository
    {
       bool add(ClienteModel clienteModel);
       bool update(ClienteModel clienteModel);
       bool delete(int id);
        ClienteModel get(int id);
       IEnumerable<ClienteModel> listar();

    }
}
