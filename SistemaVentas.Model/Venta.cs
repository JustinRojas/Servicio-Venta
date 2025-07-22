using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Model
{
    public partial class Venta
    {
        public int IdVenta { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoPago { get; set; }
        public decimal Total { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
