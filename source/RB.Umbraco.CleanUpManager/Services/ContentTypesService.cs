using System;
using RB.Umbraco.CleanUpManager.Wrappers;
using Umbraco.Core.Logging;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace RB.Umbraco.CleanUpManager.Services
{
    /// <summary>
    /// Class ContentTypesService.
    /// </summary>
    public class ContentTypesService : IContentTypesService
    {
        #region Members

        /// <summary>
        /// The _content service
        /// </summary>
        private readonly IUmbracoContentService _contentService;
        /// <summary>
        /// The _content type service
        /// </summary>
        private readonly IUmbracoContentTypeService _contentTypeService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTypesService"/> class.
        /// </summary>
        public ContentTypesService()
        {
            _contentService = new UmbracoContentServiceWrapper();
            _contentTypeService = new UmbracoContentTypeServiceWrapper();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTypesService"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        /// <param name="contentTypeService">The content type service.</param>
        public ContentTypesService(IUmbracoContentService contentService,
                                   IUmbracoContentTypeService contentTypeService)
        {
            _contentService = contentService;
            _contentTypeService = contentTypeService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the orphan content types.
        /// </summary>
        /// <returns>List&lt;IContentType&gt;.</returns>
        public virtual List<IContentType> GetOrphanContentTypes()
        {
            try
            {
                var contentTypes = _contentTypeService.GetAllContentTypes().ToList();

                var results = contentTypes.Where(HasNoInstanceOfItsOwn)
                                          .Where(IsNotAParentOfAnotherContentType)
                                          .Where(potentialResult => CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(contentTypes, potentialResult))
                                          .ToList();

                return results;
            }
            catch (Exception ex)
            {
                LogHelper.Error<ContentTypesService>("Error getting orphan data types.", ex);
                throw;
            }
        }


        /// <summary>
        /// Deletes the orphan content types.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DeleteOrphanContentTypes()
        {
            try
            {
                var orphanContentTypes = GetOrphanContentTypes();

                foreach (var orphanContentType in orphanContentTypes)
                {
                    _contentTypeService.Delete(orphanContentType);
                }

                if (orphanContentTypes.Count > 0)
                {
                    LogCleanseOperations(orphanContentTypes);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error<ContentTypesService>("Error getting orphan data types.", ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes the type of the orphan content.
        /// </summary>
        /// <param name="contentTypeId">The content type identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DeleteOrphanContentType(int contentTypeId)
        {
            try
            {
                var orphanContentType = GetOrphanContentTypes().FirstOrDefault(x => x.Id == contentTypeId);
                _contentTypeService.Delete(orphanContentType);

                if (orphanContentType != null)
                {
                    LogCleanseOperation(orphanContentType);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error<ContentTypesService>("Error getting orphan data types.", ex);
                throw;
            }
        }


        #endregion

        #region Protected Methods


        /// <summary>
        /// Checks if content type is not part of another content type composition.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected internal virtual bool CheckIfContentTypeIsNotPartOfAnotherContentTypeComposition(IEnumerable<IContentType> results, IContentType contentType)
        {
            return !results.Any(result => result.ContentTypeCompositionExists(contentType.Alias));
        }

        /// <summary>
        /// Determines whether [is not a parent of another content type] [the specified content type].
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns><c>true</c> if [is not a parent of another content type] [the specified content type]; otherwise, <c>false</c>.</returns>
        protected internal virtual bool IsNotAParentOfAnotherContentType(IContentType contentType)
        {
            return !_contentTypeService.GetContentTypeChildren(contentType.Id).Any();
        }

        /// <summary>
        /// Determines whether [has no instance of its own] [the specified content type].
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns><c>true</c> if [has no instance of its own] [the specified content type]; otherwise, <c>false</c>.</returns>
        protected internal virtual bool HasNoInstanceOfItsOwn(IContentType contentType)
        {
            return !_contentService.GetContentOfContentType(contentType.Id).Any();
        }

        /// <summary>
        /// Logs the cleanse operations.
        /// </summary>
        /// <param name="contentTypes">The content types.</param>
        /// <exception cref="System.ArgumentNullException">contentTypes</exception>
        protected internal virtual void LogCleanseOperations(List<IContentType> contentTypes)
        {
            if (contentTypes == null)
                throw new ArgumentNullException("contentTypes");

            try
            {
                foreach (var contentType in contentTypes)
                {
                    LogCleanseOperation(contentType);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<ContentTypesService>("Error logging cleanse operation", ex);
                throw;
            }
        }

        /// <summary>
        /// Logs the cleanse operation.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <exception cref="System.ArgumentNullException">contentType</exception>
        protected internal virtual void LogCleanseOperation(IContentType contentType)
        {
            if (contentType == null)
                throw new ArgumentNullException("contentType");

            try
            {
                LogHelper.Info<ContentTypesService>(string.Format("Successfully cleansed data type {0} - (id: {1})", contentType.Alias, contentType.Id));
            }
            catch (Exception ex)
            {
                LogHelper.Error<ContentTypesService>("Error logging cleanse operation", ex);
                throw;
            }
        }


        #endregion
    }
}
