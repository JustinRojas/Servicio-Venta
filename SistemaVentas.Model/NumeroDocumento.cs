using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Model
{
    public partial class NumeroDocumento
    {
        public int IdNumeroDocumento { get; set; }
        public int UltimoNumero { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}
