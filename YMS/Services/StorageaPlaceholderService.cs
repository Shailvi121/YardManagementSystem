using Microsoft.EntityFrameworkCore;
using YMS.Data;
using YMS.Models;
using YMS.Services.IServices;

namespace YMS.Services
{

    public class StoragePlaceholderService : IStoragePlaceholderService
    {
        private readonly ApplicationDbContext _context;

        public StoragePlaceholderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StoragePlaceholder> GetStorageByCoilIdAsync(string coilId)
        {
            // Simplify by directly searching the occupying coil
            return await _context.StoragePlaceholders
                .FirstOrDefaultAsync(sp => sp.OccupyingCoilID == coilId);
        }

        public async Task<bool> UpdateStoragePlaceholderAsync(StoragePlaceholder storage)
        {
            _context.StoragePlaceholders.Update(storage);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkStorageAsUnoccupiedAsync(string coilId)
        {
            var storage = await GetStorageByCoilIdAsync(coilId);
            if (storage == null) return false; // No such storage found

            storage.IsOccupied = false;
            storage.OccupyingCoilID = null;
            await UpdateStoragePlaceholderAsync(storage);
            return true;
        }
    }

}
