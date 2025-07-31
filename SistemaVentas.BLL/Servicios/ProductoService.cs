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
    public class ProductoService : IProductoService
    {
        private readonly IGenericoRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        public ProductoService(IGenericoRepository<Producto> productoService, IMapper mapper)
        {
            _productoRepositorio = productoService;
            this._mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProductos = await _productoRepositorio.Consultar();
             
                    var listaProductos = queryProductos.Include( cat => cat.IdCategoriaNavigation ).ToList();

                return  _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());


            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepositorio.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("No se pudo crear elproducto");

              //  var queryProducto = await _productoRepositorio.Consultar( p => p.IdProducto == productoCreado.IdProducto);

              //productoCreado = queryProducto.Include(p => p.IdCategoriaNavigation).First();

                return _mapper.Map<ProductoDTO>(productoCreado);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var producto = _mapper.Map<Producto>(modelo);


                var productoEncontrado = await _productoRepositorio.Obtener(p => p.IdProducto == producto.IdProducto);

                if (productoEncontrado == null)
                    throw new TaskCanceledException("No se encontró el producto");

               productoEncontrado.Nombre = producto.Nombre;
                productoEncontrado.Precio = producto.Precio;    
               productoEncontrado.Stock = producto.Stock;
                productoEncontrado.EsActivo = producto.EsActivo;
                productoEncontrado.IdCategoria = producto.IdCategoria;

                var respuesta = await _productoRepositorio.Editar(productoEncontrado);
                
                if(!respuesta)
                    throw new TaskCanceledException("No se pudo editar el producto");

                return respuesta;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
           
            var productoEncontrado = await _productoRepositorio.Obtener(p => p.IdProducto == id);

            if (productoEncontrado == null) 
                throw new TaskCanceledException("No se encontró el producto");

            bool respuesta = await _productoRepositorio.Eliminar(productoEncontrado);

            if (!respuesta)
                throw new TaskCanceledException("No se pudo eliminar el producto");

            return respuesta;

        }


    }
}
