using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.DeepUpdater
{
    public class OperationFluent<TDocument, TField> : UpdateFluent<TDocument, TField>
    {
        public OperationFluent(TDocument document)
            : base(document)
        { }



    }
}
