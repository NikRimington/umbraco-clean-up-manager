using System.Collections.Generic;
using System.Linq;
using RB.Umbraco.CleanUpManager.PagedList;

namespace RB.Umbraco.CleanUpManager.Extensions
{
    /// <summary>
    /// Class ListExtensions.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// To the paged list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        /// <returns>IPagedList&lt;T&gt;.</returns>
        public static IPagedList<T> ToPagedList<T>(
            this IList<T> list,
            int pageIndex,
            int pageSize,
            int totalCount)
        {
            return new PagedList<T>(list, pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// Takes the page.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>IPagedList&lt;T&gt;.</returns>
        public static IPagedList<T> TakePage<T>(
            this IList<T> items,
            int pageIndex,
            int pageSize)
        {
            var collection = items
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToArray();
            return new PagedList<T>(collection, pageIndex, pageSize, items.Count);
        }
    }
}
