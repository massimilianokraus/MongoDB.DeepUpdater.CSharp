namespace MongoDB.DeepUpdater
{
    internal class SingleContainer<TItem> : UpdateContainer<TItem>
    {
        internal TItem Item { get; set; }
    }
}
