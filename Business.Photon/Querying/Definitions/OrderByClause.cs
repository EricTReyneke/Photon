using System.Reflection;

namespace Business.Photon.Querying.Definitions
{
    /// <summary>
    /// Represents an ORDER BY clause within a Photon query.
    /// </summary>
    internal class OrderByClause
    {
        #region Properties
        /// <summary>
        /// Gets the property used to order the query results.
        /// </summary>
        public required PropertyInfo Property { get; init; }

        /// <summary>
        /// Gets the direction in which the query results should be ordered.
        /// </summary>
        public required QueryOrderDirection Direction { get; init; }
        #endregion
    }
}