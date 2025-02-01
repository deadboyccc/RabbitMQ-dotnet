# RabbitMQ Playground

This repository serves as a playground for testing and learning various RabbitMQ patterns, including Pub/Sub, Competing Consumers, and exploring different routing options by topic, key, and more. It is designed to help developers of beginner to intermediate level build a deeper understanding of RabbitMQ messaging patterns and practices.

## RabbitMQ Patterns

### 1. **Pub/Sub (Publish/Subscribe)**
- **Objective**: Learn how to broadcast messages to multiple consumers.
- **Key Concepts**: Exchanges, Queues, Bindings.

### 2. **Competing Consumers**
- **Objective**: Demonstrate multiple consumers processing messages from the same queue.
- **Key Concepts**: Consumers, Load balancing.

### 3. **Routing by Topic**
- **Objective**: Learn how to use topic exchanges to route messages based on patterns.
- **Key Concepts**: Topic Exchanges, Routing Keys.

### 4. **Routing by Key**
- **Objective**: Learn how to use direct exchanges with specific routing keys.
- **Key Concepts**: Direct Exchanges, Routing Keys.

### 5. **Fanout Exchange**
- **Objective**: Broadcast messages to all queues bound to a fanout exchange.
- **Key Concepts**: Fanout Exchange, Broadcasting.

### 6. **Dead Letter Exchanges (DLX)**
- **Objective**: Learn how to handle messages that cannot be delivered or processed.
- **Key Concepts**: Dead Letter Exchanges, Message TTL, Redelivery.

### 7. **Delayed Message Exchanges**
- **Objective**: Learn how to schedule messages to be delivered after a delay.
- **Key Concepts**: Delayed Exchanges, Message Expiry.

### 8. **Header Exchange**
- **Objective**: Route messages based on header values instead of routing keys.
- **Key Concepts**: Header Exchanges, Message Headers.

## Contributing

Feel free to submit pull requests or create issues if you have suggestions or improvements for the project!
