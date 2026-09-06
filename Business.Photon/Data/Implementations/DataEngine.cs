using Business.Photon.Data.Interfaces;
using Business.Photon.Exceptions;
using Business.Photon.Metadata.Caching;
using Business.Photon.Metadata.Models;
using Business.Photon.Tables.Models;
using Business.Photon.Validation.Interfaces;

namespace Business.Photon.Data.Implementations
{
    internal class DataEngine : IDataEngine
    {
        #region Fields
        private readonly Dictionary<Type, IPhotonTable> _photonDatabase;
        private readonly IMetadataCache _metadataCache;
        private readonly IDataValidator _dataValidator;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DataEngine"/> class.
        /// </summary>
        /// <param name="photonDatabase">
        /// The Photon database containing the generated tables.
        /// </param>
        /// <param name="metadataCache">
        /// The metadata cache containing generated metadata for Photon models.
        /// </param>
        /// <param name="dataValidator">
        /// The validator responsible for validating Photon data operations.
        /// </param>
        public DataEngine(
            Dictionary<Type, IPhotonTable> photonDatabase,
            IMetadataCache metadataCache,
            IDataValidator dataValidator)
        {
            if (photonDatabase == null)
                throw new ArgumentNullException(nameof(photonDatabase));

            if (metadataCache == null)
                throw new ArgumentNullException(nameof(metadataCache));

            if (dataValidator == null)
                throw new ArgumentNullException(nameof(dataValidator));

            _photonDatabase = photonDatabase;
            _metadataCache = metadataCache;
            _dataValidator = dataValidator;
        }
        #endregion

        #region Public Methods
        /// <inheritdoc />
        public bool ContainsKey<TModel>(int primaryKey)
        {
            _dataValidator.ValidatePrimaryKey(primaryKey);

            PhotonTable<TModel> photonTable = RetrieveTable<TModel>();

            return photonTable.Rows.ContainsKey(primaryKey);
        }

        /// <inheritdoc />
        public IReadOnlyCollection<TModel> RetrieveAll<TModel>()
        {
            PhotonTable<TModel> photonTable = RetrieveTable<TModel>();

            return photonTable.Rows.Values.ToList();
        }

        /// <inheritdoc />
        public bool TryRetrieve<TModel>(int primaryKey, out TModel model)
        {
            _dataValidator.ValidatePrimaryKey(primaryKey);

            PhotonTable<TModel> photonTable = RetrieveTable<TModel>();

            return photonTable.Rows.TryGetValue(
                primaryKey,
                out model);
        }

        /// <inheritdoc />
        public TModel Retrieve<TModel>(int primaryKey)
        {
            if (!TryRetrieve(
                primaryKey,
                out TModel model))
                throw new PrimaryKeyNotFoundException(
                    primaryKey,
                    typeof(TModel));

            return model;
        }

        /// <inheritdoc />
        public void Add<TModel>(TModel model)
        {
            _dataValidator.ValidateModel(model);

            int primaryKey = RetrievePrimaryKey(model);

            _dataValidator.ValidatePrimaryKey(primaryKey);

            PhotonTable<TModel> photonTable = RetrieveTable<TModel>();

            _dataValidator.ValidatePrimaryKeyDoesNotExist(
                photonTable,
                primaryKey);

            photonTable.Rows.Add(
                primaryKey,
                model);
        }

        /// <inheritdoc />
        public void Update<TModel>(TModel model)
        {
            _dataValidator.ValidateModel(model);

            int primaryKey = RetrievePrimaryKey(model);

            _dataValidator.ValidatePrimaryKey(primaryKey);

            PhotonTable<TModel> photonTable = RetrieveTable<TModel>();

            _dataValidator.ValidatePrimaryKeyExists(
                photonTable,
                primaryKey);

            photonTable.Rows[primaryKey] = model;
        }

        /// <inheritdoc />
        public void Remove<TModel>(int primaryKey)
        {
            _dataValidator.ValidatePrimaryKey(primaryKey);

            PhotonTable<TModel> photonTable = RetrieveTable<TModel>();

            _dataValidator.ValidatePrimaryKeyExists(
                photonTable,
                primaryKey);

            photonTable.Rows.Remove(primaryKey);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Retrieves the Photon table associated with the specified model type.
        /// </summary>
        /// <typeparam name="TModel">The model type represented by the table.</typeparam>
        /// <returns>
        /// The strongly typed Photon table associated with the specified model type.
        /// </returns>
        private PhotonTable<TModel> RetrieveTable<TModel>()
        {
            Type modelType = typeof(TModel);

            if (!_photonDatabase.TryGetValue(
                modelType,
                out IPhotonTable photonTable))
                throw new InvalidPhotonDataException(
                    $"No Photon table exists for model type '{modelType.Name}'.");

            if (photonTable is not PhotonTable<TModel> typedTable)
                throw new InvalidPhotonDataException(
                    $"The Photon table for model type '{modelType.Name}' is invalid.");

            return typedTable;
        }

        /// <summary>
        /// Retrieves the primary key value from the specified model
        /// using the cached Photon metadata.
        /// </summary>
        /// <typeparam name="TModel">
        /// The model type from which the primary key should be retrieved.
        /// </typeparam>
        /// <param name="model">
        /// The model from which the primary key should be retrieved.
        /// </param>
        /// <returns>
        /// The primary key value associated with the model.
        /// </returns>
        private int RetrievePrimaryKey<TModel>(TModel model)
        {
            TableMetadata tableMetadata =
                _metadataCache.Retrieve<TModel>();

            object primaryKeyValue =
                tableMetadata.PrimaryKey.Property.GetValue(model);

            if (primaryKeyValue == null)
                throw new InvalidPhotonDataException(
                    $"The primary key for model type '{typeof(TModel).Name}' cannot be null.");

            if (primaryKeyValue is not int primaryKey)
                throw new InvalidPhotonDataException(
                    $"The primary key for model type '{typeof(TModel).Name}' must be of type '{nameof(Int32)}'.");

            return primaryKey;
        }
        #endregion
    }
}