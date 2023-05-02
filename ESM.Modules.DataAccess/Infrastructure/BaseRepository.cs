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
        protected ProductDTO GetProduct(string id)
        {
            if (id.StartsWith(DAStaticData.IdPrefix[ProductType.LAPTOP]))
            {
                return _context.Laptops.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.MONITOR]))
            {
                return _context.Monitors.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.HARDDISK]))
            {
                return _context.Pcharddisks.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.CPU]))
            {
                return _context.Pccpus.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.SMARTPHONE]))
            {
                return _context.Smartphones.First(x => x.Id == id);
            }
            else if (id.StartsWith(DAStaticData.IdPrefix[ProductType.VGA]))
            {
                return _context.Vgas.First(x => x.Id == id);
            }
            return _context.Pcs.First(x => x.Id == id);
        }
    }
}
