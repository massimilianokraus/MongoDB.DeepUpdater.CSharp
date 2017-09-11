using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.DeepUpdater
{
    public class RootFluent<TDocument> : SingleFluent<TDocument, TDocument>
    {
        public RootFluent(TDocument document)
            : base(document)
        { }
    }
}
