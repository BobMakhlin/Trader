using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Trader.BLL.Services.Common;

namespace Trader.BLL.Services.Extensions
{
    /// <summary>
    /// The type that contains async extension methods for interface IGenericService.
    /// </summary>
    public static class AsyncServices
    {
        /// <summary>
        /// Get all elements of the table asynchronously.
        /// </summary>
        public async static Task<IEnumerable<TDtoElement>> GetAllAsync<TDbElement, TDtoElement, TKey>
        (
            this IGenericService<TDbElement, TDtoElement, TKey> service
        )
            where TDbElement : class
            where TDtoElement : class
        {
            return await Task.Run(service.GetAll);
        }
        /// <summary>
        /// Get the table element by key asynchronously.
        /// </summary>
        public async static Task<TDtoElement> GetAsync<TDbElement, TDtoElement, TKey>
        (
            this IGenericService<TDbElement, TDtoElement, TKey> service, 
            TKey key
        )
            where TDbElement : class
            where TDtoElement : class
        {
            return await Task.Run(() => service.Get(key));
        }

        /// <summary>
        /// Add new item in the table or update the existing one.
        /// These operations work asynchronously.
        /// </summary>
        public async static Task<TDtoElement> AddOrUpdateAsync<TDbElement, TDtoElement, TKey>
        (
            this IGenericService<TDbElement, TDtoElement, TKey> service,
            TDtoElement item
        )
            where TDbElement : class
            where TDtoElement : class
        {
            return await Task.Run(() => service.AddOrUpdate(item));
        }

        /// <summary>
        /// Remove the item of the table by key asynchronously.
        /// </summary>
        public async static Task RemoveAsync<TDbElement, TDtoElement, TKey>
        (
            this IGenericService<TDbElement, TDtoElement, TKey> service,
            TKey key
        )
            where TDbElement : class
            where TDtoElement : class
        {
            await Task.Run(() => service.Remove(key));
        }

        /// <summary>
        /// Find the items of the table by predicate asynchronously.
        /// </summary>
        public static async Task<IEnumerable<TDtoElement>> WhereAsync<TDbElement, TDtoElement, TKey>
        (
            this IGenericService<TDbElement, TDtoElement, TKey> service,
            Expression<Func<TDtoElement, bool>> predicate
        )
            where TDbElement : class
            where TDtoElement : class
        {
            return await Task.Run(() => service.Where(predicate));
        }
        /// <summary>
        /// Find the first item of the table that satisfies a condition asynchronously.
        /// </summary>
        public static async Task<TDtoElement> GetAsync<TDbElement, TDtoElement, TKey>
        (
            this IGenericService<TDbElement, TDtoElement, TKey> service,
            Expression<Func<TDtoElement, bool>> predicate
        )
            where TDbElement : class
            where TDtoElement : class
        {
            return await Task.Run(() => service.Get(predicate));
        }
    }
}
