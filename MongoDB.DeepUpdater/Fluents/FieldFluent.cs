using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MongoDB.DeepUpdater
{
    public class FieldFluent<TDocument, TField> : SingleFluent<TDocument, TField>
    {
        public FieldFluent(TDocument document)
            : base(document)
        { }

        public ArrayFluent<TDocument, TNestedField> SelectArray<TNestedField>(Expression<Func<TField, IEnumerable<TNestedField>>> selectExpression)
        {
            return null;
        }

        public UpdateDefinition<TDocument> Set(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> Inc(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> Mul(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> CurrentDate(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> Max(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> Min(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> BitwiseAnd(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> BitwiseOr(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> BitwiseXor(TField item)
        {
            throw new NotImplementedException();
        }
    }
}
