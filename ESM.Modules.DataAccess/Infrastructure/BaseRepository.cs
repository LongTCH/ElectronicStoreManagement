using ESM.Modules.DataAccess.Models;

namespace ESM.Modules.DataAccess.Infrastructure
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ESMDbContext _context;

        protected BaseRepository(ESMDbContext context)
        {
            _context = context;
        }

        public virtual Task<object?> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<object?> AddList(IEnumerable<T> list)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> IsIdExist(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<object?> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>?> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T?> GetById(string id)
        {
            throw new NotImplementedException();
        }


        public virtual Task<object?> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
