using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.DTO;
using SistemaVentas.API.Utilidades;
using SistemaVentas.BLL.Servicios.Contrato;


namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rps = new Response<List<ProductoDTO>>();

            try
            {
                rps.Status = true;
                rps.Value = await _productoService.Lista();
            }
            catch (Exception ex)
            {

                rps.Status=false;
                rps.Sms=ex.Message; 
            }

            return Ok(rps);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ProductoDTO modelo)
        {
            var rps = new Response<ProductoDTO>();

            try
            {
                rps.Status = true;
                rps.Value = await _productoService.Crear(modelo);
            }
            catch (Exception ex)
            {

                rps.Status = false;
                rps.Sms = ex.Message;
            }

            return Ok(rps);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ProductoDTO modelo)
        {
            var rps = new Response<bool>();

            try
            {
                rps.Status = true;
                rps.Value = await _productoService.Editar(modelo);
            }
            catch (Exception ex)
            {

                rps.Status = false;
                rps.Sms = ex.Message;
            }

            return Ok(rps);
        }

        [HttpPut]
        [Route("Eliminar/{Id:int}")]
        public async Task<IActionResult> Editar(int Id)
        {
            var rps = new Response<bool>();

            try
            {
                rps.Status = true;
                rps.Value = await _productoService.Eliminar(Id);
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
