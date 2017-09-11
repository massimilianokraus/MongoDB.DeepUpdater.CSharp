using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoDB.DeepUpdater
{
    public class SingleFluent<TDocument, TField> : UpdateFluent<TDocument, TField>
    {
        public SingleFluent(TDocument document)
            : base(document)
        { }

        internal List<FieldDefinition<TDocument, TField>> InternalGetFieldDefinitions()
        {
            throw new NotImplementedException();
        }

        public FieldFluent<TDocument, TNestedField> Select<TNestedField>(Expression<Func<TField, TNestedField>> selectorExpression)
        {
            throw new NotImplementedException();
        }

        public ArrayFluent<TDocument, TNestedField> SelectArray<TNestedField>(Expression<Func<TField, IEnumerable<TNestedField>>> selectorExpression)
        {
            throw new NotImplementedException();
        }
    }
}
