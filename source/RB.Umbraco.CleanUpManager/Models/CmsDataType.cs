
namespace RB.Umbraco.CleanUpManager.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CmsDataType
    {
        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        /// <value>
        /// The node identifier.
        /// </value>
        public int NodeId { get; set; }
        /// <summary>
        /// Gets or sets the pk.
        /// </summary>
        /// <value>
        /// The pk.
        /// </value>
        public int Pk { get; set; }
        /// <summary>
        /// Gets or sets the type of the database.
        /// </summary>
        /// <value>
        /// The type of the database.
        /// </value>
        public string DbType { get; set; }
        /// <summary>
        /// Gets or sets the property editor alias.
        /// </summary>
        /// <value>
        /// The property editor alias.
        /// </value>
        public string PropertyEditorAlias { get; set; } 
    }
}
