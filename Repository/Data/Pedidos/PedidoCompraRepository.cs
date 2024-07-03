using Dapper;
using examenOptativoP.Modelos;
using Npgsql;
using Repository.Data.ConfiguracionesDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Pedidos
{
    public class PedidoCompraRepository : IPedidoCompraRepository
    {
        IDbConnection connection;
        private string? connectionString;

        public PedidoCompraRepository(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new ConexionDB(connectionString).OpenConnection();
        }

        public bool add(PedidoCompraModel pedidoCompraModel)
        {
            try
            {
                var queryPedidoCompra = @"INSERT INTO pedido_compra(id_proveedor, id_sucursal, fechahora, total) 
                                          VALUES(@id_proveedor, @id_sucursal, @fechaHora, @total) RETURNING id";

                var idPedidoCompra = connection.QuerySingle<int>(queryPedidoCompra, new
                {
                    pedidoCompraModel.id_proveedor,
                    pedidoCompraModel.id_sucursal,
                    pedidoCompraModel.fechaHora,
                    pedidoCompraModel.total
                });

                foreach (var detalle in pedidoCompraModel.detallePedido)
                {
                    connection.Execute("INSERT INTO detalle_pedido(id_pedido, id_producto, cantidadproducto, subtotal) " +
                        "VALUES(@id_pedido, @id_producto, @cantidadProducto, @subtotal)", new
                        {
                            id_pedido = idPedidoCompra,
                            detalle.id_producto,
                            detalle.cantidadProducto,
                            detalle.subtotal
                        });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update(PedidoCompraModel pedidoCompraModel)
        {
            try
            {
                var queryPedidoCompra = @"UPDATE pedido_compra SET 
                                          id_proveedor=@id_proveedor, 
                                          id_sucursal=@id_sucursal,
                                          fechahora=@fecha_hora, 
                                          total=@total 
                                          WHERE id = @id";

                var queryDetallePedido = @"UPDATE detalle_pedido SET
                                           id_producto=@id_producto,
                                           cantidad_producto=@cantidad_producto,
                                           subtotal=@subtotal 
                                           WHERE id = @id";

                connection.Execute(queryPedidoCompra, pedidoCompraModel);

                foreach (var detalle in pedidoCompraModel.detallePedido)
                {
                    connection.Execute(queryDetallePedido, detalle);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool delete(int id)
        {
            try
            {
                connection.Execute("DELETE FROM detalle_pedido WHERE id_pedido = @id", new { Id = id });
                connection.Execute("DELETE FROM pedido_compra WHERE id = @id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PedidoCompraModel> listar()
        {
            try
            {
                var pedidoCompraDictionary = new Dictionary<int, PedidoCompraModel>();
                var query = @"SELECT 
                              pc.id, pc.id_proveedor, pc.id_sucursal, pc.fechahora, pc.total,
                              dp.id AS id_detalle, dp.id_producto, dp.cantidadproducto, dp.subtotal
                              FROM pedido_compra pc
                              LEFT JOIN detalle_pedido dp ON pc.id = dp.id_pedido";

                var pedidoCompra = connection.Query<PedidoCompraModel, DetallePedidoCompra, PedidoCompraModel>(query, (pedidoCompra, detallePedido) =>
                {
                    if (!pedidoCompraDictionary.TryGetValue(pedidoCompra.id, out var pedidoCompraActual))
                    {
                        pedidoCompraActual = pedidoCompra;
                        pedidoCompraActual.detallePedido = new List<DetallePedidoCompra>();
                        pedidoCompraDictionary.Add(pedidoCompraActual.id, pedidoCompraActual);
                    }

                    if (detallePedido != null)
                    {
                        pedidoCompraActual.detallePedido.Add(detallePedido);
                    }

                    return pedidoCompraActual;
                }, splitOn: "id_detalle");

                return pedidoCompra.Distinct();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PedidoCompraModel get(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT 
                                 pc.id, pc.id_proveedor, pc.id_sucursal, pc.fecha_hora, pc.total,
                                 dp.id AS id_detalle, dp.id_producto, dp.cantidad_producto, dp.subtotal
                                 FROM pedido_compra pc
                                 LEFT JOIN detalle_pedido dp ON pc.id = dp.id_pedido
                                 WHERE pc.id = @Id";

                var pedidoCompraDictionary = new Dictionary<int, PedidoCompraModel>();

                var pedidoCompra = connection.Query<PedidoCompraModel, DetallePedidoCompra, PedidoCompraModel>(query, (pedidoCompra, detallePedido) =>
                {
                    if (!pedidoCompraDictionary.TryGetValue(pedidoCompra.id, out var pedidoCompraActual))
                    {
                        pedidoCompraActual = pedidoCompra;
                        pedidoCompraActual.detallePedido = new List<DetallePedidoCompra>();
                        pedidoCompraDictionary.Add(pedidoCompraActual.id, pedidoCompraActual);
                    }

                    if (detallePedido != null)
                    {
                        pedidoCompraActual.detallePedido.Add(detallePedido);
                    }

                    return pedidoCompraActual;
                }, new { Id = id }, splitOn: "id_detalle").FirstOrDefault();

                return pedidoCompra;
            }
        }
    }
}
