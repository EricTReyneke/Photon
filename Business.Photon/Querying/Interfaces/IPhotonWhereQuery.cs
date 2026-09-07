using System.Linq.Expressions;

namespace Business.Photon.Querying.Interfaces
{
    /// <summary>
    /// Defines the operations available after a WHERE condition
    /// has been applied to a Photon query.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type associated with the query.
    /// </typeparam>
    public interface IPhotonWhereQuery<TModel> :
        IPhotonExecutableQuery<TModel>
    {
        /// <summary>
        /// Applies an additional filtering condition to the Photon query.
        /// </summary>
        /// <param name="predicate">
        /// The additional condition used to filter the query results.
        /// </param>
        /// <returns>
        /// The Photon query after the condition has been applied.
        /// </returns>
        IPhotonWhereQuery<TModel> Where(
            Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Groups the query results by the specified property.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the grouping key.
        /// </typeparam>
        /// <param name="keySelector">
        /// The property used to group the query results.
        /// </param>
        /// <returns>
        /// The grouped Photon query.
        /// </returns>
        IPhotonGroupByQuery<TModel> GroupBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector);

        /// <summary>
        /// Applies ascending ordering to the Photon query.
        /// </summary>
        IPhotonOrderedQuery<TModel> OrderBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector);

        /// <summary>
        /// Applies descending ordering to the Photon query.
        /// </summary>
        IPhotonOrderedQuery<TModel> OrderByDescending<TKey>(
            Expression<Func<TModel, TKey>> keySelector);

        /// <summary>
        /// Limits the number of records returned by the query.
        /// </summary>
        IPhotonExecutableQuery<TModel> Take(int count);
    }
}