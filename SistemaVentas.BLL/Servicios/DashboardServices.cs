using AutoMapper;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.BLL.Servicios
{
    public class DashboardServices : IDashboardService
    {
        private IVentaRepository _ventaRepository;
        private readonly IGenericoRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public DashboardServices(IVentaRepository ventaRepository,
            IGenericoRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Método que recibe un query editable de la tabla venta y un numero de días para restra
        /// Devuelve un query de la tabla venta con los filtros de fecha ya establecidos, por loq eu trae
        /// los registros que se encuentren despues de aplicar el filtro de días
        /// </summary>
        /// <param name="tablaVenta"></param>
        /// <param name="restarCantidadDias"></param>
        /// <returns></returns>
        private IQueryable<Venta> RetornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            //Obtenemos la ultima fecha de la tabla ventas
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).
                Select(v => v.FechaRegistro).First();

            //Restamos los días
            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            //Retornamos el query
            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        /// <summary>
        /// Retorna la cantidad de ventas que se han realizado
        /// </summary>
        /// <returns></returns>
        public async Task<int> TotalVentasSemana()
        {
            int totalVentas = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                //Ejecuta el metodo antes creado
                var tablaVenta = RetornarVentas(_ventaQuery, -7);
                totalVentas = tablaVenta.Count();
            }

            return totalVentas;
        }

        public async Task<string> TotalIngresosUltimaSemana()
        {
            decimal totalIngresos = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVentas(_ventaQuery, -7);

                totalIngresos = tablaVenta.Select(v => v.Total).Sum();

            }

            return Convert.ToString(totalIngresos, new CultureInfo("es-CR"));
        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> query = await _productoRepositorio.Consultar();

            int total = query.Count();

            return total;

        }

        /// <summary>
        /// Devuelve las ventas de las ultimas semanas, en un diccionario de datos
        /// que su clave es la fecha, por fecha agrupa las ventas y calcula el total
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();
            
            IQueryable<Venta> ventaQuery = await _ventaRepository.Consultar();

            if(ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVentas(ventaQuery, -7);

                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }

            return resultado;

        }

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashboardDTO = new DashboardDTO();

            try
            {
                vmDashboardDTO.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashboardDTO.TotalVentas = await TotalVentasSemana();
                vmDashboardDTO.TotalProductos = await TotalProductos();


                List<VentaSemanaDTO> listaVentaSemanaDTO = new List<VentaSemanaDTO>();

                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listaVentaSemanaDTO.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                 vmDashboardDTO.VentaUltimaSemana = listaVentaSemanaDTO; 
                return vmDashboardDTO;

            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}