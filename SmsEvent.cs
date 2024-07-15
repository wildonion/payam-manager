using System;
using System.Runtime.InteropServices;
using MassTransit;


namespace PayamEvents
{

    public interface IEvent
    {
        DateTime Timestamp { get; }
        string CorrelationId { get; }
    }

    public class PayamEvent : IEvent
    {
        public required string Message { get; set; }
        public required string[] PhoneNumbers { get; set; }
        public DateTime Timestamp { get; set; }
        public string CorrelationId { get; set; }

    }

    public class PayamEventBody
    {
        public required string DeviceId { get; set; }
        public required string DeviceBrand { get; set; }
        public required string DevicePhoneNumber { get; set; }

    }
}