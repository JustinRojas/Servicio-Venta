using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using SistemaVentas.DTO;
using SistemaVentas.API.Utilidades;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.BLL.Servicios;
namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }


        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Guardar([FromBody] VentaDTO modelo)
        {
            var rps = new Response<VentaDTO>();

            try
            {
                rps.Status = true;
                rps.Value = await _ventaService.Registrar(modelo);
            }
            catch (Exception ex)
            {

                rps.Status = false;
                rps.Sms = ex.Message;
            }

            return Ok(rps);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            var rps = new Response<List<VentaDTO>>();

            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaInicio = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;

            try
            {
                rps.Status = true;
                rps.Value = await _ventaService.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin );
            }
            catch (Exception ex)
            {

                rps.Status = false;
                rps.Sms = ex.Message;
            }

            return Ok(rps);
        }

        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            var rps = new Response<List<ReporteDTO>>();

       
            try
            {
                rps.Status = true;
                rps.Value = await _ventaService.Reporte( fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {

                rps.Status = false;
                rps.Sms = ex.Message;
            }

            return Ok(rps);
        }
    }
}
