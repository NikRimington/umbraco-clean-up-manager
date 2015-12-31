using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace RB.Umbraco.CleanUpManager.Wrappers
{
    /// <summary>
    /// Class UmbracoContentTypeServiceWrapper.
    /// </summary>
    public class UmbracoContentTypeServiceWrapper : IUmbracoContentTypeService
    {
        /// <summary>
        /// Gets the content type service.
        /// </summary>
        /// <value>The content type service.</value>
        public virtual IContentTypeService ContentTypeService
        {
            get { return ApplicationContext.Current.Services.ContentTypeService; }
        }

        /// <summary>
        /// Gets all content types.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>IEnumerable&lt;IContentType&gt;.</returns>
        public virtual IEnumerable<IContentType> GetAllContentTypes(params int[] ids)
        {
            return ApplicationContext.Current.Services.ContentTypeService.GetAllContentTypes(ids);
        }

        /// <summary>
        /// Gets the content type children.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IEnumerable&lt;IContentType&gt;.</returns>
        public virtual IEnumerable<IContentType> GetContentTypeChildren(int id)
        {
            return ApplicationContext.Current.Services.ContentTypeService.GetContentTypeChildren(id);
        }


        /// <summary>
        /// Deletes the specified content type.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="userId">The user identifier.</param>
        public virtual void Delete(IContentType contentType, int userId = 0)
        {
            ApplicationContext.Current.Services.ContentTypeService.Delete(contentType, userId);
        }
    }
}
