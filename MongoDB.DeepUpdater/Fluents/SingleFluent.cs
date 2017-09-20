using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDB.DeepUpdater
{
    public class SingleFluent<TDocument, TField> : UpdateFluent<TDocument, TField>
    {
        internal SingleFluent(TDocument document, IEnumerable<SingleContainer<TField>> items)
            : base(document)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            Containers = items;
        }

        internal IEnumerable<FieldDefinition<TDocument, TField>> InternalGetFieldDefinitions()
        {
            return Containers
                .Select(i => i.UpdateStrings)
                .Select(us => string.Join(".", us))
                .Select(us => (FieldDefinition<TDocument, TField>)us);
        }

        public FieldFluent<TDocument, TNestedField> Select<TNestedField>(Expression<Func<TField, TNestedField>> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            var newUpdateString = GetPropertyName(selector);

            var nestedItems = Containers
                .Select(containerItem => new SingleContainer<TNestedField>
                    {
                        Item = selector.Compile()(containerItem.Item),
                        UpdateStrings = containerItem.UpdateStrings.Concat(new[] { newUpdateString }),
                    });

            return new FieldFluent<TDocument, TNestedField>(Document, nestedItems);
        }

        public ArrayFluent<TDocument, TNestedField> SelectArray<TNestedField>(Expression<Func<TField, IEnumerable<TNestedField>>> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            var newUpdateString = GetPropertyName(selector);



            var nestedItems = Containers
                .Select(containerItem => new ArrayContainer<TNestedField>
                    {
                        Items = selector.Compile()(containerItem.Item),
                        UpdateStrings = containerItem.UpdateStrings.Concat(new[] { newUpdateString })
                    });

            return new ArrayFluent<TDocument, TNestedField>(Document, nestedItems);
        }

        internal IEnumerable<SingleContainer<TField>> Containers;
    }
}
