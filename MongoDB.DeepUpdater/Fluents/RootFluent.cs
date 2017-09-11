using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.DeepUpdater
{
    public class RootFluent<TDocument> : SingleFluent<TDocument, TDocument>
    {
        internal RootFluent(TDocument document)
            : base(document, new List<SingleContainer<TDocument>>())
        {
            var documentContainer = new SingleContainer<TDocument>
            {
                Item = document,
                UpdateStrings = new List<string>()
            };

            Items.Add(documentContainer);
        }
    }
}
