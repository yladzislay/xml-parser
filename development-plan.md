## Development Plan:
### Passed:
1. Initialize XmlParser microservice.
2. Improve XmlParser with parsing of RapidControlStatus section.
3. Initialize RabbitMQ for microservices communication:
   1. Define RabbitMQ project with RabbitMqClient.
   2. Add message publish and messages subscriber methods.
   3. Use RabbitMqClient at XmlParser microservice.
   4. Refactoring: Define tests units.

### Next:
1. Initialize DataProcessor microservice.
2. Define a full cycle integration test.
3. Add a full cycle integration test.
4. Define and extract configurations.

### Additional:
1. Collect messages if connection lost.