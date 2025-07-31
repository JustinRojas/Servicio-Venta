using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVentas.DTO;
using SistemaVentas.Model;

namespace SistemaVentas.Utility
{
    /// <summary>
    /// Clase que sirve para convertir Modelos a DTO y viceversa
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            #region Rol
            //Se mapea de esta forma, primero la clase origen y luego la clase destino
            CreateMap<Rol, RolDTO>().ReverseMap();


            #endregion Rol

            #region Menu
            CreateMap<Menu, Menu>().ReverseMap();
            #endregion Menu


            #region Usuario
            //Cuando tenemos propiedades con diferente nombre en las clases, pero se refiere a lo mismo
            //se trabaja así
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                destino.RolDescripcion,//para esta propiedad en el destino
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre))
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));

            CreateMap<Usuario, SesionDTO>().
                ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre));

            // le decimos que ignore la propiedad de la clase destino Rol, para que no epere un valor
            //ya que la clase origen no la tiene
            CreateMap<UsuarioDTO, Usuario>()
                 .ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore())
                 .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)); ;

            #endregion Usuario

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion Categoria

            #region Producto
            CreateMap<Producto, ProductoDTO>()
                .ForMember(destino =>
                destino.DescripcionCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio, new CultureInfo("es-CR")))
                )
                  .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)); ;

            CreateMap<ProductoDTO, Producto>()
               .ForMember(destino =>
               destino.IdCategoriaNavigation,
               opt => opt.Ignore()
               )
               .ForMember(destino =>
               destino.Precio,
               opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CR")))
               )
                 .ForMember(destino =>
               destino.EsActivo,
               opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)); ;
            #endregion Producto



            #region Venta
            //Se mapea de esta forma, primero la clase origen y luego la clase destino
            CreateMap<Venta, VentaDTO>()
                .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.ToString(), new CultureInfo("es-CR")))
                )
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<VentaDTO, Venta>()
               .ForMember(destino =>
               destino.Total,
               opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CR")))
               )
               ;
            #endregion Venta

            #region DetalleVenta

            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino =>
               destino.DescripcionProducto,
               opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
               )
              .ForMember(destino =>
               destino.PrecioTexto,
               opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.ToString(), new CultureInfo("es-CR"))))
                .ForMember(destino =>
               destino.TotaTexto,
               opt => opt.MapFrom(origen => Convert.ToString(origen.Total.ToString(), new CultureInfo("es-CR"))))
               ;


            CreateMap<DetalleVentaDTO, DetalleVenta>()
              .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-CR")))
                )
               .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotaTexto, new CultureInfo("es-CR")))
                )
              ;

            #endregion DetalleVenta

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                 .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
                )
                 .ForMember(destino =>
               destino.NumeroDocumento,
               opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
               )
                  .ForMember(destino =>
               destino.TipoPago,
               opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
               )
                   .ForMember(destino =>
               destino.TotalVenta,
               opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total, new CultureInfo("es-CR")))
               )
               .ForMember(destino =>
               destino.Producto,
               opt => opt.MapFrom(origen => Convert.ToString(origen.IdProductoNavigation.Nombre))
               )
               .ForMember(destino =>
               destino.Precio,
               opt => opt.MapFrom(origen => Convert.ToString(origen.Precio, new CultureInfo("es-CR")))
               )
               .ForMember(destino =>
               destino.Total,
               opt => opt.MapFrom(origen => Convert.ToString(origen.Total, new CultureInfo("es-CR")))
               )
              ;
            #endregion Reporte

        }
    }
}
