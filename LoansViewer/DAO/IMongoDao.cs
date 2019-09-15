using System;
using System.Threading.Tasks;

namespace LoansViewer.DAO
{
    /// <summary>
    /// Dao for mongo client
    /// </summary>
    /// <typeparam name="T">entity</typeparam>
    public interface IMongoDao<T> where T : MongoDbEntity
    {
        /// <summary>
        /// Save entity to database
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        Task<T> Save(T entity);

        /// <summary>
        /// retrieve data by custom condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns></returns>
        Task<T> GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition);
    }
}