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
8. Cover Mapper with unit-test with followed fix CombinedStatus to be non abstract.
9. Make ProcessMessage async with using the Database Repository for save instrument-statuses.
10. Cover Database.Repository.SaveOrUpdateInstrumentStatusAsync with unit-test with followed fix RapidControlStatusEntity to be without ModuleState property.
11. Define an Executor assembly with application entrypoint.
12. Resolve some database scheme mistakes. 
    - Full scheme save still not worked in reason of not resolved Discriminator at json serialization step and mapper and not finished repository save and update logic.
13. Skip the "Simplify database implementation to meet requirements" and resolve full structure database scheme with save and update records.

### Next:

- Define and extract configurations.
- Cover code with exception handling.
- Cover code with logging.

### Additional possible improvements:

- Include Executor to FullCycle integration test.
- Collect messages if RabbitMQ connection lost. (XmlParser level.)
- Collect messages if database connection lost. (DataProcessor level.)
- Ability to setup xml files folder with configuration. (Executor level.)
- Check that xml-files folder is not empty. (Executor level.)