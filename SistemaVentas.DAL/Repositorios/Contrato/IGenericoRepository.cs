using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace SistemaVentas.DAL.Repositorios.Contrato
{
    //Le indicamos que recibe un modelo, pero que este debe ser una clase
    public interface IGenericoRepository<TModel> where TModel : class
    {
        //Devuelve el modelo
        Task<TModel> Obtener(Expression<Func<TModel, bool>> filtro);

        Task<TModel> Crear (TModel model);

        Task<bool> Editar(TModel model);

        Task<bool> Eliminar(TModel model);

        //Este devuelve el query, o la consulta según el modelo. Esto devuelve la cosulta ya que se le puede agregar m{as
        //filtro o lo que sea necesario en la clase de implementación
        Task<IQueryable<TModel>> Consultar(Expression<Func<TModel, bool>> filtro = null);
    }
}
