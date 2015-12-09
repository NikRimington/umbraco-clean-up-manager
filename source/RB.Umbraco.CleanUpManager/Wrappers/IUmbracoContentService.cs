using Umbraco.Core.Services;

namespace RB.Umbraco.CleanUpManager.Wrappers
{
   
    public interface IUmbracoContentService
    {
       
        IContentService ContentService { get; }
    }
}
