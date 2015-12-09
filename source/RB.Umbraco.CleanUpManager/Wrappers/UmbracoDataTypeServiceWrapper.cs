using Umbraco.Core;
using Umbraco.Core.Services;

namespace RB.Umbraco.CleanUpManager.Wrappers
{
    /// <summary>
    /// Class UmbracoDataTypeServiceWrapper.
    /// </summary>
    public class UmbracoDataTypeServiceWrapper : IUmbracoDataTypeService
    {
        /// <summary>
        /// Gets the data type service.
        /// </summary>
        /// <value>The data type service.</value>
        public IDataTypeService DataTypeService
        {
            get { return ApplicationContext.Current.Services.DataTypeService; }
        }
    }
}
