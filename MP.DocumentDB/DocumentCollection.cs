using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MP.DocumentDB.Interfaces;
using MP.DocumentDB.Models;

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

        public async Task<T> GetFirstOrDefault(Func<T, bool> query)
        {
            var result = await _collection.FindAsync(f => query(f));

            return await result.FirstOrDefaultAsync();
        }

        public async Task<T> GetSingle(Func<T, bool> query)
        {
            var result = await _collection.FindAsync(f => query(f));

            return await result.SingleAsync();
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
