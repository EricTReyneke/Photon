using Business.Photon.Querying.Models;

namespace Business.Photon.Querying.Definitions
{
    /// <summary>
    /// Represents the execution plan for a Photon query.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type associated with the query.
    /// </typeparam>
    internal class QueryPlan<TModel>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the condition used to filter the query results.
        /// </summary>
        public QueryCondition? Condition { get; set; }

        /// <summary>
        /// Gets or sets the grouping definition applied to the query.
        /// </summary>
        public GroupByClause? GroupBy { get; set; }

        /// <summary>
        /// Gets the ordering definitions applied to the query.
        /// </summary>
        public List<OrderByClause> OrderBy { get; } = [];

        /// <summary>
        /// Gets or sets the maximum number of records returned by the query.
        /// </summary>
        public int? Take { get; set; }
        #endregion
    }
}