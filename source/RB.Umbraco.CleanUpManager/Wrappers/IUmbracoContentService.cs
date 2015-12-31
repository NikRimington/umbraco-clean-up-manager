using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace RB.Umbraco.CleanUpManager.Wrappers
{

    /// <summary>
    /// Interface IUmbracoContentService
    /// </summary>
    public interface IUmbracoContentService
    {
        /// <summary>
        /// Gets the content service.
        /// </summary>
        /// <value>The content service.</value>
        IContentService ContentService { get; }

        /// <summary>
        /// Gets the type of the content of content.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IEnumerable&lt;IContent&gt;.</returns>
        IEnumerable<IContent> GetContentOfContentType(int id);
    }
}
