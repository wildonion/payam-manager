


// 'async void' can also be used for specific use cases, such as events:
// public async void SendVerifyButton_Click(object sender, EventArgs e)  
using System.Numerics;
using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using PayamEvents;


public class payam
{
    private readonly AppDbContext _context;

    public payam(AppDbContext context)
    {
        _context = context;
    }


   public async Task<string> ProducePayam(PayamEvent payam, IPublishEndpoint publishEndpoint)
    {
        try
        {
            await publishEndpoint.Publish(payam);
            return "Message published successfully.";
        }
        catch (Exception ex)
        {
            // Log the exception (you might want to use a logging framework here)
            // For example: _logger.LogError(ex, "Failed to publish SMS event");

            return $"Failed to publish message: {ex.Message}";
        }
    }

}