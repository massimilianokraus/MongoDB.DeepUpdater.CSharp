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

        internal List<ArrayContainer<TField>> Items;
    }
}
