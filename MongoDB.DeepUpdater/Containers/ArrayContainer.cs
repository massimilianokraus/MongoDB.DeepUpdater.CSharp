using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    internal class ArrayContainer<TItem> : UpdateContainer<TItem>
    {
        internal IEnumerable<TItem> Items { get; set; }
    }
}
