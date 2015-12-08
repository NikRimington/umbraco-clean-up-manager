using System.Collections.Generic;
using Umbraco.Core.Persistence;

namespace RB.Umbraco.CleanUpManager.Wrappers
{
    public interface IUmbracoDatabaseWrapper
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        Database Db { get; }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>T.</returns>
        T ExecuteScalar<T>(string sql, params object[] args);

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <returns>T.</returns>
        T ExecuteScalar<T>(Sql sql);

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>List&lt;T&gt;.</returns>
        List<T> ExecuteReader<T>(string sql, params object[] args);
    }
}