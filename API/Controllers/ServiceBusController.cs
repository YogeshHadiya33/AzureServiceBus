using API.Models;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ServiceBusController : ControllerBase
{
    readonly IConfiguration _configuration;
    readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusSender _serviceBusSender;

    public ServiceBusController(IConfiguration configuration,
        ServiceBusClient serviceBusClient)
    {
        _configuration = configuration;
        _serviceBusClient = serviceBusClient;

        string queueName = _configuration.GetValue<string>("ServiceBusSettings:QueueName");
        _serviceBusSender = _serviceBusClient.CreateSender(queueName);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessageAsync([FromBody] EmployeeModel employee)
    {
        string connectionString = _configuration.GetValue<string>("ServiceBusSettings:ConnectionString");
        string queueName = _configuration.GetValue<string>("ServiceBusSettings:QueueName");

        var client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender(queueName);
        string body = JsonConvert.SerializeObject(employee);
        var message = new ServiceBusMessage(body);
        await sender.SendMessageAsync(message);
        return Ok("Message sent to the Service Bus queue successfully");
    }

    [HttpPost]
    public async Task<IActionResult> SendMessageWithDIAsync([FromBody] EmployeeModel employee)
    {
        string body = JsonConvert.SerializeObject(employee);
        var message = new ServiceBusMessage(body);
        await _serviceBusSender.SendMessageAsync(message);
        return Ok("Message sent to the Service Bus queue successfully using dependency injection");
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleMessageAsync([FromBody] EmployeeModel employee)
    {
        string body = JsonConvert.SerializeObject(employee);
        var message = new ServiceBusMessage(body);

        // Schedule the message to be sent 5 minutes from now
        message.ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddMinutes(5);

        // Schedule the message
        long sequenceNumber = await _serviceBusSender.ScheduleMessageAsync(message, message.ScheduledEnqueueTime);

        return Ok($"Message scheduled to the Service Bus queue successfully. Sequence number: {sequenceNumber}");
    }

    [HttpPost]
    public async Task<IActionResult> CancelScheduledMessageAsync([FromQuery] long sequenceNumber)
    {
        // Cancel the scheduled message using its sequence number
        await _serviceBusSender.CancelScheduledMessageAsync(sequenceNumber);

        return Ok($"Scheduled message with sequence number {sequenceNumber} has been canceled.");
    }
}
