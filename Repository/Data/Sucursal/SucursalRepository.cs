using Dapper;
using examenOptativoP.Modelos;
using Npgsql;
using Repository.Data.ConfiguracionesDB;
using Repository.Data.Facturas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Sucursal
{
    public class SucursalRepository
    {
        IDbConnection connection;
        private string? connectionString;

        public SucursalRepository(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new ConexionDB(connectionString).OpenConnection();
        }

        public bool add(SucursalModel facturaModel)
        {
            try
            {
                connection.Execute("INSERT INTO sucursal(descripcion,direccion,telefono, whatsapp, email, estado) " +
                    $"Values(@descripcion,@direccion,@telefono, @whatsapp,@email, @estado)", facturaModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SucursalModel> listar()
        {
            try
            {
                return connection.Query<SucursalModel>("SELECT * FROM sucursal");
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
                connection.Execute("DELETE FROM sucursal WHERE id_sucursal = @Id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update(SucursalModel sucursalModel)
        {
            try
            {
                connection.Execute("UPDATE sucursal SET " +
                    " descripcion=@descripcion, " +
                    " direccion=@direccion, " +
                    " telefono=@telefono, " +
                    " whatsapp=@whatsapp, " +
                    " email=@email, " +
                    " estado=@estado, " +
                    $" WHERE  id_sucursal = @id_sucursal", sucursalModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SucursalModel get(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM sucursal WHERE id_sucursal = @Id";
                var sucursal = connection.QueryFirstOrDefault<SucursalModel>(query, new { Id = id });

                return sucursal;
            }
        }
    }
}
