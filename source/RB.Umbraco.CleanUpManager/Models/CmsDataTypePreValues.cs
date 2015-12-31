namespace RB.Umbraco.CleanUpManager.Models
{
    /// <summary>
    /// Class CmsDataTypePreValues.
    /// </summary>
    public class CmsDataTypePreValues
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the data type node identifier.
        /// </summary>
        /// <value>The data type node identifier.</value>
        public int DataTypeNodeId { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        public int SortOrder { get; set; }
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
        public string Alias { get; set; }
    }
}
