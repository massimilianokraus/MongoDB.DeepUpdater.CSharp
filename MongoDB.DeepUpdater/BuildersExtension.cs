using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.DeepUpdater
{
    public static class BuildersExtension
    {
        public static Fluent<TDocument> Deep<TDocument>(
            this UpdateDefinitionBuilder<TDocument> This,
            TDocument document)
        {
            return new Fluent<TDocument>(document);
        }
    }
}
