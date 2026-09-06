using System.Linq.Expressions;

namespace Business.Photon.Querying.Builders.Interfaces
{
    public interface IPhotonQuery<TModel>
    {
        /// <summary>
        /// Adds a filter condition to the Photon query.
        /// </summary>
        /// <param name="predicate">
        /// The expression used to filter the query results.
        /// </param>
        /// <returns>
        /// The current Photon query with the filter applied.
        /// </returns>
        IPhotonQuery<TModel> Where(
            Expression<Func<TModel, bool>> predicate);

        /// <summary>
        /// Executes the Photon query and returns the matching models as a list.
        /// </summary>
        /// <returns>
        /// A list containing the models returned by the query.
        /// </returns>
        List<TModel> ToList();

        /// <summary>
        /// Executes the Photon query and returns the matching models as an array.
        /// </summary>
        /// <returns>
        /// An array containing the models returned by the query.
        /// </returns>
        TModel[] ToArray();
    }
}