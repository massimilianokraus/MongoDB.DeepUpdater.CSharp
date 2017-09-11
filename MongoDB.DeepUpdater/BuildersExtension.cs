using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.DeepUpdater
{
    public static class BuildersExtension
    {
        public static FieldFluent<TDocument, TDocument> Deep<TDocument>(
            this UpdateDefinitionBuilder<TDocument> This,
            TDocument document)
        {
            return null; // new BaseSingleUpdateFluent<TDocument, TDocument>(document);
        }
    }
}
