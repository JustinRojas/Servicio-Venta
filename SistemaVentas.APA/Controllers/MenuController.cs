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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

      
    }


   
}
