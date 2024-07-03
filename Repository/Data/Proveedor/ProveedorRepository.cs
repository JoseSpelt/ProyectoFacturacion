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

namespace Repository.Data.Proveedor
{
    public class ProveedorRepository : IProveedorRepository
    {
        IDbConnection connection;
        private string? connectionString;

        public ProveedorRepository(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new ConexionDB(connectionString).OpenConnection();
        }

        public bool add(ProveedorModel proveedorModel)
        {
            try
            {
                connection.Execute("INSERT INTO proveedor(razonsocial, tipodocumento, numerodocumento, direccion, mail, celular, estado) " +
                    $"Values(@razonSocial, @tipoDocumento, @numeroDocumento, @direccion, @mail, @celular, @estado)", proveedorModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProveedorModel> listar()
        {
            try
            {
                return connection.Query<ProveedorModel>("SELECT * FROM proveedor");
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
                connection.Execute("DELETE FROM proveedor WHERE id = @Id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update(ProveedorModel proveedorModel)
        {
            try
            {
                connection.Execute("UPDATE proveedor SET " +
                    " razonsocial=@razonSocial, " +
                    " tipodocumento=@tipoDocumento, " +
                    " numerodocumento=@numeroDocumento, " +
                    " direccion=@direccion, " +
                    " mail=@mail, " +
                    " celular=@celular, " +
                    " estado=@estado " +
                    $" WHERE id = @id", proveedorModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProveedorModel get(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM proveedor WHERE id = @Id";
                var proveedor = connection.QueryFirstOrDefault<ProveedorModel>(query, new { Id = id });

                return proveedor;
            }
        }
    }
}
