using Business.Photon.Querying.Models;
using System.Linq.Expressions;

namespace Business.Photon.Querying.Expressions
{
    internal interface IPhotonExpressionVisitor
    {
        /// <summary>
        /// Parses the specified Photon query expression into an internal
        /// query condition representation.
        /// </summary>
        /// <typeparam name="TModel">
        /// The model type associated with the query.
        /// </typeparam>
        /// <param name="expression">
        /// The expression to parse.
        /// </param>
        /// <returns>
        /// The parsed query condition.
        /// </returns>
        QueryCondition Parse<TModel>(
            Expression<Func<TModel, bool>> expression);
    }
}