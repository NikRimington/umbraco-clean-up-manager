using Umbraco.Core;
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
        public IContentTypeService ContentTypeService
        {
            get { return ApplicationContext.Current.Services.ContentTypeService; }
        }
    }
}
