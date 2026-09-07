using System.Linq.Expressions;

namespace Business.Photon.Querying.Interfaces
{
    /// <summary>
    /// Defines the operations available when starting a Photon query.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type associated with the query.
    /// </typeparam>
    public interface IPhotonQueryStart<TModel> :
        IPhotonExecutableQuery<TModel>
    {
        /// <summary>
        /// Applies a filtering condition to the Photon query.
        /// </summary>
        /// <param name="predicate">
        /// The condition used to filter the query results.
        /// </param>
        /// <returns>
        /// The Photon query after the filtering condition has been applied.
        /// </returns>
        IPhotonWhereQuery<TModel> Where(
            Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Applies ascending ordering to the Photon query.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the property used for ordering.
        /// </typeparam>
        /// <param name="keySelector">
        /// The property used to order the query results.
        /// </param>
        /// <returns>
        /// The ordered Photon query.
        /// </returns>
        IPhotonOrderedQuery<TModel> OrderBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector);

        /// <summary>
        /// Applies descending ordering to the Photon query.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the property used for ordering.
        /// </typeparam>
        /// <param name="keySelector">
        /// The property used to order the query results.
        /// </param>
        /// <returns>
        /// The ordered Photon query.
        /// </returns>
        IPhotonOrderedQuery<TModel> OrderByDescending<TKey>(
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