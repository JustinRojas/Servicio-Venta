﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.Model
{
    public partial class Menu
    {
        public int IdMenu { get; set; }
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public string Url { get; set; }

        public virtual ICollection<MenuRol> MenuRols { get; set; }
    }
}
