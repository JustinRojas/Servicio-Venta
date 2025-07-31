using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.BLL.Servicios.Contrato;

using SistemaVentas.DTO;
using SistemaVentas.API.Utilidades;
using SistemaVentas.BLL.Servicios.Contrato;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rps = new Response<List<UsuarioDTO>>();

            try
            {
                rps.Status = true;
                rps.Value = await _usuarioService.Lista();
            }
            catch (Exception ex)
            {

                rps.Status = false; 
                rps.Sms = ex.Message;
            }

            return Ok(rps); 
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            var rps = new Response<SesionDTO>();

            try
            {
                rps.Status = true;
                rps.Value = await _usuarioService.ValidarCredenciales(login.Correo, login.Clave);
            }
            catch (Exception ex)
            {

                rps.Status = false;
                rps.Sms = ex.Message;
            }

            return Ok(rps);
        }


        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
        {
            var rps = new Response<UsuarioDTO>();

            try
            {
                rps.Status = true;
                rps.Value = await _usuarioService.Crear(usuario);
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
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
        {
            var rps = new Response<bool>();

            try
            {
                rps.Status = true;
                rps.Value = await _usuarioService.Editar(usuario);
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
        public async Task<IActionResult> Eliminar(int Id)
        {
            var rps = new Response<bool>();

            try
            {
                rps.Status = true;
                rps.Value = await _usuarioService.Eliminar(Id);
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
