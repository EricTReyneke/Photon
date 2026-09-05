using Abstractions.Photon.Attributes;
using Business.Photon.Exceptions;
using Business.Photon.Metadata.Models;
using System.Reflection;

namespace Business.Photon.Metadata.Generation
{
    internal class MetadataGenerator : IMetadataGenerator
    {
        #region Public Methods
        /// <inheritdoc />
        public TableMetadata Generate(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));

            List<ColumnMetadata> columns = GenerateColumns(modelType);

            ColumnMetadata primaryKey = RetrievePrimaryKey(
                modelType,
                columns);

            return new TableMetadata
            {
                Name = modelType.Name,
                ModelType = modelType,
                PrimaryKey = primaryKey,
                Columns = columns
            };
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generates column metadata from the public properties
        /// defined on the specified model type.
        /// </summary>
        /// <param name="modelType">
        /// The model type from which column metadata should be generated.
        /// </param>
        /// <returns>
        /// A collection containing the generated column metadata.
        /// </returns>
        private List<ColumnMetadata> GenerateColumns(Type modelType)
        {
            PropertyInfo[] properties = modelType.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance);

            List<ColumnMetadata> columns = [];

            foreach (PropertyInfo property in properties)
            {
                bool isPrimaryKey =
                    property.GetCustomAttribute<PhotonPrimaryKeyAttribute>()
                    != null;

                ColumnMetadata column = new()
                {
                    Name = property.Name,
                    DataType = property.PropertyType,
                    Property = property,
                    IsPrimaryKey = isPrimaryKey
                };

                columns.Add(column);
            }

            return columns;
        }

        /// <summary>
        /// Retrieves the primary key column from the generated column metadata
        /// and validates that exactly one primary key has been defined.
        /// </summary>
        /// <param name="modelType">
        /// The model type represented by the metadata.
        /// </param>
        /// <param name="columns">
        /// The generated columns associated with the model.
        /// </param>
        /// <returns>
        /// The column representing the primary key.
        /// </returns>
        private ColumnMetadata RetrievePrimaryKey(
            Type modelType,
            List<ColumnMetadata> columns)
        {
            List<ColumnMetadata> primaryKeys = columns
                .Where(column => column.IsPrimaryKey)
                .ToList();

            if (primaryKeys.Count == 0)
                throw new InvalidPhotonDataException(
                    $"Model type '{modelType.Name}' does not define a Photon primary key.");

            if (primaryKeys.Count > 1)
                throw new InvalidPhotonDataException(
                    $"Model type '{modelType.Name}' defines multiple Photon primary keys.");

            return primaryKeys[0];
        }
        #endregion
    }
}