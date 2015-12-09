using Umbraco.Core.Services;

namespace RB.Umbraco.CleanUpManager.Wrappers
{
    /// <summary>
    /// Interface IUmbracoDataTypeService
    /// </summary>
    public interface IUmbracoDataTypeService
    {
        /// <summary>
        /// Gets the data type service.
        /// </summary>
        /// <value>The data type service.</value>
        IDataTypeService DataTypeService { get; }
    }
}
