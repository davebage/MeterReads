# MeterReads

## Setting up
This is using entity framework core code first approach.

Two basic entities:
- Customer Accounts store the customer accounts (of course)
- Meter reads



The full API can be tested over swagger.  To do so will require you to set up a local sql server database, the connection string is in appsettings.json. I have made it a trusted account

## Tests
The unit tests also use the same schema, but the context is an In memory database to allow for better control of the test scenarios and will therefore not alter the main database when testing.

Frameworks used for tests
- nUnit
- FakeItEasy
- FluentAssertions

If creating a new test which will require access to the in memory Db context, ensure the test file derives from the WithInMemoryContext abstract class
Folder structure
The web api and test folder structures are the same to give better visibility as to what is being tested.

To create a new validator, add it to the Validators folder and ensure it implements the IMeterReadValidator interface. Register the validator in the DI container.  This will ensure it will be picked up by the API.

## Validation classes
Each file performs its own validation
- MeterReadAlreadyExistsValidator - performs a check to see if there is a entity in the database which matches the model on AccountId, MeterReadDateTime and value.  Returns false (to reject) if the entry is present already
- MeterReadHasValidCustomerAccountValidator - checks to see if the account id on the model already exists in the database.  Rejects if the account is not found
- MeterReadIsNewerValidator - checks to see if there are any newer meter reads in the database.  Rejects if there are newer reads already stored
- MeterReadValueValidator - checks to see if the meter read value is less than 0 (Zero) or greater than 9,9999

