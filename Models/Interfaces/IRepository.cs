using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Models.Interfaces;

public interface IRepository<T> where T : class
{
    abstract T? Get(string id);
    abstract IEnumerable<T>? GetAll();
    abstract IEnumerable<T>? Find(Expression<Func<T, bool>> predicate);
    abstract void Add(T entity);
    abstract void Remove(string id);
    abstract void Update(T entity);
}
