using System.Collections.Generic;
using Umbraco.Core.Models;

namespace RB.Umbraco.CleanUpManager.Services
{
    /// <summary>
    /// Interface IContentTypesService
    /// </summary>
    public interface IContentTypesService
    {
        /// <summary>
        /// Gets the orphan content types.
        /// </summary>
        /// <returns>List&lt;IContentType&gt;.</returns>
        List<IContentType> GetOrphanContentTypes();

        /// <summary>
        /// Deletes the orphan content types.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool DeleteOrphanContentTypes();

        /// <summary>
        /// Deletes the type of the orphan content.
        /// </summary>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool DeleteOrphanContentType(int contentTypeId);
    }
}