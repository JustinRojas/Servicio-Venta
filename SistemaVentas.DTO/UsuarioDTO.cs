using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Correo { get; set; }
        public int IdRol { get; set; }

        public string? RolDescripcion { get; set; }
        public string? Clave { get; set; }
        /// <summary>
        /// Acá se trabaja como entero para que sea más facil cambiar entre activo e inactivo
        /// ya que se va usar en un desplegable
        /// </summary>
        public int EsActivo { get; set; }
    }
}
