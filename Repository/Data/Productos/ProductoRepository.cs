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

namespace Repository.Data.Productos
{
    public class ProductoRepository : IProductoRepository
    {
        IDbConnection connection;
        private string? connectionString;

        public ProductoRepository(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new ConexionDB(connectionString).OpenConnection();
        }

        public bool add(ProductoModel productoModel)
        {
            try
            {
                connection.Execute("INSERT INTO producto(descripcion,cantidad_minima, cantidad_stock, precio_compra, precio_venta, categoria, marca, estado) " +
                    $"Values(@descripcion, @cantidad_minima, @cantidad_stock, @precio_compra, @precio_venta, @categoria, @marca,@estado)", productoModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProductoModel> listar()
        {
            try
            {
                return connection.Query<ProductoModel>("SELECT * FROM producto");
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
                connection.Execute("DELETE FROM producto WHERE id_prodcuto = @Id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update(ProductoModel productoModel)
        {
            try
            {
                connection.Execute("UPDATE producto SET " +
                    " descripcion=@descripcion, " +
                    " cantidad_minima=@cantidad_minima, " +
                    " cantidad_stock=@cantidad_stock, " +
                    " precio_compra=@precio_compra, " +
                    " precio_venta=@precio_venta, " +
                    " categoria=@categoria, " +
                    " marca=@marca, " +
                    " estado=@estado " +
                    $" WHERE  id_producto = @id_producto", productoModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProductoModel get(int id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM producto WHERE id_producto = @Id";
                var producto = connection.QueryFirstOrDefault<ProductoModel>(query, new { Id = id });

                return producto;
            }
        }
    }
}
