using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace RB.Umbraco.CleanUpManager.Wrappers
{
    /// <summary>
    /// Interface IUmbracoContentTypeService
    /// </summary>
    public interface IUmbracoContentTypeService
    {
        /// <summary>
        /// Gets the content type service.
        /// </summary>
        /// <value>The content type service.</value>
        IContentTypeService ContentTypeService { get; }

        /// <summary>
        /// Gets all content types.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>IEnumerable&lt;IContentType&gt;.</returns>
        IEnumerable<IContentType> GetAllContentTypes(params int[] ids);
        /// <summary>
        /// Gets the content type children.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IEnumerable&lt;IContentType&gt;.</returns>
        IEnumerable<IContentType> GetContentTypeChildren(int id);

        /// <summary>
        /// Deletes the specified content type.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="userId">The user identifier.</param>
        void Delete(IContentType contentType, int userId = 0);
    }
}
