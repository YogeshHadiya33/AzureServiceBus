# AzureServiceBus

AzureServiceBus is a .NET 8 project that demonstrates how to interact with Azure Service Bus using a Web API and Azure Function. The project includes methods for sending messages to a queue, scheduling messages, and cancelling scheduled messages. It also includes an Azure Function that receives messages from the queue.

## Features

- Send messages to an Azure Service Bus queue
- Schedule messages for future delivery
- Cancel scheduled messages
- Azure Function that receives messages from the queue

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 8
- Azure Service Bus

### Installation

1. Clone the repo git clone https://github.com/YogeshHadiya33/AzureServiceBus.git
2. Open the project in Visual Studio
3. Build the solution
4. Run the project

## Project Structure

The project consists of two main parts:

1. **API**: This is a .NET 8 Web API that sends messages to an Azure Service Bus queue. It includes methods for sending messages, scheduling messages, and cancelling scheduled messages. The API uses dependency injection to manage the Service Bus client and sender.

2. **ServiceBusReceiver**: This is an Azure Function that is triggered by messages arriving in the Azure Service Bus queue. It logs the message details and then completes the message.

## API Endpoints

The API includes the following endpoints:

- `POST /api/ServiceBus/SendMessageAsync`: Sends a message to the Service Bus queue. The message body should be a JSON object representing an `EmployeeModel`.

- `POST /api/ServiceBus/SendMessageWithDIAsync`: Sends a message to the Service Bus queue using dependency injection. The message body should be a JSON object representing an `EmployeeModel`.

- `POST /api/ServiceBus/ScheduleMessageAsync`: Schedules a message to be sent to the Service Bus queue 5 minutes from now. The message body should be a JSON object representing an `EmployeeModel`.

- `POST /api/ServiceBus/CancelScheduledMessageAsync`: Cancels a scheduled message. The sequence number of the message to be cancelled should be provided as a query parameter.



## Further Reading

For more information, check out the [Integrating Azure Service Bus with .NET Applications](https://www.yogeshhadiya.in/2024/05/integrating-azure-service-bus-with-net.html).
   
