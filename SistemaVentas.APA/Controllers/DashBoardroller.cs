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
    public class DashBoardroller : ControllerBase
    {

        private readonly IDashboardService _dashboardService;

        public DashBoardroller(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }


        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rps = new Response<DashboardDTO>();

            try
            {
                rps.Status = true;
                rps.Value = await _dashboardService.Resumen();
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
