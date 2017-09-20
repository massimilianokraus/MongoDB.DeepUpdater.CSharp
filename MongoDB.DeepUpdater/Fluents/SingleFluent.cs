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

        public FieldFluent<TDocument, TNestedField> Select<TNestedField>(Expression<Func<TField, TNestedField>> selectorExpression)
        {
            if (selectorExpression == null) throw new ArgumentNullException(nameof(selectorExpression));

            var selector = selectorExpression.Compile();
            var newUpdateString = GetPropertyName(selectorExpression);

            var nestedItems = Containers.Select(containerItem => createNestedContainer(containerItem, selector, newUpdateString));

            return new FieldFluent<TDocument, TNestedField>(Document, nestedItems);
        }

        private SingleContainer<TNestedField> createNestedContainer<TNestedField>(
            SingleContainer<TField> container, Func<TField, TNestedField> selector, string newUpdateString)
        {
            var newUpdateStrings = container.UpdateStrings.Concat(new[] { newUpdateString });
            var newItem = selector(container.Item);

            return new SingleContainer<TNestedField>(newItem, newUpdateStrings);
        }

        public ArrayFluent<TDocument, TNestedField> SelectArray<TNestedField>(
            Expression<Func<TField, IEnumerable<TNestedField>>> selectorExpression)
        {
            if (selectorExpression == null) throw new ArgumentNullException(nameof(selectorExpression));

            var selector = selectorExpression.Compile();
            var newUpdateString = GetPropertyName(selectorExpression);

            var nestedItems = Containers.Select(containerItem => createNestedContainer(containerItem, selector, newUpdateString));

            return new ArrayFluent<TDocument, TNestedField>(Document, nestedItems);
        }

        private ArrayContainer<TNestedField> createNestedContainer<TNestedField>(
            SingleContainer<TField> container, Func<TField, IEnumerable<TNestedField>> selector, string newUpdateString)
        {
            var newUpdateStrings = container.UpdateStrings.Concat(new[] { newUpdateString });
            var newItems = selector(container.Item);

            return new ArrayContainer<TNestedField>(newItems, newUpdateStrings);
        }

        internal IEnumerable<SingleContainer<TField>> Containers;
    }
}
