using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class UsuarioService : IUsuarioService
    {
        //Para poder usar esto como es el que hace las consultas ala bd, se utiliza el repositorio de tipo usuario
        //el modelo ya que este tiene los mismos campos que la tabla en la BD
        private readonly IGenericoRepository<Usuario> _usuarioRepositorio;

        private readonly IMapper _mapper;

        public UsuarioService(IGenericoRepository<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }
        public async Task<List<UsuarioDTO>> Lista()
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar();
                //Se incluye que traiga la información del rol, id-descripcion, que son propiedades de usuarioDTO que también se mapean en la clase de utility
                var lisUsers = queryUsuario.Include(p => p.IdRolNavigation).ToList();
                return _mapper.Map<List<UsuarioDTO>>(lisUsers.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepositorio.Consultar(
                               p => p.Correo == correo &&
                               p.Clave == clave);

                if (queryUsuario.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("Usuario o contraseña no existen");
                }

                Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<SesionDTO>(devolverUsuario);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                //Como pasamos un usuarioDTO y el método ocupa un tipo usuario, se hace el mapeo dentro
                var usuarioCreado = await _usuarioRepositorio.Crear(_mapper.Map<Usuario>(modelo));

                if (usuarioCreado.IdUsuario == 0)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                //Ahora vamos a actualizar el usuarioCreado, para agregar el Rol, y tambíen para poder devolver la info

                //Creamos el query que trae el usuario
                var query = await _usuarioRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);

                //hacemos el filtro para incluir el rol en la consulta e igualamos al usuarioCreado
                //ya que se necesitan datos del rol aparte del Id
                usuarioCreado = query.Include(rol => rol.IdRol).First();

                //Devolvemos el usuarioDTO, por lo que se mapea el usuarioCreado
                return _mapper.Map<UsuarioDTO>(usuarioCreado);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                Usuario user = _mapper.Map<Usuario>(modelo);

                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == user.IdUsuario);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("No se pudo crear el usuario");

                usuarioEncontrado.IdUsuario = user.IdUsuario;
                usuarioEncontrado.NombreCompleto = user.NombreCompleto;
                usuarioEncontrado.Correo = user.Correo;
                usuarioEncontrado.IdRol = user.IdRol;
                //Nota siempre es importante hashear las claves.
                usuarioEncontrado.Clave = user.Clave;
                usuarioEncontrado.EsActivo = user.EsActivo;

                var respuesta = await _usuarioRepositorio.Editar(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar el usuario");


                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuarioRepositorio.Obtener(u => u.IdUsuario == id);

                if (usuarioEncontrado == null) throw new TaskCanceledException("No se encontró el usuario");

                var respuesta = await _usuarioRepositorio.Eliminar(usuarioEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar el usuario");

                return respuesta;
            }
            catch (Exception)
            {

                throw;
            }

        }




    }
}
