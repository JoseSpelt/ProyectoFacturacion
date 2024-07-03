using examenOptativoP.Modelos;
using Repository.Data.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logica
{
    public class PedidoCompraService
    {
        private PedidoCompraRepository pedidoCompraRepository;

        public PedidoCompraService(string connectionString)
        {
            pedidoCompraRepository = new PedidoCompraRepository(connectionString);
        }

        public bool add(PedidoCompraModel pedidoCompra)
        {
            return validarDatos(pedidoCompra) ? pedidoCompraRepository.add(pedidoCompra) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<PedidoCompraModel> listar()
        {
            return pedidoCompraRepository.listar();
        }

        public bool delete(int id)
        {
            return id > 0 ? pedidoCompraRepository.delete(id) : false;
        }

        public bool update(PedidoCompraModel pedidoCompraModel)
        {
            return validarDatos(pedidoCompraModel) ? pedidoCompraRepository.update(pedidoCompraModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public bool validarDatos(PedidoCompraModel pedidoCompra)
        {
            if (pedidoCompra == null || pedidoCompra.detallePedido == null || pedidoCompra.detallePedido.Count == 0)
                return false;

            if (pedidoCompra.id_proveedor <= 0 || pedidoCompra.id_sucursal <= 0)
                return false;

            return true;
        }

        public PedidoCompraModel get(int id)
        {
            return pedidoCompraRepository.get(id);
        }
    }
}
