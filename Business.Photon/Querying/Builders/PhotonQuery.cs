using Business.Photon.Querying.Definitions;
using Business.Photon.Querying.Expressions;
using Business.Photon.Querying.Interfaces;
using Business.Photon.Querying.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace Business.Photon.Querying.Builders
{
    /// <summary>
    /// Represents a Photon query and manages the construction
    /// of its internal query plan.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type associated with the query.
    /// </typeparam>
    internal class PhotonQuery<TModel> :
        IPhotonQueryStart<TModel>,
        IPhotonWhereQuery<TModel>,
        IPhotonGroupByQuery<TModel>,
        IPhotonOrderedQuery<TModel>,
        IPhotonExecutableQuery<TModel>
    {
        #region Fields
        private readonly QueryPlan<TModel> _queryPlan;
        private readonly IPhotonExpressionVisitor _expressionVisitor;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotonQuery{TModel}"/> class.
        /// </summary>
        /// <param name="expressionVisitor">
        /// The expression visitor used to translate query expressions
        /// into Photon query definitions.
        /// </param>
        public PhotonQuery(
            IPhotonExpressionVisitor expressionVisitor)
        {
            if (expressionVisitor == null)
                throw new ArgumentNullException(nameof(expressionVisitor));

            _expressionVisitor = expressionVisitor;

            _queryPlan = new QueryPlan<TModel>();
        }
        #endregion

        #region Public Methods
        /// <inheritdoc />
        public IPhotonWhereQuery<TModel> Where(
            Expression<Func<TModel, bool>> predicate)
        {
            QueryCondition queryCondition =
                _expressionVisitor.Parse(predicate);

            _queryPlan.Condition = queryCondition;

            return this;
        }

        /// <inheritdoc />
        public IPhotonGroupByQuery<TModel> GroupBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector)
        {
            PropertyInfo property =
                RetrieveProperty(keySelector);

            _queryPlan.GroupBy = new GroupByClause
            {
                Property = property
            };

            return this;
        }

        /// <inheritdoc />
        public IPhotonOrderedQuery<TModel> OrderBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector)
        {
            AddOrderBy(
                keySelector,
                QueryOrderDirection.Ascending);

            return this;
        }

        /// <inheritdoc />
        public IPhotonOrderedQuery<TModel> OrderByDescending<TKey>(
            Expression<Func<TModel, TKey>> keySelector)
        {
            AddOrderBy(
                keySelector,
                QueryOrderDirection.Descending);

            return this;
        }

        /// <inheritdoc />
        public IPhotonOrderedQuery<TModel> ThenBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector)
        {
            AddOrderBy(
                keySelector,
                QueryOrderDirection.Ascending);

            return this;
        }

        /// <inheritdoc />
        public IPhotonOrderedQuery<TModel> ThenByDescending<TKey>(
            Expression<Func<TModel, TKey>> keySelector)
        {
            AddOrderBy(
                keySelector,
                QueryOrderDirection.Descending);

            return this;
        }

        /// <inheritdoc />
        public IPhotonExecutableQuery<TModel> Take(
            int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            _queryPlan.Take = count;

            return this;
        }

        /// <inheritdoc />
        public List<TModel> ToList()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TModel[] ToArray()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TModel First()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public TModel? FirstOrDefault()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public int Count()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Any()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Adds an ordering definition to the Photon query plan.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the property used for ordering.
        /// </typeparam>
        /// <param name="keySelector">
        /// The property used to order the query results.
        /// </param>
        /// <param name="direction">
        /// The direction in which the query results should be ordered.
        /// </param>
        private void AddOrderBy<TKey>(
            Expression<Func<TModel, TKey>> keySelector,
            QueryOrderDirection direction)
        {
            PropertyInfo property =
                RetrieveProperty(keySelector);

            _queryPlan.OrderBy.Add(
                new OrderByClause
                {
                    Property = property,
                    Direction = direction
                });
        }

        /// <summary>
        /// Retrieves the model property represented by the supplied expression.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the property represented by the expression.
        /// </typeparam>
        /// <param name="keySelector">
        /// The expression used to select the model property.
        /// </param>
        /// <returns>
        /// The resolved model property.
        /// </returns>
        private PropertyInfo RetrieveProperty<TKey>(
            Expression<Func<TModel, TKey>> keySelector)
        {
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            if (keySelector.Body is not MemberExpression memberExpression)
                throw new InvalidOperationException(
                    "The supplied query expression must reference a model property.");

            if (memberExpression.Member is not PropertyInfo property)
                throw new InvalidOperationException(
                    "The supplied query expression must reference a model property.");

            return property;
        }
        #endregion
    }
}