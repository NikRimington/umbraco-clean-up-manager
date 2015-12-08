﻿using System;
using System.Collections.Generic;
using RB.Umbraco.CleanUpManager.Extensions;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Web;

namespace RB.Umbraco.CleanUpManager.Wrappers
{
    /// <summary>
    /// Class Umbraco Db Wrapper.
    /// </summary>
    public class UmbracoDatabaseWrapper : IUmbracoDatabaseWrapper
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        public Database Db
        {
            get
            {
                return UmbracoContext.Current.Application.DatabaseContext.Database;                
            }
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>T.</returns>
        public T ExecuteScalar<T>(string sql, params object[] args)
        {
            try
            {
                Db.OpenSharedConnection();
                var results = Db.ExecuteScalar<T>(sql, args);
                return results;
            }
            catch (Exception ex)
            {
                LogHelper.Error<UmbracoDatabaseWrapper>(string.Format("Error Execute Scalar. SQL: {0};", sql), ex);
                return default(T);
            }
            finally
            {
                Db.CloseSharedConnection();
            }
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <returns>T.</returns>
        public T ExecuteScalar<T>(Sql sql)
        {
            try
            {
                return ExecuteScalar<T>(sql.SQL, sql.Arguments);
            }
            catch (Exception ex)
            {
                LogHelper.Error<UmbracoDatabaseWrapper>(string.Format("Error Execute Scalar. SQL: {0};", sql), ex);
                return default(T);
            }
            finally
            {
                Db.CloseSharedConnection();
            }
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> ExecuteReader<T>(string sql, params object[] args)
        {
            
            try
            {
                Db.OpenSharedConnection();    
                using (var command = Db.CreateCommand(Db.Connection, sql, args))
                {
                    var data = command.ExecuteReader();
                    var results = data.MapToList<T>();
                    return results;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<UmbracoDatabaseWrapper>(string.Format("Error Execute Reader. SQL: {0};", sql), ex);
                throw;
            }
            finally
            {
                Db.CloseSharedConnection();
            }
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> Delete<T>(string sql, params object[] args)
        {

            try
            {
                Db.OpenSharedConnection();
                using (var command = Db.CreateCommand(Db.Connection, sql, args))
                {
                    var data = command.ExecuteReader();
                    var results = data.MapToList<T>();
                    return results;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<UmbracoDatabaseWrapper>(string.Format("Error Execute Reader. SQL: {0};", sql), ex);
                throw;
            }
            finally
            {
                Db.CloseSharedConnection();
            }
        }



    }
}
