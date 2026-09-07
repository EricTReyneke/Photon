namespace Business.Photon.Querying.Interfaces
{
    /// <summary>
    /// Defines the executable operations available for a Photon query.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type associated with the query.
    /// </typeparam>
    public interface IPhotonExecutableQuery<TModel>
    {
        /// <summary>
        /// Executes the query and returns the resulting records.
        /// </summary>
        /// <returns>
        /// The records returned by the Photon query.
        /// </returns>
        List<TModel> ToList();

        /// <summary>
        /// Executes the query and returns the resulting records as an array.
        /// </summary>
        /// <returns>
        /// The records returned by the Photon query.
        /// </returns>
        TModel[] ToArray();

        /// <summary>
        /// Executes the query and returns the first matching record.
        /// </summary>
        /// <returns>
        /// The first matching record.
        /// </returns>
        TModel First();

        /// <summary>
        /// Executes the query and returns the first matching record,
        /// or the default value when no records are found.
        /// </summary>
        /// <returns>
        /// The first matching record or the default value.
        /// </returns>
        TModel? FirstOrDefault();

        /// <summary>
        /// Executes the query and returns the number of matching records.
        /// </summary>
        /// <returns>
        /// The number of matching records.
        /// </returns>
        int Count();

        /// <summary>
        /// Executes the query and determines whether any matching records exist.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> when at least one matching record exists;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        bool Any();
    }
}