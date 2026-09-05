using Business.Photon.Tables.Models;

namespace Business.Photon.Validation
{
    internal interface IDataValidator
    {
        /// <summary>
        /// Validates that the supplied primary key is valid for Photon storage.
        /// </summary>
        /// <param name="primaryKey">The primary key to validate.</param>
        void ValidatePrimaryKey(int primaryKey);

        /// <summary>
        /// Validates that the supplied model contains valid data.
        /// </summary>
        /// <typeparam name="TModel">The model type to validate.</typeparam>
        /// <param name="model">The model to validate.</param>
        void ValidateModel<TModel>(TModel model);

        /// <summary>
        /// Validates that the supplied primary key does not already exist
        /// within the specified Photon table.
        /// </summary>
        /// <typeparam name="TModel">The model type represented by the table.</typeparam>
        /// <param name="photonTable">The Photon table to validate against.</param>
        /// <param name="primaryKey">The primary key to validate.</param>
        void ValidatePrimaryKeyDoesNotExist<TModel>(
            PhotonTable<TModel> photonTable,
            int primaryKey);

        /// <summary>
        /// Validates that the supplied primary key exists
        /// within the specified Photon table.
        /// </summary>
        /// <typeparam name="TModel">The model type represented by the table.</typeparam>
        /// <param name="photonTable">The Photon table to validate against.</param>
        /// <param name="primaryKey">The primary key to validate.</param>
        void ValidatePrimaryKeyExists<TModel>(
            PhotonTable<TModel> photonTable,
            int primaryKey);
    }
}