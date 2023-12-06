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
7. Define a Database assembly using entity framework core orm with: entities, configuration, automapper, repository.
8. Cover Mapper with unit-test.

### Next:

- Define Executor with application entrypoint.
- Define and extract configurations.
- Cover code with exception handling.
- Cover code with logging.

### Additional:

1. Collect messages if RabbitMQ connection lost. (XmlParser level.)
2. Collect messages if database connection lost. (DataProcessor level.)
3. Ability to setup xml files folder with configuration. (Executor level.)
4. Check that xml-files folder is not empty. (Executor level.)