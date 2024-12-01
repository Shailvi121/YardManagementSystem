using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YMS.Data;
using YMS.MESModels;

namespace YMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MESController : ControllerBase
    {
        private readonly MesDbContext _mesContext;
        public MESController(MesDbContext mesContext)
        {
            _mesContext = mesContext;
        }

        [HttpGet]
        public async Task<ActionResult<MESCoil>> GetCoilFromMes(string id) {

            return await _mesContext.MESCoils.FirstOrDefaultAsync(c => c.CoilID==id);
        }
    }
}
