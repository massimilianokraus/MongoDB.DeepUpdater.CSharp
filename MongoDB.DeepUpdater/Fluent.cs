using System;
using System.Linq.Expressions;

namespace MongoDB.DeepUpdater
{
    public class Fluent<TDocument>
    {
        public Fluent(TDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));
            _document = document;
        }

        //public Fluent<TDocument> Select(Expression<Func<TDocument, TItem>> )

        private TDocument _document;
    }
}
