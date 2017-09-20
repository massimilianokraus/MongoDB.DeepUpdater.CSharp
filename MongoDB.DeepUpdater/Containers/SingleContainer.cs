using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    internal class SingleContainer<TItem> : UpdateContainer<TItem>
    {
        internal SingleContainer(TItem item, IEnumerable<string> updateStrings)
            : base(updateStrings)
        {
            Item = item;
        }

        internal TItem Item { get; }
    }
}
