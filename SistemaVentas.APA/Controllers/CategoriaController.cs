using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using SistemaVentas.BLL.Servicios;
using SistemaVentas.DTO;
using SistemaVentas.API.Utilidades;
using SistemaVentas.BLL.Servicios.Contrato;


namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            this._categoriaService = categoriaService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Listar()
        {
            var rps = new Response<List<CategoriaDTO>>();
            try
            {
                rps.Status = true;
                rps.Value = await _categoriaService.Lista();
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
