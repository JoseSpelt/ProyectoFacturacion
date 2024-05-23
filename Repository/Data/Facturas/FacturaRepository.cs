using Dapper;
using examenOptativoP.Modelos;
using Npgsql;
using Repository.Data.Clientes;
using Repository.Data.ConfiguracionesDB;
using Repository.Data.Sucursal;
using System;
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
                connection.Execute("INSERT INTO factura(id_sucursal,id_cliente,nro_factura, fecha_hora, total, total_iva5, total_iva10, total_iva, total_letras, sucursal) " +
                    $"Values(@id_sucursal,@id_cliente,@nro_factura, @fecha_hora,@total, @total_iva5, @total_iva10,@total_iva,@total_letras,@sucursal)", facturaModel);
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
                return connection.Query<FacturaModel>("SELECT * FROM factura");
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
                connection.Execute("DELETE FROM factura WHERE id_factura = @Id", new { Id = id });
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
                connection.Execute("UPDATE factura SET " +
                    " fecha_hora=@fecha_hora, " +
                    " total=@total, " +
                    " total_iva5=@total_iva5, " +
                    " total_iva10=@total_iva10, " +
                    " total_iva=@total_iva, " +
                    " total_letras=@total_letras, " +
                    " sucursal=@sucursal " +
                    $" WHERE  id_factura = @id_factura", facturaModel);
                return true;
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

                string query = "SELECT * FROM factura WHERE id_factura = @Id";
                var factura = connection.QueryFirstOrDefault<FacturaModel>(query, new { Id = id });

                return factura;
            }
        }
    }
}
