using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoDB.DeepUpdater
{
    public class UpdateFluent<TDocument, TField>
    {
        public UpdateFluent(TDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            Document = document;
        }

        internal TDocument Document
        {
            get;
        }

        public UpdateFluent<TDocument, TNestedField> Select<TNestedField>(Expression<Func<TField, TNestedField>> selectExpression)
        {
            return null;
        }

        public UpdateFluent<TDocument, TNestedField> SelectArray<TNestedField>(Expression<Func<TField, IEnumerable<TNestedField>>> selectExpression)
        {
            return null;
        }

        public List<FieldDefinition<TDocument>> GetFieldDefinitions()
        {
            return null;
        }
    }
}
