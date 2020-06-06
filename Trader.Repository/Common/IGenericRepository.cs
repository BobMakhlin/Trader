using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Trader.Repository.Common
{
    public interface IGenericRepository<TElement, TKey>
        where TElement : class
    {
        // Read.
        IEnumerable<TElement> GetAll();
        TElement Get(TKey key);

        // Create/Update.
        void AddOrUpdate(TElement item);

        // Delete.
        void Remove(TKey key);

        // Filter.
        IEnumerable<TElement> Where(Expression<Func<TElement, bool>> predicate);
        TElement Get(Expression<Func<TElement, bool>> predicate);

        // Save.
        void Save();
    }
}
