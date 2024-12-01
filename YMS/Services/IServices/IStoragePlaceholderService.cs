using YMS.Models;

namespace YMS.Services.IServices
{
    public interface IStoragePlaceholderService
    {
        Task<StoragePlaceholder> GetStorageByCoilIdAsync(string coilId);
        Task<bool> UpdateStoragePlaceholderAsync(StoragePlaceholder storage);
        Task<bool> MarkStorageAsUnoccupiedAsync(string coilId);
    }
}
