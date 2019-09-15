using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace LoansViewer.DAO
{
    public class MongoDbDao<T> : IMongoDao<T> where T: MongoDbEntity
    {
        private readonly IMongoDatabase _db;
        private readonly string _collectioname = typeof(T).Name;

        public MongoDbDao(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            _db = client.GetDatabase(connection.DatabaseName);
        }

        public async Task<T> Save(T entity)
        {
            await _db.GetCollection<T>(_collectioname).ReplaceOneAsync(
                filter: new BsonDocument(nameof(entity.Id), entity.Id),
                options: new UpdateOptions { IsUpsert = true },
                replacement: entity);
            
            return entity;
        }

        public async Task<T> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return await _db.GetCollection<T>(_collectioname).AsQueryable().Where(condition).FirstOrDefaultAsync();
        }
    }
}
