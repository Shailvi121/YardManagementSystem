using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YMS.Data;
using YMS.Dtos;
using YMS.Models;
using YMS.Services.IServices;

namespace YMS.Services
{
   
    public class CoilService : ICoilService
    {
        private readonly ApplicationDbContext _context;
        private readonly MesDbContext _mesContext;
        private readonly IStoragePlaceholderService _storageService;

        public CoilService(ApplicationDbContext context,MesDbContext mesContext ,IStoragePlaceholderService storageService)
        {
            _context = context;
            _storageService = storageService;
            _mesContext = mesContext;
        }

        public async Task<IEnumerable<Coil>> GetAllCoilsAsync()
        {
            // Only include storage info when necessary
            return await _context.Coils.ToListAsync();
        }

        public async Task<Coil> GetCoilByIdAsync(string id)
        {
            var coil = await _context.Coils
                .Include(c => c.CurrentLocation)
                .FirstOrDefaultAsync(c => c.CoilID == id);

            return coil;
        }


        public async Task SaveCoilAsync([FromBody] CoilDto coilDto)
        {
            if (coilDto.CoilID == "string" && coilDto.CoilID == null)
            {
                throw new ArgumentNullException(nameof(coilDto), "Coil data cannot be null.");
            }

            // Create a new Coil entity from the request data
            var coilToAdd = new Coil
            {
                CoilID = coilDto.CoilID,
                CoilBarcodeID = coilDto.CoilBarcodeID,
                MaterialType = coilDto.MaterialType,
                Weight = coilDto.Weight,
                Width = coilDto.Width,
                Diameter = coilDto.Diameter,
                ProductionDate = coilDto.ProductionDate,
                Status = "Received", // Default status, could be parameterized if needed
                LastMoved = DateTime.Now,
               
            };

            // Add the Coil entity to the context
            await _context.Coils.AddAsync(coilToAdd);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateCoilAsync(Coil coil)
        {
            var existingCoil = await _context.Coils.FindAsync(coil.CoilID);
            if (existingCoil == null) return false; // Coil not found

            // Check if the location has changed
            if (coil.CurrentLocationID != existingCoil.CurrentLocationID)
            {
                // Unoccupy previous location if any
                if (existingCoil.CurrentLocationID.HasValue)
                {
                    await _storageService.MarkStorageAsUnoccupiedAsync(existingCoil.CoilID);
                }

                // Occupy the new location if applicable
                if (coil.CurrentLocationID.HasValue)
                {
                    var newStorage = await _context.StoragePlaceholders.FindAsync(coil.CurrentLocationID.Value);
                    if (newStorage?.IsOccupied == true)
                    {
                        return false; // New storage is already occupied
                    }

                    newStorage.IsOccupied = true;
                    newStorage.OccupyingCoilID = coil.CoilID;
                    _context.StoragePlaceholders.Update(newStorage);
                }
            }

            _context.Entry(existingCoil).CurrentValues.SetValues(coil);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCoilAsync(string id)
        {
            var coil = await GetCoilByIdAsync(id);
            if (coil == null) return false; // Coil not found

            // Unoccupy the storage before deleting
            if (coil.CurrentLocationID.HasValue)
            {
                await _storageService.MarkStorageAsUnoccupiedAsync(coil.CoilID);
            }

            _context.Coils.Remove(coil);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}
