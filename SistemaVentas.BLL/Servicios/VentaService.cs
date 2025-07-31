using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericoRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepositorio, IGenericoRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
    {
        try
        {
            var ventaGenerada = await _ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

            if (ventaGenerada.IdVenta == 0)
                throw new TaskCanceledException("No se pudo crear la venta");

            return _mapper.Map<VentaDTO>(ventaGenerada);
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
    {
        IQueryable<Venta> query = await _ventaRepositorio.Consultar();
            var ListaResultado = new List<Venta>();

            try
            {
                if(buscarPor == "fecha")
                {
                    DateTime fecha_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CR"));
                     DateTime fecha_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CR"));

                    //Acá usamos la consulta o query que se genera en la línea 49 para agregrale el filtro
                    //de fechas, adenás se incluye el detalle de esta venta en la línea 64 y según este detalle
                    //se trae los porductos con el thenInclude
                    ListaResultado = await query.Where(v => 
                    v.FechaRegistro.Value.Date >= fecha_Inicio.Date &&
                    v.FechaRegistro.Value.Date <= fecha_Fin.Date
                    ).Include( dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();

                }
                else
                {
                    ListaResultado = await query.Where(v =>v.NumeroDocumento == numeroVenta 
                   ).Include(dv => dv.DetalleVenta)
                   .ThenInclude(p => p.IdProductoNavigation)
                   .ToListAsync();
                }

                return _mapper.Map<List<VentaDTO>>(ListaResultado);
            }
            catch (Exception)
            {

                throw;
            }

    }



    public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
    {
            IQueryable<DetalleVenta> queryDetalle = await _detalleVentaRepositorio.Consultar();
            var ListaResultado = new List<DetalleVenta>();

            try
            {
                DateTime fecha_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CR"));
                DateTime fecha_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CR"));

                //Le decimos a la consulta que incluya traer la información de los productos dentro del detalle
                //y la información de la venta, para obtener la fecha de registro de esta y poder filtrar por fechas
                ListaResultado = await queryDetalle
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv =>
                    dv.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_Inicio.Date &&
                    dv.IdVentaNavigation.FechaRegistro.Value.Date <= fecha_Fin.Date)
                    .ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }

            return _mapper.Map<List<ReporteDTO>>(ListaResultado);
        }
    }
}
