using System;
using System.Linq;
using System.Net.Http;

namespace RB.Umbraco.CleanUpManager.Extensions
{
    /// <summary>
    /// Class Request Extensions.
    /// </summary>
    public static class RequestExtensions
    {
        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <param name="defaultSize">The default size.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPageSize(this HttpRequestMessage requestMessage,int defaultSize = 5)
        {
            return GetIntFromQueryString(requestMessage, "pageSize", defaultSize);
        }

        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <param name="defaultIndex">The default index.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPageIndex(this HttpRequestMessage requestMessage,int defaultIndex = 0)
        {
            return GetIntFromQueryString(requestMessage, "pageIndex", defaultIndex);            
        }

        /// <summary>
        /// Gets the filter.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>System.String.</returns>
        public static string GetFilter(this HttpRequestMessage requestMessage, string filter = "")
        {
            return GetValueFromQueryString(requestMessage, "filter", filter);
        }

        /// <summary>
        /// Gets the int from query string.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetIntFromQueryString(
            this HttpRequestMessage requestMessage,
            string key,
            int defaultValue)
        {
            var pair = requestMessage
                .GetQueryNameValuePairs()
                .FirstOrDefault(p => p.Key.Equals(key,
                    StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(pair.Value))
            {
                int value;
                if (int.TryParse(pair.Value, out value))
                {
                    return value;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the value from query string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestMessage">The request message.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>T.</returns>
        public static T GetValueFromQueryString<T>(this HttpRequestMessage requestMessage,string key, T defaultValue = null) 
            where T: class
        {
            var pair = requestMessage
                .GetQueryNameValuePairs()
                .FirstOrDefault(p => p.Key.Equals(key,
                    StringComparison.InvariantCultureIgnoreCase));
            
            if (string.IsNullOrWhiteSpace(pair.Value))
                return defaultValue;

            return ConvertValue<T>(pair.Value) ?? defaultValue;
        }

        /// <summary>
        /// Converts the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T ConvertValue<T>(string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

    }
}
