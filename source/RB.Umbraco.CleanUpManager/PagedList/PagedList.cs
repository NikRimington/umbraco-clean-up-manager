using System.Collections;
using System.Collections.Generic;

namespace RB.Umbraco.CleanUpManager.PagedList
{
    /// <summary>
    /// Class PagedList.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="totalCount">The total count.</param>
        public PagedList(
            IList<T> list,
            int pageIndex,
            int pageSize,
            int totalCount)
        {
            List = list;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount { get; }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <value>The list.</value>
        public IList<T> List { get; }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <value>The list.</value>
        IList IPagedList.List
        {
            get { return (IList)List; }
        }
    }
}
