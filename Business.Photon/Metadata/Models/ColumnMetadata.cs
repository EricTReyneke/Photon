using System.Reflection;

namespace Business.Photon.Metadata.Models
{
    /// <summary>
    /// Represents metadata describing a column within a Photon table.
    /// </summary>
    internal class ColumnMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets the data type stored within the column.
        /// </summary>
        public required Type DataType { get; init; }

        /// <summary>
        /// Gets the property associated with the column on the model.
        /// </summary>
        public required PropertyInfo Property { get; init; }

        /// <summary>
        /// Gets a value indicating whether the column represents the primary key
        /// of the Photon table.
        /// </summary>
        public required bool IsPrimaryKey { get; init; }
        #endregion
    }
}