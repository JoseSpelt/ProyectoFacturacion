using examenOptativoP.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Productos
{
    public interface IProductoRepository
    {
        bool add(ProductoModel productoModel);
        bool update(ProductoModel productoModel);
        bool delete(int id);
        ProductoModel get(int id);
        IEnumerable<ProductoModel> listar();
    }
}
