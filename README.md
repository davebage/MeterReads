# MeterReads

## Setting up
This is using entity framework core.

The full API can be tested over swagger.  To do so will require you to set up a local sql server database, the connection string is in appsettings.json. I have made it a trusted account

## Tests
The unit tests also use the same schema, but the context is an In memory database to allow for better control of the test scenarios.

Frameworks used for tests
- nUnit
- FakeItEasy
- FluentAssertions

Folder structure
The web api and test folder structures are the same to give better visibility as to what is being tested.

To create a new validator, add it to the Validators folder and ensure it implements the IMeterReadValidator interface. Register the validator in the DI container.  This will ensure it will be picked up by the API.
