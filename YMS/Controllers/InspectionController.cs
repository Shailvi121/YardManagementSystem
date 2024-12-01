using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YMS.Data;
using YMS.Dtos;
using YMS.Models;
using YMS.Services.IServices;

namespace YMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        public InspectionController(ApplicationDbContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<Inspection>> SaveInspectedData([FromBody] InspectionDto inspect)
        {
            //// Get the current user's ID as a string
            //string inspectorIdString = _userService.GetCurrentUserId();

            //// Convert the string to an integer. If the conversion fails, handle it appropriately.
            //if (!int.TryParse(inspectorIdString, out int inspectorId))
            //{
            //    // If conversion fails, return a bad request or error
            //    return BadRequest("Invalid inspector ID.");
            //}

            // Create the inspection based on the request
            var inspectedCoil = new Inspection
            {
                CoilID = inspect.CoilID,  // This should exist in the Coils table
                InspectorID = inspect.InspectorID, // The inspector is the current user (as an int)
                InspectionDate = DateTime.Now,   // The current inspection date
                Weight = inspect.Weight,
                Width = inspect.Width,
                Diameter = inspect.Diameter,
                VisualCondition = inspect.VisualCondition,
                Remark1 = inspect.Remark1,
                Remark2 = inspect.Remark2,
                Remark3 = inspect.Remark3,
                ImagePaths = inspect.ImagePaths,
              // Only set CoilID, omit currentLocation and other properties
            };

            // Add the Inspection entity to the context
            await _context.Inspections.AddAsync(inspectedCoil);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(inspectedCoil);  // Return the saved inspection
        }



    }
}
