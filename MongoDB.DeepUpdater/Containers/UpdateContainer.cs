using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    internal abstract class UpdateContainer<TItem>
    {
        protected UpdateContainer() { }

        internal IEnumerable<string> UpdateStrings { get; set; }
    }
}
