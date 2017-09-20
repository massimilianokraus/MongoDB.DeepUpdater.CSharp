using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.DeepUpdater
{
    public class FieldFluent<TDocument, TField> : SingleFluent<TDocument, TField>
    {
        internal FieldFluent(TDocument document, IEnumerable<SingleContainer<TField>> items)
            : base(document, items)
        { }

        public IEnumerable<FieldDefinition<TDocument, TField>> GetFieldDefinitions()
        {
            return InternalGetFieldDefinitions();
        }

        public UpdateDefinition<TDocument> Set(TField item)
        {
            return Do(Builders<TDocument>.Update.Set, item);
        }

        public UpdateDefinition<TDocument> Inc(TField item)
        {
            return Do(Builders<TDocument>.Update.Inc, item);
        }

        public UpdateDefinition<TDocument> Mul(TField item)
        {
            return Do(Builders<TDocument>.Update.Mul, item);
        }

        public UpdateDefinition<TDocument> Max(TField item)
        {
            return Do(Builders<TDocument>.Update.Max, item);
        }

        public UpdateDefinition<TDocument> Min(TField item)
        {
            return Do(Builders<TDocument>.Update.Min, item);
        }

        public UpdateDefinition<TDocument> BitwiseAnd(TField item)
        {
            return Do(Builders<TDocument>.Update.BitwiseAnd, item);
        }

        public UpdateDefinition<TDocument> BitwiseOr(TField item)
        {
            return Do(Builders<TDocument>.Update.BitwiseOr, item);
        }

        public UpdateDefinition<TDocument> BitwiseXor(TField item)
        {
            return Do(Builders<TDocument>.Update.BitwiseXor, item);
        }

        internal UpdateDefinition<TDocument> Do(
            Func<FieldDefinition<TDocument, TField>, TField, UpdateDefinition<TDocument>> updateCreator,
                TField item)
        {
            if (updateCreator == null) throw new ArgumentNullException(nameof(updateCreator));
            
            var builder = Builders<TDocument>.Update;

            var fieldDefinitions = GetFieldDefinitions();

            var updateDefinitions = fieldDefinitions.Select(fd => updateCreator(fd, item));

            var combined = builder.Combine(updateDefinitions);

            return combined;
        }

        public UpdateDefinition<TDocument> CurrentDate(UpdateDefinitionCurrentDateType? type = null)
        {
            var builder = Builders<TDocument>.Update;

            var fieldDefinitions = GetFieldDefinitions();

            var updateDefinitions = fieldDefinitions.Select(fd => builder.CurrentDate(fd, type));

            var combined = builder.Combine(updateDefinitions);

            return combined;
        }
    }
}
