namespace NetBlaze.Application.Interfaces.General
{
    public interface IMemoryCacheRepository
    {
        bool SetItemInCache<TItem, TKey>(TKey key, TItem item, int expiryInDays = 1) where TKey : struct;

        TItem? GetItemFromCache<TItem, TKey>(TKey key) where TKey : struct;
    }
}
