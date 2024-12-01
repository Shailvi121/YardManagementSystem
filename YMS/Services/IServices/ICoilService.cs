using Microsoft.AspNetCore.Mvc;
using YMS.Dtos;
using YMS.Models;

namespace YMS.Services.IServices
{
  
    public interface ICoilService
    {
        Task<IEnumerable<Coil>> GetAllCoilsAsync();
        Task<Coil> GetCoilByIdAsync(string id);
        Task SaveCoilAsync(CoilDto coilDto);
        Task<bool> UpdateCoilAsync(Coil coil);
        Task<bool> DeleteCoilAsync(string id);
    }

}
