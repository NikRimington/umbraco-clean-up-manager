using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RB.Umbraco.CleanUpManager.Extensions
{
    /// <summary>
    /// Class SqlDataReaderExtensions.
    /// </summary>
    public static class SqlDataReaderExtensions
    {
        /// <summary>
        /// Maps to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr">The dr.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public static List<T> MapToList<T>(this IDataReader dr)
        {
            var results = new List<T>();
            while (dr.Read())
            {
                var outputType = Activator.CreateInstance<T>();
                foreach (var property in outputType.GetType().GetProperties()
                      .Where(property => !Equals(dr[property.Name], DBNull.Value)))
                {
                    property.SetValue(outputType, dr[property.Name], null);
                }
                results.Add(outputType);
            }
            return results;
        }
    }
}
