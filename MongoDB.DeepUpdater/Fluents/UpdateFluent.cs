using System;

namespace MongoDB.DeepUpdater
{
    public class UpdateFluent<TDocument, TField>
    {
        public UpdateFluent(TDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            Document = document;
        }

        internal TDocument Document
        {
            get;
        }
    }
}
