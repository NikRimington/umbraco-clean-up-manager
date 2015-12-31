using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace RB.Umbraco.CleanUpManager.Wrappers
{

    /// <summary>
    /// Class UmbracoContentServiceWrapper.
    /// </summary>
    public class UmbracoContentServiceWrapper : IUmbracoContentService
    {

        /// <summary>
        /// Gets the content service.
        /// </summary>
        /// <value>The content service.</value>
        public IContentService ContentService
        {
            get { return ApplicationContext.Current.Services.ContentService; }
        }

        /// <summary>
        /// Gets the type of the content of content.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IEnumerable&lt;IContent&gt;.</returns>
        public IEnumerable<IContent> GetContentOfContentType(int id)
        {
            return ApplicationContext.Current.Services.ContentService.GetContentOfContentType(id);
        }
    }
}
