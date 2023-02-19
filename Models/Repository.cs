using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Models;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly ESMDbContext _context;
    public Repository(ESMDbContext context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerable<T>? Find(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public virtual T? Get(string id)
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerable<T>? GetAll()
    {
        throw new NotImplementedException();
    }

    public virtual void Remove(string id)
    {
        throw new NotImplementedException();
    }

    public virtual void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
