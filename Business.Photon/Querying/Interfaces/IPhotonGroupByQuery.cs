using System.Linq.Expressions;

namespace Business.Photon.Querying.Interfaces
{
    /// <summary>
    /// Defines the operations available after grouping has been applied
    /// to a Photon query.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type associated with the query.
    /// </typeparam>
    public interface IPhotonGroupByQuery<TModel> :
        IPhotonExecutableQuery<TModel>
    {
        /// <summary>
        /// Applies ascending ordering to the grouped query.
        /// </summary>
        IPhotonOrderedQuery<TModel> OrderBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector);

        /// <summary>
        /// Applies descending ordering to the grouped query.
        /// </summary>
        IPhotonOrderedQuery<TModel> OrderByDescending<TKey>(
            Expression<Func<TModel, TKey>> keySelector);

        /// <summary>
        /// Limits the number of records returned by the query.
        /// </summary>
        IPhotonExecutableQuery<TModel> Take(int count);
    }
}