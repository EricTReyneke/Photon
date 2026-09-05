using Business.Photon.Metadata.Models;

namespace Business.Photon.Metadata.Caching
{
    internal interface IMetadataCache
    {
        /// <summary>
        /// Retrieves the cached table metadata associated with the specified model type.
        /// </summary>
        /// <typeparam name="TModel">The model type whose metadata should be retrieved.</typeparam>
        /// <returns>
        /// The cached table metadata associated with the specified model type.
        /// </returns>
        TableMetadata Retrieve<TModel>();

        /// <summary>
        /// Retrieves the cached table metadata associated with the specified model type.
        /// </summary>
        /// <param name="modelType">The model type whose metadata should be retrieved.</param>
        /// <returns>
        /// The cached table metadata associated with the specified model type.
        /// </returns>
        TableMetadata Retrieve(Type modelType);

        /// <summary>
        /// Determines whether metadata exists for the specified model type.
        /// </summary>
        /// <typeparam name="TModel">The model type to check.</typeparam>
        /// <returns>
        /// <see langword="true"/> when metadata exists; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool Contains<TModel>();
    }
}