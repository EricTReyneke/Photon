using System.Reflection;

namespace Business.Photon.Querying.Definitions
{
    /// <summary>
    /// Represents a GROUP BY clause within a Photon query.
    /// </summary>
    internal class GroupByClause
    {
        #region Properties
        /// <summary>
        /// Gets the property used to group the query results.
        /// </summary>
        public required PropertyInfo Property { get; init; }
        #endregion
    }
}