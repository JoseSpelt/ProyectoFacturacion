using examenOptativoP.Modelos;
using Repository.Data.Clientes;
using Repository.Data.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logica
{
    public class ProductoService
    {
        private ProductoRepository productoRepository;
        public ProductoService(string connectionString)
        {
            productoRepository = new ProductoRepository(connectionString);
        }

        public bool add(ProductoModel producto)
        {
            return validarDatos(producto) ? productoRepository.add(producto) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<ProductoModel> listar()
        {
            return productoRepository.listar();
        }

        public bool delete(int id)
        {
            return id > 0 ? productoRepository.delete(id) : false;
        }


        public bool update(ProductoModel productoModel)
        {
            return validarDatos(productoModel) ? productoRepository.update(productoModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(ProductoModel producto)
        {

            if (string.IsNullOrEmpty(producto.descripcion) ||
                string.IsNullOrEmpty(producto.categoria) ||
                string.IsNullOrEmpty(producto.marca) ||
                string.IsNullOrEmpty(producto.estado) ||
                producto.cantidad_minima == 0 || 
                producto.cantidad_stock == 0 || 
                producto.precio_compra == 0 || 
                producto.precio_venta == 0)
            {
                return false;
            }

            if (producto.cantidad_minima <= 1)
            {
                return false;
            }

            if (producto.precio_compra <= 0 || producto.precio_venta <= 0)
            {
                return false;
            }

            return true;
        }
        public ProductoModel get(int id)
        {
            return productoRepository.get(id);
        }
    }
}
