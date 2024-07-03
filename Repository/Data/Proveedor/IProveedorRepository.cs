using examenOptativoP.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Proveedor
{
    public interface IProveedorRepository
    {
        bool add(ProveedorModel proveedorModel);
        bool update(ProveedorModel proveedorModel);
        bool delete(int id);
        ProveedorModel get(int id);
        IEnumerable<ProveedorModel> listar();
    }
}
