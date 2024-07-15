using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payam.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace payam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceActionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeviceActionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DeviceAction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceAction>>> GetDeviceActions()
        {
            return await _context.DeviceActions.ToListAsync();
        }

        // GET: api/DeviceAction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceAction>> GetDeviceAction(int id)
        {
            var deviceAction = await _context.DeviceActions.FindAsync(id);

            if (deviceAction == null)
            {
                return NotFound();
            }

            return deviceAction;
        }

        // POST: api/DeviceAction
        [HttpPost]
        public async Task<ActionResult<DeviceAction>> PostDeviceAction(DeviceActionBody deviceActionBody)
        {
            var deviceAction = new DeviceAction
            {
                Brand = deviceActionBody.Brand,
                // Actions = JsonDocument.Parse(deviceActionBody.Actions)
                Actions = deviceActionBody.Actions,
                Schema = deviceActionBody.Schema
            };

            _context.DeviceActions.Add(deviceAction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeviceAction), new { id = deviceAction.Id }, deviceAction);
        }

        // PUT: api/DeviceAction/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceAction(int id, DeviceActionBody deviceActionBody)
        {
            var existingDeviceAction = await _context.DeviceActions.FindAsync(id);
            if (existingDeviceAction == null)
            {
                return NotFound(new { message = "DeviceAction not found" });
            }

            existingDeviceAction.Brand = deviceActionBody.Brand;
            // existingDeviceAction.Actions = JsonDocument.Parse(deviceActionBody.Actions);
            existingDeviceAction.Actions = deviceActionBody.Actions;
            existingDeviceAction.Schema = deviceActionBody.Schema;

            _context.Entry(existingDeviceAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceActionExists(id))
                {
                    return NotFound(new { message = "DeviceAction no longer exists" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "DeviceAction Updated successfully" } );
        }

        // DELETE: api/DeviceAction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceAction(int id)
        {
            var deviceAction = await _context.DeviceActions.FindAsync(id);
            if (deviceAction == null)
            {
                return NotFound(new { message = "DeviceAction not found" });
            }

            _context.DeviceActions.Remove(deviceAction);
            await _context.SaveChangesAsync();

            return Ok(new { message = "DeviceAction deleted successfully" });
        }

        private bool DeviceActionExists(int id)
        {
            return _context.DeviceActions.Any(e => e.Id == id);
        }
    }
}
