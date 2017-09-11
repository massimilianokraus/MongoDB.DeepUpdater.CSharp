using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    public class ArrayFluent<TDocument, TField> : UpdateFluent<TDocument, TField>
    {
        public ArrayFluent(TDocument document)
            : base(document)
        { }

        public FieldFluent<TDocument, TField> Where(Func<TField, bool> selector)
        {
            throw new NotImplementedException();
        }

        public List<FieldDefinition<TDocument>> GetFieldDefinitions()
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> AddToSet(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> AddToSetEach(IEnumerable<TField> items)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> Push(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> PushEach(IEnumerable<TField> items)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> PopFirst(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> PopLast(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> Pull(TField item)
        {
            throw new NotImplementedException();
        }

        public UpdateDefinition<TDocument> PullAll(IEnumerable<TField> items)
        {
            throw new NotImplementedException();
        }
    }
}
