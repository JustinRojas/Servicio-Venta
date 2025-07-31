using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using AutoMapper;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.Model;

namespace SistemaVentas.BLL.Servicios
{
    public class RolService : IRolService
    {
        private readonly IGenericoRepository<Rol> _rolRepositorio;
        private readonly IMapper _mapper;

        public RolService(IGenericoRepository<Rol> rolRepositorio, IMapper mapper)
        {
            this._rolRepositorio = rolRepositorio;
            this._mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var listaRoles = await _rolRepositorio.Consultar();
                //Esto nos ayuda a convertir de rol que es lo que devuekve el método consultar
                //a una lista de ROLDTO, el método consultar devuelve un IQueryable, por lo que se debe hacer la conversión
                return _mapper.Map<List<RolDTO>>(listaRoles.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
