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

        public List<FieldDefinition<TDocument, TField>> GetFieldDefinitions()
        {
            return InternalGetFieldDefinitions();
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
