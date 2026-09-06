namespace Business.Photon.Data.Interfaces
{
    internal interface IDataEngine
    {
        /// <summary>
        /// Determines whether the Photon table associated with the specified model
        /// contains the supplied primary key.
        /// </summary>
        /// <typeparam name="TModel">The model type associated with the table.</typeparam>
        /// <param name="primaryKey">The primary key to check.</param>
        /// <returns>
        /// <see langword="true"/> when the primary key exists; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool ContainsKey<TModel>(int primaryKey);

        /// <summary>
        /// Retrieves all models stored within the Photon table associated
        /// with the specified model type.
        /// </summary>
        /// <typeparam name="TModel">The model type associated with the table.</typeparam>
        /// <returns>
        /// A read-only collection containing all models stored within the Photon table.
        /// </returns>
        IReadOnlyCollection<TModel> RetrieveAll<TModel>();

        /// <summary>
        /// Attempts to retrieve a model using the specified primary key.
        /// </summary>
        /// <typeparam name="TModel">The model type associated with the table.</typeparam>
        /// <param name="primaryKey">The primary key of the model to retrieve.</param>
        /// <param name="model">The model associated with the primary key when found.</param>
        /// <returns>
        /// <see langword="true"/> when the model exists; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        bool TryRetrieve<TModel>(int primaryKey, out TModel model);

        /// <summary>
        /// Retrieves a model using the specified primary key.
        /// </summary>
        /// <typeparam name="TModel">The model type associated with the table.</typeparam>
        /// <param name="primaryKey">The primary key of the model to retrieve.</param>
        /// <returns>
        /// The model associated with the specified primary key.
        /// </returns>
        TModel Retrieve<TModel>(int primaryKey);

        /// <summary>
        /// Adds the specified model to its associated Photon table.
        /// The model's configured Photon primary key is used to identify the row.
        /// </summary>
        /// <typeparam name="TModel">The model type associated with the table.</typeparam>
        /// <param name="model">The model to add to the Photon table.</param>
        void Add<TModel>(TModel model);

        /// <summary>
        /// Updates an existing model within its associated Photon table.
        /// The model's configured Photon primary key is used to identify the row.
        /// </summary>
        /// <typeparam name="TModel">The model type associated with the table.</typeparam>
        /// <param name="model">The updated model.</param>
        void Update<TModel>(TModel model);

        /// <summary>
        /// Removes the model associated with the specified primary key
        /// from its Photon table.
        /// </summary>
        /// <typeparam name="TModel">The model type associated with the table.</typeparam>
        /// <param name="primaryKey">The primary key of the model to remove.</param>
        void Remove<TModel>(int primaryKey);
    }
}