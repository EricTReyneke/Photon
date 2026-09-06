namespace Business.Photon.Querying.Models
{
    internal class QueryCondition
    {
        #region Properties
        public string ColumnName { get; init; }

        public QueryOperator Operator { get; init; }

        public object Value { get; init; }

        public QueryCondition Left { get; init; }

        public QueryCondition Right { get; init; }
        #endregion
    }
}