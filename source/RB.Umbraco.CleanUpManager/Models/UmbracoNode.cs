using System;

namespace RB.Umbraco.CleanUpManager.Models
{
    /// <summary>
    /// Class UmbracoNode.
    /// </summary>
    public class UmbracoNode
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UmbracoNode"/> is trashed.
        /// </summary>
        /// <value><c>true</c> if trashed; otherwise, <c>false</c>.</value>
        public bool Trashed { get; set; }
        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        public int ParentId { get; set; }
        /// <summary>
        /// Gets or sets the node user.
        /// </summary>
        /// <value>The node user.</value>
        public int NodeUser { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public int Level { get; set; }
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        public int SortOrder { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>The unique identifier.</value>
        public Guid UniqueId { get; set; }
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the type of the node object.
        /// </summary>
        /// <value>The type of the node object.</value>
        public Guid NodeObjectType { get; set; }
        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreateDate { get; set; }
    }
}
