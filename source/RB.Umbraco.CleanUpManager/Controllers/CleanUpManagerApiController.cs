using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Umbraco.Web.Mvc;
using RB.Umbraco.CleanUpManager.Services;
using Umbraco.Web.WebApi;
using RB.Umbraco.CleanUpManager.Extensions;

namespace RB.Umbraco.CleanUpManager.Controllers
{
    /// <summary>
    /// Class CleanUpManagerApiController.
    /// </summary>
    [PluginController("RBCleanUpManager")]
    public class CleanUpManagerApiController : UmbracoAuthorizedApiController
    {
        #region Members
        /// <summary>
        /// The _data types service
        /// </summary>
        private readonly IDataTypesService _dataTypesService;
        /// <summary>
        /// The _content types service
        /// </summary>
        private readonly IContentTypesService _contentTypesService;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CleanUpManagerApiController"/> class.
        /// </summary>
        public CleanUpManagerApiController()
        {
            _dataTypesService = new DataTypesService();
            _contentTypesService = new ContentTypesService();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanUpManagerApiController"/> class.
        /// </summary>
        /// <param name="dataTypesService">The data types service.</param>
        /// <param name="contentTypesService">The content types service.</param>
        public CleanUpManagerApiController(IDataTypesService dataTypesService,
                                        IContentTypesService contentTypesService)
        {
            _dataTypesService = dataTypesService;
            _contentTypesService = contentTypesService;
        }

        #endregion

        #region Data Type Http End-Points
        /// <summary>
        /// Gets the orphan data types.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        [HttpGet]
        [AllowAnonymous]
        //[PagedListActionFilter]
        public HttpResponseMessage GetOrphanDataTypes()
        {
            var results = _dataTypesService.GetOrphanDataTypes();
            var filteredResults = results;

            // Get page information for query string.
            var index = Request.GetPageIndex();
            var size = Request.GetPageSize();
            var filter = Request.GetFilter();

            if (!string.IsNullOrEmpty(filter))
            {
                filteredResults = results.Where(x => x.PropertyEditorAlias.ToLower().Contains(filter.ToLower()) || x.DbType.ToLower().Contains(filter.ToLower())).ToList();
            }


            // Take the selected page.           

            index = index < 0 ? 0 : index;

            var page = filteredResults.TakePage(index, size);
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

        #region Content Type Http End-Points
        /// <summary>
        /// Gets the orphan content types.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        [HttpGet]
        //[PagedListActionFilter]
        [AllowAnonymous]
        public HttpResponseMessage GetOrphanContentTypes()
        {
            var results = _contentTypesService.GetOrphanContentTypes();
            var filteredResults = results;
            var index = Request.GetPageIndex();
            var size = Request.GetPageSize();
            var filter = Request.GetFilter();

            if (!string.IsNullOrEmpty(filter))
            {
                filteredResults = results.Where(x => x.Alias.ToLower().Contains(filter.ToLower()) || x.Description.ToLower().Contains(filter.ToLower())).ToList();
            }


            // Take the selected page.           

            index = index < 0 ? 0 : index;

            var page = filteredResults.TakePage(index, size);
            return Request.CreateResponse(HttpStatusCode.OK, page);
        }

        /// <summary>
        /// Deletes the orphan content types.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        public HttpResponseMessage DeleteOrphanContentTypes()
        {
            var result = _contentTypesService.DeleteOrphanContentTypes();

            return result ? Request.CreateResponse(HttpStatusCode.OK, true) :
                              Request.CreateResponse(HttpStatusCode.InternalServerError, false);
        }

        /// <summary>
        /// Deletes the type of the orphan content.
        /// </summary>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <returns>HttpResponseMessage.</returns>
        [HttpPost]
        public HttpResponseMessage DeleteOrphanContentType([FromBody]int contentTypeId)
        {
            var result = _contentTypesService.DeleteOrphanContentType(contentTypeId);
            return result ? Request.CreateResponse(HttpStatusCode.OK, true) :
                              Request.CreateResponse(HttpStatusCode.InternalServerError, false);
        }

        #endregion
    }
}