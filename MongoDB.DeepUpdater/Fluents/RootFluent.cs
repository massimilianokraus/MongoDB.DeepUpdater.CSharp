using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    public class RootFluent<TDocument> : SingleFluent<TDocument, TDocument>
    {
        internal RootFluent(TDocument document)
            : base(document, new[] { new SingleContainer<TDocument>(document, new List<string>()) })
        { }
    }
}
