using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MP.DocumentDB.Interfaces;
using MP.DocumentDB.Models;
using System.Linq.Expressions;

namespace MP.DocumentDB
{
    public class DocumentCollection<T> : IDocumentCollection<T> where T : class
    {
        private readonly CollectionConfiguration<T> _configuration;
        private readonly IMongoCollection<T> _collection;

        public DocumentCollection(IOptions<CollectionConfiguration<T>> configuration)
        {
            _configuration = configuration.Value;

            var client = new MongoClient(_configuration.ConnectionString);
            var database = client.GetDatabase(_configuration.DatabaseName);

            _collection = database.GetCollection<T>(_configuration.CollectionName);
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> query)
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(query);
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> query)
        {
            return await _collection.AsQueryable().SingleAsync(query);
        }

        public async Task Insert(T item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task Insert(IEnumerable<T> items)
        {
            await _collection.InsertManyAsync(items);
        }
    }
}
