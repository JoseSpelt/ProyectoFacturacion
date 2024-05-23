using examenOptativoP.Modelos;
using Repository.Data.Facturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Sucursal
{
    public interface ISucursalRepository
    {
        bool add(SucursalModel sucursdalModel);
        bool update(SucursalModel sucursdalModel);
        bool delete(int id);
        SucursalModel get(int id);
        IEnumerable<SucursalModel> listar();
    }
}
