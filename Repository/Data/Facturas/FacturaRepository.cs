using Dapper;
using examenOptativoP.Modelos;
using Npgsql;
using Repository.Data.ConfiguracionesDB;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Facturas
{
    public class FacturaRepository : IFacturaRepository
    {
        IDbConnection connection;
        private string? connectionString;

        public FacturaRepository(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new ConexionDB(connectionString).OpenConnection();
        }

        public bool add(FacturaModel facturaModel)
        {
            try
            {
                var queryFactura = @"INSERT INTO factura(id_sucursal, id_cliente, nro_factura, fecha_hora, total, total_iva5, total_iva10, total_iva, total_letras, sucursal) 
                             VALUES(@id_sucursal, @id_cliente, @nro_factura, @fecha_hora, @total, @total_iva5, @total_iva10, @total_iva, @total_letras, @sucursal) RETURNING id_factura";

                var idFactura = connection.QuerySingle<int>(queryFactura, new
                {
                    facturaModel.id_sucursal,
                    facturaModel.id_cliente,
                    facturaModel.nro_factura,
                    facturaModel.fecha_hora,
                    facturaModel.total,
                    facturaModel.total_iva5,
                    facturaModel.total_iva10,
                    facturaModel.total_iva,
                    facturaModel.total_letras,
                    facturaModel.sucursal
                });

                foreach (var detalle in facturaModel.detalleFactura)
                {
                    connection.Execute("INSERT INTO detalle_factura(id_factura, id_producto, cantidad_producto, subtotal) " +
                        "VALUES(@id_factura, @id_producto, @cantidad_producto, @subtotal)", new
                        {
                            id_factura = idFactura,
                            detalle.id_producto,
                            detalle.cantidad_producto,
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


        public bool update(FacturaModel facturaModel)
        {
            try
            {
                var queryFactura = @"UPDATE factura SET 
                                     id_sucursal=@id_sucursal, 
                                     id_cliente=@id_cliente,
                                     nro_factura=@nro_factura,
                                     fecha_hora=@fecha_hora, 
                                     total=@total, 
                                     total_iva5=@total_iva5, 
                                     total_iva10=@total_iva10, 
                                     total_iva=@total_iva, 
                                     total_letras=@total_letras, 
                                     sucursal=@sucursal 
                                     WHERE id_factura = @id_factura";

                var queryDetalleFactura = @"UPDATE detalle_factura SET
                                            id_producto=@id_producto,
                                            cantidad_producto=@cantidad_producto,
                                            subtotal=@subtotal 
                                            WHERE id_detalle = @id_detalle";

                connection.Execute(queryFactura, facturaModel);

                foreach (var detalle in facturaModel.detalleFactura)
                {
                    connection.Execute(queryDetalleFactura, detalle);
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
                connection.Execute("DELETE FROM detalle_factura WHERE id_factura = @id", new { Id = id });
                connection.Execute("DELETE FROM factura WHERE id_factura = @id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<FacturaModel> listar()
        {
            try
            {
                var facturaDictionary = new Dictionary<int, FacturaModel>();
                var query = @"SELECT 
                              f.id_factura, f.id_sucursal, f.id_cliente, f.nro_factura, f.fecha_hora, f.total, f.total_iva5, f.total_iva10, f.total_iva, f.total_letras, f.sucursal,
                              df.id_detalle, df.id_producto, df.cantidad_producto, df.subtotal
                              FROM factura f
                              LEFT JOIN detalle_factura df ON f.id_factura = df.id_factura";

                var factura = connection.Query<FacturaModel, DetalleFacturaModel, FacturaModel>(query, (factura, detalleFactura) =>
                {
                    if (!facturaDictionary.TryGetValue(factura.id_factura, out var facturaActual))
                    {
                        facturaActual = factura;
                        facturaActual.detalleFactura = new List<DetalleFacturaModel>();
                        facturaDictionary.Add(facturaActual.id_factura, facturaActual);
                    }

                    if (detalleFactura != null)
                    {
                        facturaActual.detalleFactura.Add(detalleFactura);
                    }

                    return facturaActual;
                }, splitOn: "id_detalle");

                return factura.Distinct();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacturaModel get(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT 
                                 f.id_factura, f.id_sucursal, f.id_cliente, f.nro_factura, f.fecha_hora, f.total, f.total_iva5, f.total_iva10, f.total_iva, f.total_letras, f.sucursal,
                                 df.id_detalle, df.id_producto, df.cantidad_producto, df.subtotal
                                 FROM factura f
                                 LEFT JOIN detalle_factura df ON f.id_factura = df.id_factura
                                 WHERE f.id_factura = @Id";

                var facturaDictionary = new Dictionary<int, FacturaModel>();

                var factura = connection.Query<FacturaModel, DetalleFacturaModel, FacturaModel>(query, (factura, detalleFactura) =>
                {
                    if (!facturaDictionary.TryGetValue(factura.id_factura, out var facturaActual))
                    {
                        facturaActual = factura;
                        facturaActual.detalleFactura = new List<DetalleFacturaModel>();
                        facturaDictionary.Add(facturaActual.id_factura, facturaActual);
                    }

                    if (detalleFactura != null)
                    {
                        facturaActual.detalleFactura.Add(detalleFactura);
                    }

                    return facturaActual;
                }, new { Id = id }, splitOn: "id_detalle").FirstOrDefault();

                return factura;
            }
        }
    }
}
