namespace Business.Photon.Metadata.Models
{
    /// <summary>
    /// Represents metadata describing the structure of a Photon table.
    /// </summary>
    internal class TableMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the name of the Photon table.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets the model type represented by the Photon table.
        /// </summary>
        public required Type ModelType { get; init; }

        /// <summary>
        /// Gets the primary key column of the Photon table.
        /// </summary>
        public required ColumnMetadata PrimaryKey { get; init; }

        /// <summary>
        /// Gets the columns defined within the Photon table.
        /// </summary>
        public required IReadOnlyList<ColumnMetadata> Columns { get; init; }
        #endregion
    }
}