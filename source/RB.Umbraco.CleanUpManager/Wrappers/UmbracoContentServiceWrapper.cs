using Umbraco.Core;
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
    }
}
