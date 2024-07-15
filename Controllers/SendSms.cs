


using System.Text.RegularExpressions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PayamEvents;


namespace SendPayam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendPayamController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        private readonly AppDbContext _context;
        private readonly payam _payam;

        public SendPayamController(IPublishEndpoint publishEndpoint, AppDbContext context, payam payam)
        {
            _publishEndpoint = publishEndpoint;
            _context = context;
            _payam = payam;
        }

        [HttpPost]
        public async Task<IActionResult> SendPayam([FromBody] PayamEventBody message)
        {

            // Retrieve the device action info from the database
            var deviceAction = await _context.DeviceActions
                .FirstOrDefaultAsync(da => da.Brand == message.DeviceBrand);

            if (deviceAction == null)
            {
                return NotFound("Device not found.");
            }

            // Parse the actions JSON
            var actions = JsonConvert.DeserializeObject<Dictionary<string, object>>(deviceAction.Actions.RootElement.ToString());

            // Sort the keys
            var sortedKeys = actions.Keys.OrderBy(k => k).ToList();

            // Replace the placeholders in the schema
            string schema = deviceAction.Schema;
            string processedMessage = Regex.Replace(schema, @"{p(\d+)}", match =>
            {
                int index = int.Parse(match.Groups[1].Value) - 1;
                if (index >= 0 && index < sortedKeys.Count)
                {
                    string key = sortedKeys[index];
                    return actions.ContainsKey(key) ? actions[key].ToString() : match.Value;
                }
                return match.Value;
            });

            Console.WriteLine($"Action: {deviceAction.Actions.RootElement.ToString()}");
            Console.WriteLine($"Message: {processedMessage} with schema: {schema}");
            // Build the PayamEvent instance
            var messageInfo = new PayamEvent
            {
                Message = processedMessage,
                PhoneNumbers = new string[] { message.DevicePhoneNumber },
                Timestamp = DateTime.UtcNow,
                CorrelationId = Guid.NewGuid().ToString()
            };

            var result = await _payam.ProducePayam(messageInfo, _publishEndpoint);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetHealth()
        {
            return Ok("healthy");
        }
    }
}