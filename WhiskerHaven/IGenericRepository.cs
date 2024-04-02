using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiskerHaven
{
    /// <summary>
    /// Define una interfaz generica para un repositorio que maneja entidades.
    /// </summary> 
    /// <typeparam name="T">El tipo de entidad que el repositorio manejará</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Obtiene una lista de todas las entidades en el repositorio
        /// </summary>
        /// <returns>Una lista de entidades</returns>
        Task<IEnumerable<TEntity>> ToListAsync();

        /// <summary>
        /// Busca una entidad por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Una entidad si es encontrada</returns>
        Task<TEntity> FindAsync(int id);

        /// <summary>
        /// Agrega una entidad al repositorio
        /// </summary>
        /// <param name="entity"></param>
        void AddAsync(TEntity entity);

        /// <summary>
        /// Actualiza 
        /// </summary>
        /// <param name="entity"></param>
        void UpdateAsync(int Id, TEntity entity);
        
        // Puede retornar un bool
        Task RemoveAsync(int id);

    }
}
