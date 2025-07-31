using SistemaVentas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.BLL.Servicios.Contrato
{
    public interface IVentaService
    {
        Task<VentaDTO> Registrar(VentaDTO modelo);
        /// <summary>
        /// Método que devuelve un historial de ventas haciendo un filto por fechas
        /// </summary>
        /// <param name="buscarPor"></param>
        /// <param name="numeroVenta"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);
        /// <summary>
        /// Devuelve un reporte de ventas haciendo un filtro por fechas
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin);


    }
}
