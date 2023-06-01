namespace ESM.Modules.DataAccess.Infrastructure
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetById(string id);
        Task<IEnumerable<T>?> GetAll();
        Task<object?> Add(T entity); 
        Task<object?> AddList(IEnumerable<T> list);
        Task<object?> Delete(string id);
        Task<object?> Update(T entity);
        Task<bool> IsIdExist(string id);
        Task<string> GetSuggestID();
        
    }
    public interface IProductRepository<T> : IBaseRepository<T> where T: class
    {
        Task<IEnumerable<T>?> SearchProduct(string keyword);
    }
}
