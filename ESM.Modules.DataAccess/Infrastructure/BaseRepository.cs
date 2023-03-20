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

        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(string id)
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


        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
