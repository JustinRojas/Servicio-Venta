using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using SistemaVentas.Model;
using SistemaVentas.DAL.Repositorios.Contrato;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace SistemaVentas.DAL.Repositorios
{
    public class VentaRepository : GenericoRepository<Venta>, IVentaRepository
    {
        private readonly DBContextSistemaVenta _dbContext;

        //Como el GenericoRepository, también necesita el contexto, es de esta forma que se pasa 
        //con el  base (dbContext), se lo pasamos a la otra clase
        public VentaRepository(DBContextSistemaVenta dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {

            Venta ventaGenerada = new Venta();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //se actualiza el stock de los productos que se compran, es decir, que estan en el detalle de compra
                    foreach(DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto productoEncontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        productoEncontrado.Stock = productoEncontrado.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(productoEncontrado);
                    }

                    await _dbContext.SaveChangesAsync();

                    //Este es el numero que generamos para el documento de venta
                    NumeroDocumento correlativo = _dbContext.NumeroDocumentos.First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    _dbContext.NumeroDocumentos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


                    //Definimos el formato para el # de documento
                    int cantidadDigitos = 4;
                    //Le decimos que repita el 0, cuatro veces, la funcion Enumerable.Repeat() recibe lo que se va repetir y la cantidad
                    string ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos));

                    // Ejemplo: 00001
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    //queda así 0001
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - cantidadDigitos, cantidadDigitos);

                    //Actualizamos el campo del modelo y lo guardamos en la bd
                    modelo.NumeroDocumento = numeroVenta;
                   await  _dbContext.Ventas.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();


                    ventaGenerada = modelo;
                    transaction.Commit();

                    ;
                }
                catch (Exception)
                {

                    transaction.Rollback();
                    throw;
                }

                return ventaGenerada;
            }


        }
    }
}
