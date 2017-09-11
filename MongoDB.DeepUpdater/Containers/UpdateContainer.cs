using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    internal abstract class UpdateContainer<TItem>
    {
        protected UpdateContainer() { }

        internal List<string> UpdateStrings { get; set; }
    }
}
