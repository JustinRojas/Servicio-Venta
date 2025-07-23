using SistemaVentas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.DAL.Repositorios.Contrato
{
    public interface IVentaRepository : IGenericoRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);
    }
}
