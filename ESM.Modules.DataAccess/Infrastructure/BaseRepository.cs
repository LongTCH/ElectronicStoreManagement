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

        public virtual object? Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool Any(string id)
        {
            throw new NotImplementedException();
        }

        public virtual object? Delete(string id)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T>? GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual T? GetById(string id)
        {
            throw new NotImplementedException();
        }


        public virtual object? Update(T entity)
        {
            throw new NotImplementedException();
        }
        
    }
}
