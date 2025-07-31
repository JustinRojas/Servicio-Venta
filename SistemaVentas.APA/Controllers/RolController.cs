using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.DTO;
using SistemaVentas.API.Utilidades;
using SistemaVentas.BLL.Servicios.Contrato;
using System.Reflection.Metadata.Ecma335;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rps = new Response<List<RolDTO>>();

            try
            {
                rps.Status = true;
                rps.Value = await _rolService.Lista();
            }
            catch (Exception ex)
            {
                rps.Status=false;
                rps.Sms = ex.Message;

            }

            return Ok(rps);
        }
       
    }
}
