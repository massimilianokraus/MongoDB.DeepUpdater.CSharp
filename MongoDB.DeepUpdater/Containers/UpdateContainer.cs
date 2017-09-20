using System;
using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    internal abstract class UpdateContainer<TItem>
    {
        protected UpdateContainer(IEnumerable<string> updateStrings)
        {
            UpdateStrings = updateStrings ?? throw new ArgumentNullException(nameof(updateStrings));
        }

        internal IEnumerable<string> UpdateStrings { get; }
    }
}
