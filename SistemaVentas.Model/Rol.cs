using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Model
{
    public partial class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<MenuRol> MenuRols { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
