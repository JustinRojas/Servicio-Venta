using SistemaVentas.DAL.Repositorios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SistemaVentas.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace SistemaVentas.DAL.Repositorios
{
    public class GenericoRepository<TModel> : IGenericoRepository<TModel> where TModel : class
    {
        //Variable que vincula con el contexto
        private readonly DBContextSistemaVenta _dbContext;

        public GenericoRepository(DBContextSistemaVenta dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro)
        {
            try
            {
                TModel modelo = await _dbContext.Set<TModel>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //Crea un modelo en la bd, y lo devuelve después de guardar los cambios
        public async Task<TModel> Crear(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Editar(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Remove(model);
                _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //Acá es que se crea la consulta más no se ejecuta,sino, que se ejecuta donde lo llamen
        public async Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null)
        {
            try
            {
                IQueryable<TModel> queryModel = filtro == null? _dbContext.Set<TModel>(): _dbContext.Set<TModel>().Where(filtro);
                return queryModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
