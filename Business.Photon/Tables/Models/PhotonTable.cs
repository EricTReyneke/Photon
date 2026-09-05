namespace Business.Photon.Tables.Models
{
    /// <summary>
    /// Represents an in-memory Photon table containing rows of the specified model type.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type represented by the Photon table.
    /// </typeparam>
    internal class PhotonTable<TModel> : IPhotonTable
    {
        #region Properties
        /// <summary>
        /// Gets the model type represented by the Photon table.
        /// </summary>
        public Type ModelType => typeof(TModel);

        /// <summary>
        /// Gets the rows currently stored within the Photon table,
        /// indexed by their primary key.
        /// </summary>
        internal Dictionary<int, TModel> Rows { get; } = [];
        #endregion
    }
}