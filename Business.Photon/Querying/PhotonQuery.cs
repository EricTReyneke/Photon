using Business.Photon.Querying.Expressions;
using System.Linq.Expressions;

namespace Business.Photon.Querying
{
    internal class PhotonQuery<TModel> : IPhotonQuery<TModel>
    {
        #region Fields
        private readonly List<Expression<Func<TModel, bool>>> _predicates;
        private readonly IPhotonExpressionVisitor _expressionVisitor;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotonQuery{TModel}"/> class.
        /// </summary>
        /// <param name="expressionVisitor">
        /// The expression visitor responsible for parsing Photon query expressions.
        /// </param>
        public PhotonQuery(
            IPhotonExpressionVisitor expressionVisitor)
        {
            if (expressionVisitor == null)
                throw new ArgumentNullException(nameof(expressionVisitor));

            _expressionVisitor = expressionVisitor;

            _predicates = [];
        }
        #endregion

        #region Public Methods
        /// <inheritdoc />
        public IPhotonQuery<TModel> Where(
            Expression<Func<TModel, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            _predicates.Add(predicate);

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
            return ToList().ToArray();
        }
        #endregion
    }
}