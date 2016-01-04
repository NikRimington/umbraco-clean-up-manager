using System;
using RB.Umbraco.CleanUpManager.Models;
using RB.Umbraco.CleanUpManager.Wrappers;
using Umbraco.Core.Logging;
using System.Collections.Generic;
using System.Linq;

namespace RB.Umbraco.CleanUpManager.Services
{
    /// <summary>
    /// Class DataTypesService.
    /// </summary>
    public class DataTypesService : IDataTypesService
    {
        #region Members
        /// <summary>
        /// The _DB
        /// </summary>
        private readonly IUmbracoDatabaseWrapper _db;

        /// <summary>
        /// The select orphan data types
        /// </summary>
        private const string SelectOrphanDataTypes =
            "SELECT DISTINCT dt.nodeId, dt.pk, dt.dbType, dt.propertyEditorAlias, un.text AS name " +
            "FROM dbo.cmsDataType AS dt INNER JOIN " +
            "dbo.umbracoNode AS un ON dt.nodeId = un.id " +
            "WHERE (dt.nodeId NOT IN (SELECT dataTypeId FROM dbo.cmsPropertyType AS pt WHERE (dataTypeId = dt.nodeId)))";

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTypesService"/> class.
        /// </summary>
        public DataTypesService()
        {
            _db = new UmbracoDatabaseWrapper();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTypesService"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public DataTypesService(IUmbracoDatabaseWrapper db)
        {
            _db = db;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the orphan data types.
        /// </summary>
        /// <returns>IQueryable&lt;CmsDataType&gt;.</returns>
        public virtual List<CmsDataType> GetOrphanDataTypes()
        {
            try
            {
                var result = _db.ExecuteReader<CmsDataType>(SelectOrphanDataTypes);
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error getting orphan data types.", ex);
                return null;
            }
        }


        /// <summary>
        /// Deletes up orphan data types.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public virtual bool DeleteOrphanDataTypes()
        {
            try
            {
                var orphanDataTypes = GetOrphanDataTypes();
                CleanUpPropertyTypeTable(orphanDataTypes);
                CleanUpDataTypePreValuesTable(orphanDataTypes);
                CleanUpDataTypeTable(orphanDataTypes);
                CleanUpUmbracoNodeTable(orphanDataTypes);

                LogCleanseOperations(orphanDataTypes);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error getting orphan data types.", ex);
                return false;
            }
        }

        /// <summary>
        /// Deletes the type of up orphan data.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool DeleteOrphanDataType(int nodeId)
        {
            try
            {
                var dataType = _db.ExecuteReader<CmsDataType>(SelectOrphanDataTypes).FirstOrDefault(x => x.NodeId == nodeId);

                if (dataType == null)
                    return true;

                CleanUpPropertyTypeTable(dataType);
                CleanUpDataTypePreValuesTable(dataType);
                CleanUpDataTypeTable(dataType);
                CleanUpUmbracoNodeTable(dataType);

                LogCleanseOperation(dataType);

                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error getting orphan data types.", ex);
                return false;
            }
        }


        #endregion

        #region Protected Methods

        #region Delete Multiple Records At The Same Time

        /// <summary>
        /// Cleans up property type table.
        /// </summary>
        /// <param name="orphanDataTypes">The orphan data types.</param>
        /// <exception cref="System.ArgumentNullException">orphanDataTypes</exception>
        protected internal virtual void CleanUpPropertyTypeTable(List<CmsDataType> orphanDataTypes)
        {
            try
            {
                if (orphanDataTypes == null)
                    throw new ArgumentNullException("orphanDataTypes");


                var ids = string.Join(",", orphanDataTypes.Select(x => x.NodeId));
                _db.Delete<CmsPropertyType>("WHERE DataTypeId IN (" + ids + ")");

            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error cleaning Up data types", ex);
                throw;
            }

        }
        /// <summary>
        /// Cleans up data type pre values table.
        /// </summary>
        /// <param name="orphanDataTypes">The orphan data types.</param>
        /// <exception cref="System.ArgumentNullException">orphanDataTypes</exception>
        protected internal virtual void CleanUpDataTypePreValuesTable(List<CmsDataType> orphanDataTypes)
        {
            try
            {
                if (orphanDataTypes == null)
                    throw new ArgumentNullException("orphanDataTypes");


                var ids = string.Join(",", orphanDataTypes.Select(x => x.NodeId));
                _db.Delete<CmsDataTypePreValues>("WHERE DataTypeNodeId IN (" + ids + ")");

            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error cleaning Up data types", ex);
                throw;
            }
        }

        /// <summary>
        /// Cleans up data type table.
        /// </summary>
        /// <param name="dataTypeIds">The data type ids.</param>
        /// <exception cref="System.ArgumentNullException">dataTypeIds</exception>
        protected internal virtual void CleanUpDataTypeTable(List<CmsDataType> dataTypeIds)
        {
            try
            {
                if (dataTypeIds == null)
                    throw new ArgumentNullException("dataTypeIds");


                var ids = string.Join(",", dataTypeIds.Select(x => x.NodeId));
                _db.Delete<CmsDataType>("WHERE nodeId IN (" + ids + ")");

            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error cleaning Up data types", ex);
                throw;
            }

        }

        /// <summary>
        /// Cleans up umbraco node table.
        /// </summary>
        /// <param name="dataTypeIds">The data type ids.</param>
        /// <exception cref="System.ArgumentNullException">dataTypeIds</exception>
        protected internal virtual void CleanUpUmbracoNodeTable(List<CmsDataType> dataTypeIds)
        {
            try
            {
                if (dataTypeIds == null)
                    throw new ArgumentNullException("dataTypeIds");


                var ids = string.Join(",", dataTypeIds.Select(x => x.NodeId));
                _db.Delete<UmbracoNode>("WHERE Id IN (" + ids + ")");

            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error cleaning Up data types", ex);
                throw;
            }

        }

        #endregion

        #region Delete Single Record Only

        /// <summary>
        /// Cleans up data type table.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <exception cref="System.ArgumentNullException">dataType</exception>
        protected internal virtual void CleanUpDataTypeTable(CmsDataType dataType)
        {
            try
            {
                if (dataType == null)
                    throw new ArgumentNullException("dataType");

                _db.Delete<CmsDataType>("WHERE nodeId = " + dataType.NodeId);
            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error cleaning Up data types", ex);
                throw;
            }
        }

        /// <summary>
        /// Cleans up data type pre values table.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <exception cref="System.ArgumentNullException">dataType</exception>
        protected internal virtual void CleanUpDataTypePreValuesTable(CmsDataType dataType)
        {
            try
            {
                if (dataType == null)
                    throw new ArgumentNullException("dataType");

                _db.Delete<CmsDataTypePreValues>("WHERE DataTypeNodeId = " + dataType.NodeId);

            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error cleaning Up data types", ex);
                throw;
            }
        }

        /// <summary>
        /// Cleans up property type table.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <exception cref="System.ArgumentNullException">dataType</exception>
        protected internal virtual void CleanUpPropertyTypeTable(CmsDataType dataType)
        {
            try
            {
                if (dataType == null)
                    throw new ArgumentNullException("dataType");

                _db.Delete<CmsPropertyType>("WHERE DataTypeId = " + dataType.NodeId);

            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error cleaning Up data types", ex);
                throw;
            }
        }

        /// <summary>
        /// Cleans up umbraco node table.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <exception cref="System.ArgumentNullException">dataType</exception>
        protected internal virtual void CleanUpUmbracoNodeTable(CmsDataType dataType)
        {
            try
            {
                if (dataType == null)
                    throw new ArgumentNullException("dataType");

                _db.Delete<UmbracoNode>("WHERE Id = " + dataType.NodeId);

            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error cleaning Up data types", ex);
                throw;
            }
        }

        #endregion

        #region Logging Methods

        /// <summary>
        /// Logs the cleanse operations.
        /// </summary>
        /// <param name="dataTypes">The data types.</param>
        /// <exception cref="System.ArgumentNullException">dataTypes</exception>
        protected internal virtual void LogCleanseOperations(List<CmsDataType> dataTypes)
        {
            if (dataTypes == null)
                throw new ArgumentNullException("dataTypes");

            try
            {
                foreach (var dataType in dataTypes)
                {
                    LogCleanseOperation(dataType);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error logging cleanse operation", ex);
                throw;
            }
        }

        /// <summary>
        /// Logs the cleanse operation.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <exception cref="System.ArgumentNullException">dataType</exception>
        protected internal virtual void LogCleanseOperation(CmsDataType dataType)
        {
            if (dataType == null)
                throw new ArgumentNullException("dataType");

            try
            {
                LogHelper.Info<DataTypesService>(string.Format("Successfully cleansed data type {0} - (id: {1})",
                                                   dataType.PropertyEditorAlias, dataType.NodeId));
            }
            catch (Exception ex)
            {
                LogHelper.Error<DataTypesService>("Error logging cleanse operation", ex);
                throw;
            }
        }

        #endregion

        #endregion

    }
}
