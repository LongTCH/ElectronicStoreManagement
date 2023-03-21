namespace ESM.Modules.DataAccess.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(string id);
        IEnumerable<T>? GetAll();
        void Add(T entity);
        void Delete(string id);
        void Update(T entity);
        bool Any(string id);
    }
}
