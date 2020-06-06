using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Trader.BLL.Services.Common
{
    public interface IGenericService<TDbElement, TDtoElement, TKey>
        where TDbElement : class
        where TDtoElement : class
    {
        // Read.
        IEnumerable<TDtoElement> GetAll();
        TDtoElement Get(TKey key);

        // Create/Update.
        TDtoElement AddOrUpdate(TDtoElement item);

        // Delete.
        void Remove(TKey key);

        // Filter.
        IEnumerable<TDtoElement> Where(Expression<Func<TDtoElement, bool>> predicate);
        TDtoElement Get(Expression<Func<TDtoElement, bool>> predicate);
    }
}
