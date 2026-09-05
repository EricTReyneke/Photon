using Business.Photon.Exceptions;
using Business.Photon.Tables.Models;

namespace Business.Photon.Validation
{
    internal class DataValidator : IDataValidator
    {
        #region Public Methods
        /// <inheritdoc />
        public void ValidatePrimaryKey(int primaryKey)
        {
            if (primaryKey <= 0)
                throw new InvalidPhotonDataException(
                    $"Primary key '{primaryKey}' is invalid. Primary keys must be greater than zero.");
        }

        /// <inheritdoc />
        public void ValidateModel<TModel>(TModel model)
        {
            if (model == null)
                throw new InvalidPhotonDataException(
                    $"A valid '{typeof(TModel).Name}' model must be supplied.");
        }

        /// <inheritdoc />
        public void ValidatePrimaryKeyDoesNotExist<TModel>(
            PhotonTable<TModel> photonTable,
            int primaryKey)
        {
            ValidateTable(photonTable);

            if (photonTable.Rows.ContainsKey(primaryKey))
                throw new DuplicatePrimaryKeyException(
                    primaryKey,
                    typeof(TModel));
        }

        /// <inheritdoc />
        public void ValidatePrimaryKeyExists<TModel>(
            PhotonTable<TModel> photonTable,
            int primaryKey)
        {
            ValidateTable(photonTable);

            if (!photonTable.Rows.ContainsKey(primaryKey))
                throw new PrimaryKeyNotFoundException(
                    primaryKey,
                    typeof(TModel));
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Validates that the supplied Photon table is available for validation.
        /// </summary>
        /// <typeparam name="TModel">The model type represented by the table.</typeparam>
        /// <param name="photonTable">The Photon table to validate.</param>
        private void ValidateTable<TModel>(PhotonTable<TModel> photonTable)
        {
            if (photonTable == null)
                throw new InvalidPhotonDataException(
                    $"A valid Photon table for model type '{typeof(TModel).Name}' must be supplied.");
        }
        #endregion
    }
}