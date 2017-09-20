using System.Collections.Generic;

namespace MongoDB.DeepUpdater
{
    public class RootFluent<TDocument> : SingleFluent<TDocument, TDocument>
    {
        private static IEnumerable<SingleContainer<TDocument>> createRootContainersList(TDocument document)
        {
            return new[]
            {
                new SingleContainer<TDocument>
                {
                    Item = document,
                    UpdateStrings = new List<string>()
                }
            };
        }

        internal RootFluent(TDocument document)
            : base(document, createRootContainersList(document))
        { }
    }
}
