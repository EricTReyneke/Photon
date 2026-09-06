using Business.Photon.Core.Interfaces;
using Business.Photon.Data.Implementations;
using Business.Photon.Data.Interfaces;
using Business.Photon.Metadata.Caching;
using Business.Photon.Metadata.Generation;
using Business.Photon.Tables.Generation;
using Business.Photon.Tables.Models;
using Business.Photon.Validation.Implementations;
using Business.Photon.Validation.Interfaces;

namespace Business.Photon.Core.Implementations
{
    /// <summary>
    /// Represents the core Photon database engine and manages the
    /// in-memory database tables.
    /// </summary>
    public class PhotonCore : IPhotonCore
    {
        #region Fields
        private readonly Dictionary<Type, IPhotonTable> _photonDatabase;
        private readonly IMetadataCache _metadataCache;
        private readonly IDataEngine _dataEngine;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotonCore"/> class
        /// and creates the internal Photon database structure.
        /// </summary>
        public PhotonCore()
        {
            ITableGenerator tableGenerator = new TableGenerator();
            IMetadataGenerator metadataGenerator = new MetadataGenerator();
            IDataValidator dataValidator = new DataValidator();

            _photonDatabase = tableGenerator.GenerateTables();

            _metadataCache = new MetadataCache(
                _photonDatabase.Keys,
                metadataGenerator);

            _dataEngine = new DataEngine(
                _photonDatabase,
                _metadataCache,
                dataValidator);
        }
        #endregion

        #region Public Methods
        /// <inheritdoc />
        public bool ContainsKey<TModel>(int primaryKey) =>
            _dataEngine.ContainsKey<TModel>(primaryKey);

        /// <inheritdoc />
        public bool TryRetrieve<TModel>(int primaryKey, out TModel model) =>
            _dataEngine.TryRetrieve(primaryKey, out model);

        /// <inheritdoc />
        public IReadOnlyCollection<TModel> RetrieveAll<TModel>() =>
            _dataEngine.RetrieveAll<TModel>();

        /// <inheritdoc />
        public TModel Retrieve<TModel>(int primaryKey) =>
            _dataEngine.Retrieve<TModel>(primaryKey);

        /// <inheritdoc />
        public void Add<TModel>(TModel model) =>
            _dataEngine.Add(model);

        /// <inheritdoc />
        public void Update<TModel>(TModel model) =>
            _dataEngine.Update(model);

        /// <inheritdoc />
        public void Remove<TModel>(int primaryKey) =>
            _dataEngine.Remove<TModel>(primaryKey);
        #endregion
    }
}