using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MongoDB.DeepUpdater
{
    public class WhereFluent<TDocument, TField> : SingleFluent<TDocument, TField>
    {
        public WhereFluent(TDocument document)
            : base(document)
        { }

        public SingleFluent<TDocument, TNestedField> Select<TNestedField>(Expression<Func<TField, TNestedField>> selectExpression)
        {
            return null;
        }
    }
}
