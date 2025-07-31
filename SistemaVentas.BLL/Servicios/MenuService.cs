using AutoMapper;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentas.BLL.Servicios
{
    public class MenuService : IMenuService
    {
        private readonly IGenericoRepository<Usuario> _usuarioRepositorio;
         private readonly IGenericoRepository<MenuRol> _menuRolRepositorio;
        private readonly IGenericoRepository<Menu> _menuRepositorio;
        private readonly IMapper _mapper;


        public MenuService(IGenericoRepository<Usuario> usuarioRepositorio, IGenericoRepository<MenuRol> menuRolRepositorio, IGenericoRepository<Menu> menuRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _menuRolRepositorio = menuRolRepositorio;
            _menuRepositorio = menuRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tbUsuario = await _usuarioRepositorio.Consultar( u => u.IdUsuario == idUsuario);
            IQueryable<Menu> tbMenu = await _menuRepositorio.Consultar();
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.Consultar();

            try
            {
                IQueryable<Menu> tbResultado = (from u in tbUsuario 
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();

                var listaMenus = tbResultado.ToList();

                return _mapper.Map<List<MenuDTO>>(listaMenus);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
