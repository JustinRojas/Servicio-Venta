using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SistemaVentas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DAL.Repositorios;
using SistemaVentas.DAL.Repositorios.Contrato;

using SistemaVentas.Utility;
using SistemaVentas.DTO;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.BLL.Servicios;
using SistemaVentas.DAL.DBContext;


namespace SistemaVentas.IOC
{
    //clase que va a tener el {Método de extensión}
    //acá se van a inyectar las dependencias
    public static class Dependencia
    {
        //Recibe un servicio de colecciones, el servicio de IServiceCollection ya lo trae nuestra app
        //lo que decimos con el this es que el metodo se ejecute dentro del servicio
        //Este método lo llamamos desde el API, y solo le pasamos la config
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            //CONFIGURAMOS LA CADENA DE CONEXION A ESTA CLASE DBContextSistemaVenta
            services.AddDbContext<DBContextSistemaVenta>( options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            services.AddTransient(typeof(IGenericoRepository<>), typeof(GenericoRepository<>));
            services.AddScoped<IVentaRepository , VentaRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));


            //Todas las dependencias que se crean en la biblioteca BLL 
            services.AddScoped<IRolService, RolService>();
             services.AddScoped<ICategoriaService, CategoriaService>();
             services.AddScoped<IDashboardService, DashboardServices>();
             services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IVentaService, VentaService>();




        }
    }
}
