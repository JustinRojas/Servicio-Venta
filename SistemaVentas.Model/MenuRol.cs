using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Model
{
    public partial class MenuRol
    {
        [Key]
        public int IdMenuRol { get; set; }
        public int IdMenu { get; set; }
        public int IdRol { get; set; }

        public virtual Menu IdMenuNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
    }
}
