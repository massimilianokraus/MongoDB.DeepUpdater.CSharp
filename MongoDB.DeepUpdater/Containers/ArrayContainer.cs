using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    internal class ArrayContainer<TItem> : UpdateContainer<TItem>
    {
        internal List<TItem> Items { get; set; }
    }
}
