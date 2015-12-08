using System.Collections;
using System.Collections.Generic;

namespace RB.Umbraco.CleanUpManager.PagedList
{
    /// <summary>
    /// Interface IPagedList
    /// </summary>
    public interface IPagedList
    {
        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        int PageIndex { get; }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        int PageSize { get; }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <value>The total count.</value>
        int TotalCount { get; }

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <value>The list.</value>
        IList List { get; }
    }

    /// <summary>
    /// Interface IPagedList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IPagedList
    {
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <value>The list.</value>
        new IList<T> List { get; }
    }
}
