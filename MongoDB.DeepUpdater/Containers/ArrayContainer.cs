using System;
using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    internal class ArrayContainer<TItem> : UpdateContainer<TItem>
    {
        internal ArrayContainer(IEnumerable<TItem> items, IEnumerable<string> updateStrings)
            : base(updateStrings)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        internal IEnumerable<TItem> Items { get; }
    }
}
