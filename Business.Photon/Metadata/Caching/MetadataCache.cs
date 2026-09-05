using Business.Photon.Exceptions;
using Business.Photon.Metadata.Generation;
using Business.Photon.Metadata.Models;

namespace Business.Photon.Metadata.Caching
{
    internal class MetadataCache : IMetadataCache
    {
        #region Fields
        private readonly Dictionary<Type, TableMetadata> _metadata;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataCache"/> class
        /// and generates metadata for the supplied model types.
        /// </summary>
        /// <param name="modelTypes">
        /// The model types for which metadata should be generated and cached.
        /// </param>
        /// <param name="metadataGenerator">
        /// The metadata generator used to generate table metadata.
        /// </param>
        public MetadataCache(
            IEnumerable<Type> modelTypes,
            IMetadataGenerator metadataGenerator)
        {
            if (modelTypes == null)
                throw new ArgumentNullException(nameof(modelTypes));

            if (metadataGenerator == null)
                throw new ArgumentNullException(nameof(metadataGenerator));

            _metadata = GenerateMetadata(
                modelTypes,
                metadataGenerator);
        }
        #endregion

        #region Public Methods
        /// <inheritdoc />
        public TableMetadata Retrieve<TModel>() =>
            Retrieve(typeof(TModel));

        /// <inheritdoc />
        public TableMetadata Retrieve(Type modelType)
        {
            if (modelType == null)
                throw new ArgumentNullException(nameof(modelType));

            if (!_metadata.TryGetValue(
                modelType,
                out TableMetadata tableMetadata))
                throw new InvalidPhotonDataException(
                    $"No Photon metadata exists for model type '{modelType.Name}'.");

            return tableMetadata;
        }

        /// <inheritdoc />
        public bool Contains<TModel>() =>
            _metadata.ContainsKey(typeof(TModel));
        #endregion

        #region Private Methods
        /// <summary>
        /// Generates and caches metadata for the supplied model types.
        /// </summary>
        /// <param name="modelTypes">
        /// The model types for which metadata should be generated.
        /// </param>
        /// <param name="metadataGenerator">
        /// The metadata generator used to generate each table definition.
        /// </param>
        /// <returns>
        /// A dictionary containing the generated metadata indexed by model type.
        /// </returns>
        private Dictionary<Type, TableMetadata> GenerateMetadata(
            IEnumerable<Type> modelTypes,
            IMetadataGenerator metadataGenerator)
        {
            Dictionary<Type, TableMetadata> metadata = [];

            foreach (Type modelType in modelTypes)
            {
                TableMetadata tableMetadata =
                    metadataGenerator.Generate(modelType);

                metadata.Add(
                    modelType,
                    tableMetadata);
            }

            return metadata;
        }
        #endregion
    }
}