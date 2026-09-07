using System.Linq.Expressions;

namespace Business.Photon.Querying.Interfaces
{
    /// <summary>
    /// Defines the operations available after ordering has been applied
    /// to a Photon query.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type associated with the query.
    /// </typeparam>
    public interface IPhotonOrderedQuery<TModel> :
        IPhotonExecutableQuery<TModel>
    {
        /// <summary>
        /// Applies an additional ascending ordering condition to the query.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the property used for ordering.
        /// </typeparam>
        /// <param name="keySelector">
        /// The property used to apply the additional ordering.
        /// </param>
        /// <returns>
        /// The ordered Photon query.
        /// </returns>
        IPhotonOrderedQuery<TModel> ThenBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector);

        /// <summary>
        /// Limits the number of records returned by the query.
        /// </summary>
        /// <param name="count">
        /// The maximum number of records to return.
        /// </param>
        /// <returns>
        /// The executable Photon query.
        /// </returns>
        IPhotonExecutableQuery<TModel> Take(int count);
    }
}