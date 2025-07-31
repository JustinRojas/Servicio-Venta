using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.DTO
{
    public class DashboardDTO
    {
        public int TotalVentas { get; set; }
        public string? TotalIngresos { get; set; }
        public int TotalProductos { get; set; }
        /// <summary>
        /// Va a guardar las ventas de la ultima semana, creando una lista de VentaSemana
        /// </summary>
        public List<VentaSemanaDTO> VentaUltimaSemana { get; set; }

    }
}
