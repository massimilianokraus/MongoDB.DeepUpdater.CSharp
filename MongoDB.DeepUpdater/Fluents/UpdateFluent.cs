using System;
using System.Linq.Expressions;

namespace MongoDB.DeepUpdater
{
    public abstract class UpdateFluent<TDocument, TField>
    {
        protected UpdateFluent(TDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            Document = document;
        }

        internal TDocument Document
        {
            get;
        }

        protected string GetPropertyName<TNestedField>(Expression<Func<TField, TNestedField>> selectorExpression)
        {
            var wholeReturnExpression = selectorExpression.Body.ToString();

            var firstDotIndex = wholeReturnExpression.IndexOf('.');

            var propertyString = wholeReturnExpression.Substring(firstDotIndex + 1);

            return propertyString;
        }
    }
}
