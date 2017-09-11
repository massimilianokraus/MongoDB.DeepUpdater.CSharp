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

        public List<FieldDefinition<TDocument, TField>> GetFieldDefinitions()
        {
            return null;
        }
    }
}
