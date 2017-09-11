using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDB.DeepUpdater
{
    public class SingleFluent<TDocument, TField> : UpdateFluent<TDocument, TField>
    {
        internal SingleFluent(TDocument document, List<SingleContainer<TField>> items)
            : base(document)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            Items = items;
        }

        internal List<FieldDefinition<TDocument, TField>> InternalGetFieldDefinitions()
        {
            var updateStrings = Items
                .Select(i => i.UpdateStrings)
                .Select(us => string.Join(".", us))
                .ToList();

            return updateStrings.Select(us => (FieldDefinition<TDocument, TField>)us).ToList();
        }

        public FieldFluent<TDocument, TNestedField> Select<TNestedField>(Expression<Func<TField, TNestedField>> selectorExpression)
        {
            if (selectorExpression == null) throw new ArgumentNullException(nameof(selectorExpression));

            var selector = selectorExpression.Compile();
            var newUpdateString = GetPropertyName(selectorExpression);

            var nestedItems = Items
                .Select(containerItem => new SingleContainer<TNestedField>
                    {
                        Item = selector(containerItem.Item),
                        UpdateStrings = containerItem.UpdateStrings.Union(new[] { newUpdateString }).ToList(),
                    })
                .ToList();

            return new FieldFluent<TDocument, TNestedField>(Document, nestedItems);
        }

        public ArrayFluent<TDocument, TNestedField> SelectArray<TNestedField>(Expression<Func<TField, IEnumerable<TNestedField>>> selectorExpression)
        {
            if (selectorExpression == null) throw new ArgumentNullException(nameof(selectorExpression));

            var selector = selectorExpression.Compile();
            var newUpdateString = GetPropertyName(selectorExpression);

            var nestedItems = Items
                .Select(containerItem => new ArrayContainer<TNestedField>
                    {
                        Items = selector(containerItem.Item).ToList(),
                        UpdateStrings = containerItem.UpdateStrings.Union(new[] { newUpdateString }).ToList()
                    })
                .ToList();

            return new ArrayFluent<TDocument, TNestedField>(Document, nestedItems);
        }

        internal List<SingleContainer<TField>> Items;
    }
}
