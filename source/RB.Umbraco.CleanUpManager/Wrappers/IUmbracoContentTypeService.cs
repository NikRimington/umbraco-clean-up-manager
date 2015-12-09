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
    }
}
