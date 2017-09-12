using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.DeepUpdater
{
    public class ArrayFluent<TDocument, TField> : UpdateFluent<TDocument, TField>
    {
        internal ArrayFluent(TDocument document, List<ArrayContainer<TField>> items)
            : base(document)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            Items = items;
        }

        public FieldFluent<TDocument, TField> Where(Func<TField, bool> filter)
        {
            var newItems = new List<SingleContainer<TField>>();

            foreach (var container in Items)
            {
                for (int itemIndex = 0; itemIndex < container.Items.Count; itemIndex++)
                {
                    var item = container.Items[itemIndex];

                    if (filter(item))
                    {
                        var newList = container.UpdateStrings.ToList();

                        newList.Add(itemIndex.ToString());

                        newItems.Add(new SingleContainer<TField>
                        {
                            Item = item,
                            UpdateStrings = newList
                        });
                    }
                }
            }

            return new FieldFluent<TDocument, TField>(Document, newItems);
        }

        public List<FieldDefinition<TDocument>> GetFieldDefinitions()
        {
            var updateStrings = Items
                .Select(i => i.UpdateStrings)
                .Select(us => string.Join(".", us))
                .ToList();

            return updateStrings.Select(us => (FieldDefinition<TDocument>)us).ToList();
        }

        public UpdateDefinition<TDocument> AddToSet(TField item)
        {
            return Do(Builders<TDocument>.Update.AddToSet, item);
        }

        public UpdateDefinition<TDocument> AddToSetEach(IEnumerable<TField> items)
        {
            return Do(Builders<TDocument>.Update.AddToSetEach, items);
        }

        public UpdateDefinition<TDocument> Push(TField item)
        {
            return Do(Builders<TDocument>.Update.Push, item);
        }

        public UpdateDefinition<TDocument> PushEach(IEnumerable<TField> items, int? slice = null, int? position = null, SortDefinition<TField> sort = null)
        {
            var builder = Builders<TDocument>.Update;

            var fieldDefinitions = GetFieldDefinitions();

            var updateDefinitions = fieldDefinitions.Select(fd => Builders<TDocument>.Update.PushEach(fd, items, slice, position, sort));

            var combined = builder.Combine(updateDefinitions);

            return combined;
        }

        public UpdateDefinition<TDocument> PopFirst()
        {
            return Do(Builders<TDocument>.Update.PopFirst);
        }

        public UpdateDefinition<TDocument> PopLast()
        {
            return Do(Builders<TDocument>.Update.PopLast);
        }

        public UpdateDefinition<TDocument> Pull(TField item)
        {
            return Do(Builders<TDocument>.Update.Pull, item);
        }

        public UpdateDefinition<TDocument> PullAll(IEnumerable<TField> items)
        {
            return Do(Builders<TDocument>.Update.PullAll, items);
        }

        internal UpdateDefinition<TDocument> Do(
            Func<FieldDefinition<TDocument>, UpdateDefinition<TDocument>> updateOperator)
        {
            if (updateOperator == null) throw new ArgumentNullException(nameof(updateOperator));

            var builder = Builders<TDocument>.Update;

            var fieldDefinitions = GetFieldDefinitions();

            var updateDefinitions = fieldDefinitions.Select(updateOperator);

            var combined = builder.Combine(updateDefinitions);

            return combined;
        }

        internal UpdateDefinition<TDocument> Do(
            Func<FieldDefinition<TDocument>, TField, UpdateDefinition<TDocument>> updateCreator,
            TField item)
        {
            if (updateCreator == null) throw new ArgumentNullException(nameof(updateCreator));

            var builder = Builders<TDocument>.Update;

            var fieldDefinitions = GetFieldDefinitions();

            var updateDefinitions = fieldDefinitions.Select(fd => updateCreator(fd, item));

            var combined = builder.Combine(updateDefinitions);

            return combined;
        }

        internal UpdateDefinition<TDocument> Do(
            Func<FieldDefinition<TDocument>, IEnumerable<TField>, UpdateDefinition<TDocument>> updateOperator,
            IEnumerable<TField> items)
        {
            if (updateOperator == null) throw new ArgumentNullException(nameof(updateOperator));

            var builder = Builders<TDocument>.Update;

            var fieldDefinitions = GetFieldDefinitions();

            var updateDefinitions = fieldDefinitions.Select(fd => updateOperator(fd, items));

            var combined = builder.Combine(updateDefinitions);

            return combined;
        }

        internal List<ArrayContainer<TField>> Items;
    }
}
