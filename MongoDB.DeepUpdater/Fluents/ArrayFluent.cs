using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.DeepUpdater
{
    public class ArrayFluent<TDocument, TField> : UpdateFluent<TDocument, TField>
    {
        internal ArrayFluent(TDocument document, IEnumerable<ArrayContainer<TField>> items)
            : base(document)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            Containers = items;
        }

        public FieldFluent<TDocument, TField> Where(Func<TField, bool> filter)
        {
            var newItems = Containers
                .Select(container => createRecursiveContainers(container, filter))
                .SelectMany(x => x);

            //var newItems = new List<SingleContainer<TField>>();
            
            //foreach (var container in Items)
            //{
            //    for (int itemIndex = 0; itemIndex < container.Items.Count; itemIndex++)
            //    {
            //        var item = container.Items[itemIndex];

            //        if (filter(item))
            //        {
            //            var newList = container.UpdateStrings.Concat(new[] { itemIndex.ToString() });

            //            newItems.Add(new SingleContainer<TField>
            //            {
            //                Item = item,
            //                UpdateStrings = newList
            //            });
            //        }
            //    }
            //}

            return new FieldFluent<TDocument, TField>(Document, newItems);
        }

        private IEnumerable<SingleContainer<TField>> createRecursiveContainers(ArrayContainer<TField> container, Func<TField, bool> filter)
        {
            return container.Items
                .Select((item, index) => new { Item = item, Index = index })
                .Where(wrapper => filter(wrapper.Item))
                .Select(wrapper =>
                    {
                        return new SingleContainer<TField>
                        {
                            Item = wrapper.Item,
                            UpdateStrings = container.UpdateStrings.Concat(new[] { wrapper.Index.ToString() })
                        };
                    });
        }

        public IEnumerable<FieldDefinition<TDocument>> GetFieldDefinitions()
        {
            return Containers
                .Select(i => i.UpdateStrings)
                .Select(us => string.Join(".", us))
                .Select(us => (FieldDefinition<TDocument>)us);
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

        internal IEnumerable<ArrayContainer<TField>> Containers;
    }
}
