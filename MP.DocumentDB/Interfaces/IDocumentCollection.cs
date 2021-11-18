using System.Linq.Expressions;

namespace MP.DocumentDB.Interfaces
{
    public interface IDocumentCollection<T> where T : class
    {
        Task<T> GetFirstOrDefault(Expression<Func<T, bool>> query);
        Task<T> GetSingle(Expression<Func<T, bool>> query);
        Task Insert(T item);
        Task Insert(IEnumerable<T> items);
    }
}
