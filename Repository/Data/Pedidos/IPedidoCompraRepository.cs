using examenOptativoP.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Pedidos
{
    public interface IPedidoCompraRepository
    {
        bool add(PedidoCompraModel pedidoCompraModel);
        bool update(PedidoCompraModel pedidoCompraModel);
        bool delete(int id);
        PedidoCompraModel get(int id);
        IEnumerable<PedidoCompraModel> listar();
    }
}
