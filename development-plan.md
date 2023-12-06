## Development Plan:

### Passed:

1. Initialize XmlParser microservice.
2. Improve XmlParser with parsing of RapidControlStatus section.
3. Initialize RabbitMQ for microservices communication:

   1. Define RabbitMQ project with RabbitMqClient.
   2. Add message publish and messages subscriber methods.
   3. Use RabbitMqClient at XmlParser microservice.
   4. Refactoring: Define tests units.

4. Initialize DataProcessor microservice.
5. Define a full cycle integration test.
6. DataProcessor.ProcessMessage: Deserialize message.

### Next:

- Define a database scheme.
- Define DataProcessor.SqlClient.

- Define Executor application entrypoint.
- Define and extract configurations.
- Cover code with exception handling.
- Cover code with logging.

### Additional:

1. Collect messages if connection lost.