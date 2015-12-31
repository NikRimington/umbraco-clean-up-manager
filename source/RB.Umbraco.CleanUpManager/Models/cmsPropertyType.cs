namespace RB.Umbraco.CleanUpManager.Models
{
    /// <summary>
    /// Class CmsPropertyType.
    /// </summary>
    public class CmsPropertyType
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the data type identifier.
        /// </summary>
        /// <value>The data type identifier.</value>
        public int DataTypeId { get; set; }
        /// <summary>
        /// Gets or sets the content type identifier.
        /// </summary>
        /// <value>The content type identifier.</value>
        public int ContentTypeId { get; set; }
        /// <summary>
        /// Gets or sets the property type group identifier.
        /// </summary>
        /// <value>The property type group identifier.</value>
        public int PropertyTypeGroupId { get; set; }
        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
        public string Alias { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the helptext.
        /// </summary>
        /// <value>The helptext.</value>
        public string Helptext { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        public int SortOrder { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CmsPropertyType"/> is mandatory.
        /// </summary>
        /// <value><c>true</c> if mandatory; otherwise, <c>false</c>.</value>
        public bool Mandatory { get; set; }
        /// <summary>
        /// Gets or sets the validation reg exp.
        /// </summary>
        /// <value>The validation reg exp.</value>
        public string ValidationRegExp { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

    }
}
