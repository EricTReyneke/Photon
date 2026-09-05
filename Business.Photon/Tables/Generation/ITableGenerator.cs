using Business.Photon.Tables.Models;

namespace Business.Photon.Tables.Generation
{
    internal interface ITableGenerator
    {
        /// <summary>
        /// Generates Photon table instances for all valid model types discovered
        /// within the configured models assembly.
        /// </summary>
        /// <returns>
        /// A dictionary where each key represents the model type and each value
        /// represents the corresponding strongly typed Photon table.
        /// </returns>
        Dictionary<Type, IPhotonTable> GenerateTables();
    }
}