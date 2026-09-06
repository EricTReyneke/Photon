namespace Business.Photon.Querying.Models
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
        /// Gets or initializes the condition used to filter the query results.
        /// </summary>
        public QueryCondition Condition { get; init; }
        #endregion
    }
}