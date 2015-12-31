

using System.Collections.Generic;
using RB.Umbraco.CleanUpManager.Models;

namespace RB.Umbraco.CleanUpManager.Services
{
    /// <summary>
    /// Interface IDataTypesService
    /// </summary>
    public interface IDataTypesService
    {
        /// <summary>
        /// Gets the orphan data types.
        /// </summary>
        /// <returns>List&lt;CmsDataType&gt;.</returns>
        List<CmsDataType> GetOrphanDataTypes();

        /// <summary>
        /// Deletes the orphan data types.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool DeleteOrphanDataTypes();

        /// <summary>
        /// Deletes the type of the orphan data.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool DeleteOrphanDataType(int nodeId);
    }
}