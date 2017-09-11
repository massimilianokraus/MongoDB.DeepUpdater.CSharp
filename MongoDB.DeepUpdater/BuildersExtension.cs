using MongoDB.Driver;

namespace MongoDB.DeepUpdater
{
    public static class BuildersExtension
    {
        public static RootFluent<TDocument> Deep<TDocument>(
            this UpdateDefinitionBuilder<TDocument> This,
            TDocument document)
        {
            return new RootFluent<TDocument>(document);
        }
    }
}
