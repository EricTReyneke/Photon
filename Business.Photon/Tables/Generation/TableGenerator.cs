using Business.Photon.Tables.Models;
using System.Reflection;

namespace Business.Photon.Tables.Generation
{
    /// <summary>
    /// Provides functionality for discovering Photon model types and generating
    /// strongly typed table instances for each discovered model.
    /// </summary>
    internal class TableGenerator : ITableGenerator
    {
        #region Constants
        private const string ModelsAssemblyName = "Data.Photon.Models";
        private const string ModelsNamespace = "Data.Photon.Models.Models";
        #endregion

        #region Public Methods
        public Dictionary<Type, IPhotonTable> GenerateTables()
        {
            Assembly modelsAssembly = Assembly.Load(ModelsAssemblyName);

            List<Type> modelTypes = modelsAssembly
                .GetTypes()
                .Where(type =>
                    type.IsClass &&
                    !type.IsAbstract &&
                    type.Namespace != null &&
                    type.Namespace.StartsWith(
                        ModelsNamespace,
                        StringComparison.Ordinal))
                .OrderBy(type => type.FullName)
                .ToList();

            Dictionary<Type, IPhotonTable> tables = [];

            foreach (Type modelType in modelTypes)
                tables.Add(modelType, CreateTable(modelType));

            return tables;
        }
        #endregion

        #region Private Methods
        /// <inheritdoc />
        private IPhotonTable CreateTable(Type modelType)
        {
            Type photonTableType = typeof(PhotonTable<>)
                .MakeGenericType(modelType);

            object? tableInstance = Activator.CreateInstance(photonTableType);

            if (tableInstance is not IPhotonTable photonTable)
                throw new InvalidOperationException(
                    $"Unable to create a Photon table for model type '{modelType.FullName}'.");

            return photonTable;
        }
        #endregion
    }
}