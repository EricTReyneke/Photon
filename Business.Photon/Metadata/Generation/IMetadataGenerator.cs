using Business.Photon.Metadata.Models;

namespace Business.Photon.Metadata.Generation
{
    internal interface IMetadataGenerator
    {
        /// <summary>
        /// Generates table metadata for the specified model type.
        /// </summary>
        /// <param name="modelType">
        /// The model type for which metadata should be generated.
        /// </param>
        /// <returns>
        /// The generated metadata describing the Photon table.
        /// </returns>
        TableMetadata Generate(Type modelType);
    }
}