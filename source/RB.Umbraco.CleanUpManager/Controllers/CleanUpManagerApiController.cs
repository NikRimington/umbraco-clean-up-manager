using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RB.Umbraco.CleanUpManager.Services;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
using RB.Umbraco.CleanUpManager.Extensions;
using RB.Umbraco.CleanUpManager.Models;
using RB.Umbraco.CleanUpManager.PagedList;

namespace RB.Umbraco.CleanUpManager.Controllers
{
    /// <summary>
    /// Class CleanUpManagerController.
    /// </summary>
    [PluginController("RBCleanUpManager")]
    public class CleanUpManagerController : UmbracoAuthorizedApiController
    {
        #region Members
        /// <summary>
        /// The _clean up data service
        /// </summary>
        private readonly IDataTypesService _dataTypesService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CleanUpManagerController"/> class.
        /// </summary>
        public CleanUpManagerController()
        {
            _dataTypesService = new DataTypesService();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanUpManagerController"/> class.
        /// </summary>
        /// <param name="dataTypesService">The clean up data service.</param>
        public CleanUpManagerController(IDataTypesService dataTypesService)
        {
            _dataTypesService = dataTypesService;
        }
        #endregion

        #region Http End-Points        
        /// <summary>
        /// Gets the orphan data types.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        [HttpGet]
        //[PagedListActionFilter]
        public HttpResponseMessage GetOrphanDataTypes()
        {
            var results = _dataTypesService.GetOrphanDataTypes();
            var filteredResults = results;

            // Get page information for query string.
            var index = Request.GetPageIndex();
            var size = Request.GetPageSize();
            var filter = Request.GetFilter();
            IPagedList<CmsDataType> page = null;

            if (!string.IsNullOrEmpty(filter))
            {
                filteredResults = results.Where(x => x.PropertyEditorAlias.ToLower().Contains(filter.ToLower()) || x.DbType.ToLower().Contains(filter.ToLower())).ToList();
                //var returnedPages = filteredResults.Count / size;
                //page = filteredResults.TakePage(index <= returnedPages ? index : 1, size);
            }
            
            
            // Take the selected page.           
            
            index = index < 0 ? 0 : index;

            page = filteredResults.TakePage(index, size);    
            return Request.CreateResponse(HttpStatusCode.OK, page);
        }

        /// <summary>
        /// Deletes the orphan data types.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        public HttpResponseMessage DeleteOrphanDataTypes()
        {
            var result = _dataTypesService.DeleteOrphanDataTypes();

            return result ? Request.CreateResponse(HttpStatusCode.OK, true) :
                              Request.CreateResponse(HttpStatusCode.InternalServerError, false);
        }

        /// <summary>
        /// Deletes the type of the orphan data.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        public HttpResponseMessage DeleteOrphanDataType([FromBody]int nodeId)
        {
            var result = _dataTypesService.DeleteOrphanDataType(nodeId);
            return result ? Request.CreateResponse(HttpStatusCode.OK, true) :
                              Request.CreateResponse(HttpStatusCode.InternalServerError, false);
        }
        
        #endregion
    }
}