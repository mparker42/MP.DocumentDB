namespace MP.DocumentDB.Interfaces
{
    internal interface IDocumentCollection<T> where T : class
    {
        Task<T> GetFirstOrDefault(Func<T, bool> query);
        Task<T> GetSingle(Func<T, bool> query);
        Task Insert(T item);
        Task Insert(IEnumerable<T> items);
    }
}
