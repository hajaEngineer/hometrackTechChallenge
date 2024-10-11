# Purgomalum API Automation

This project automates the testing of the Purgomalum API, a service used to filter profanity from text. The project is developed using C#, Xunit as the testing framework, and SpecFlow for behavior-driven development (BDD).

## Table of Contents

- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Tests](#running-the-tests)
- [Endpoints Tested](#endpoints-tested)
- [Customization](#customization)
- [Contributing](#contributing)
- [License](#license)

## Project Structure

- *Models*: Contains the model classes representing API responses.
- *Services*: Contains the service class responsible for making API calls to the Purgomalum API.
- *Steps*: Contains step definitions for SpecFlow tests.
- *Features*: Contains feature files written in Gherkin for BDD-style tests.

### Key Classes:

- PurgomalumService.cs: Contains methods for interacting with the Purgomalum API.
- PurgomalumSteps.cs: Step definitions for the SpecFlow scenarios.
- ProfanityCheckJsonResponse.cs: Represents the JSON response model.
- ContainsProfanityResponse.cs: Represents the response for the containsprofanity endpoint.

## Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 6 or later](https://dotnet.microsoft.com/download)
- [Xunit](https://xunit.net/)
- [SpecFlow](https://specflow.org/)
- Any IDE that supports C# (e.g., Visual Studio or JetBrains Rider)
