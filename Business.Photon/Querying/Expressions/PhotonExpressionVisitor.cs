using Business.Photon.Exceptions;
using Business.Photon.Querying.Models;
using System.Linq.Expressions;

namespace Business.Photon.Querying.Expressions
{
    internal class PhotonExpressionVisitor : ExpressionVisitor, IPhotonExpressionVisitor
    {
        #region Fields
        private QueryCondition _queryCondition;
        #endregion

        #region Public Methods
        /// <summary>
        /// Parses the specified Photon query expression into an internal
        /// query condition representation.
        /// </summary>
        /// <typeparam name="TModel">The model type associated with the query.</typeparam>
        /// <param name="expression">The expression to parse.</param>
        /// <returns>
        /// The parsed query condition.
        /// </returns>
        public QueryCondition Parse<TModel>(
            Expression<Func<TModel, bool>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            _queryCondition = ParseExpression(expression.Body);

            return _queryCondition;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Parses the supplied expression node into a Photon query condition.
        /// </summary>
        /// <param name="expression">The expression node to parse.</param>
        /// <returns>
        /// The generated Photon query condition.
        /// </returns>
        private QueryCondition ParseExpression(Expression expression)
        {
            if (expression is BinaryExpression binaryExpression)
                return ParseBinaryExpression(binaryExpression);

            throw new InvalidPhotonDataException(
                $"Expression type '{expression.NodeType}' is not currently supported by Photon.");
        }

        /// <summary>
        /// Parses the specified binary expression into a Photon query condition.
        /// </summary>
        /// <param name="binaryExpression">
        /// The binary expression to parse.
        /// </param>
        /// <returns>
        /// The generated Photon query condition.
        /// </returns>
        private QueryCondition ParseBinaryExpression(
            BinaryExpression binaryExpression)
        {
            if (binaryExpression.NodeType == ExpressionType.AndAlso ||
                binaryExpression.NodeType == ExpressionType.OrElse)
                return ParseLogicalExpression(binaryExpression);

            return ParseComparisonExpression(binaryExpression);
        }

        /// <summary>
        /// Parses a logical binary expression into a Photon query condition.
        /// </summary>
        /// <param name="binaryExpression">
        /// The logical binary expression to parse.
        /// </param>
        /// <returns>
        /// The generated logical query condition.
        /// </returns>
        private QueryCondition ParseLogicalExpression(
            BinaryExpression binaryExpression)
        {
            return new QueryCondition
            {
                Operator = ConvertOperator(binaryExpression.NodeType),
                Left = ParseExpression(binaryExpression.Left),
                Right = ParseExpression(binaryExpression.Right)
            };
        }

        /// <summary>
        /// Parses a comparison binary expression into a Photon query condition.
        /// </summary>
        /// <param name="binaryExpression">
        /// The comparison binary expression to parse.
        /// </param>
        /// <returns>
        /// The generated comparison query condition.
        /// </returns>
        private QueryCondition ParseComparisonExpression(
            BinaryExpression binaryExpression)
        {
            MemberExpression memberExpression =
                RetrieveMemberExpression(binaryExpression.Left);

            object value =
                RetrieveExpressionValue(binaryExpression.Right);

            return new QueryCondition
            {
                ColumnName = memberExpression.Member.Name,
                Operator = ConvertOperator(binaryExpression.NodeType),
                Value = value
            };
        }

        /// <summary>
        /// Retrieves the member expression associated with the supplied expression.
        /// </summary>
        /// <param name="expression">
        /// The expression expected to represent a model property.
        /// </param>
        /// <returns>
        /// The resolved member expression.
        /// </returns>
        private MemberExpression RetrieveMemberExpression(
            Expression expression)
        {
            if (expression is MemberExpression memberExpression)
                return memberExpression;

            throw new InvalidPhotonDataException(
                $"Expression '{expression}' does not reference a supported Photon column.");
        }

        /// <summary>
        /// Retrieves the runtime value represented by the supplied expression.
        /// </summary>
        /// <param name="expression">
        /// The expression whose value should be evaluated.
        /// </param>
        /// <returns>
        /// The resolved expression value.
        /// </returns>
        private object RetrieveExpressionValue(
            Expression expression)
        {
            if (expression is ConstantExpression constantExpression)
                return constantExpression.Value;

            LambdaExpression lambdaExpression =
                Expression.Lambda(expression);

            Delegate compiledExpression =
                lambdaExpression.Compile();

            return compiledExpression.DynamicInvoke();
        }

        /// <summary>
        /// Converts a .NET expression node type into the corresponding
        /// Photon query operator.
        /// </summary>
        /// <param name="expressionType">
        /// The expression node type to convert.
        /// </param>
        /// <returns>
        /// The corresponding Photon query operator.
        /// </returns>
        private QueryOperator ConvertOperator(
            ExpressionType expressionType)
        {
            return expressionType switch
            {
                ExpressionType.Equal =>
                    QueryOperator.Equal,

                ExpressionType.NotEqual =>
                    QueryOperator.NotEqual,

                ExpressionType.GreaterThan =>
                    QueryOperator.GreaterThan,

                ExpressionType.GreaterThanOrEqual =>
                    QueryOperator.GreaterThanOrEqual,

                ExpressionType.LessThan =>
                    QueryOperator.LessThan,

                ExpressionType.LessThanOrEqual =>
                    QueryOperator.LessThanOrEqual,

                ExpressionType.AndAlso =>
                    QueryOperator.And,

                ExpressionType.OrElse =>
                    QueryOperator.Or,

                _ => throw new InvalidPhotonDataException(
                    $"Expression operator '{expressionType}' is not currently supported by Photon.")
            };
        }
        #endregion
    }
}