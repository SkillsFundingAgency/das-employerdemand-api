## ⛔ Never push sensitive information such as client id's, secrets or keys into repositories including in the README file ⛔

# Employer Demand API

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">


[![Build Status](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_apis/build/status/das-employerdemand-api?repoName=SkillsFundingAgency%2Fdas-employerdemand-api&branchName=refs%2Fpull%2F16%2Fmerge)](https://dev.azure.com/sfa-gov-uk/Digital%20Apprenticeship%20Service/_build/latest?definitionId=2388&repoName=SkillsFundingAgency%2Fdas-employerdemand-api&branchName=refs%2Fpull%2F16%2Fmerge)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-employerdemand-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=SkillsFundingAgency_das-employerdemand-api)
[![Jira Project](https://img.shields.io/badge/Jira-Project-blue)](https://skillsfundingagency.atlassian.net/secure/RapidBoard.jspa?rapidView=664)
[![Confluence Project](https://img.shields.io/badge/Confluence-Project-blue)](https://skillsfundingagency.atlassian.net/wiki/spaces/NDL/pages/2393178481/AED)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

The Employer Demand API is responsible for storing an Employers request for training in an unavailable area for a Training Provider to pick up. The demand is created against a particular course taken from the [courses-api](https://github.com/skillsfundingagency/das-courses-api) and a location, which then a number of providers can then enquire about providing training for that employer

## How It Works

The Employer Demand API is a inner API consumed by [das-apim-endpoints](https://github.com/skillsfundingagency/das-apim-endpoints). All data is stored in a SQL database using EF Core and configuration is read from Azure Storage Configuration table. 

## 🚀 Installation

### Pre-Requisites

* A clone of this repository
* A code editor that supports Azure functions and .NetCore 3.1
* Azure Storage Emulator if not running in DEV mode
* SQL Database if not running DEV mode

### Config

AppSettings.json file
```json
{
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true;",
    "ConfigNames": "SFA.DAS.EmployerDemand.Api",
    "EnvironmentName": "LOCAL",
    "Version": "1.0",
    "APPINSIGHTS_INSTRUMENTATIONKEY": ""
  }  
```

Azure Table Storage config

Row Key: SFA.DAS.EmployerDemand.Api._1.0

Partition Key: LOCAL

Data:

```json
{
  "EmployerDemandConfiguration": {
    "ConnectionString": "Data Source=.;Initial Catalog=SFA.DAS.EmployerDemand;Integrated Security=True;Pooling=False;Connect Timeout=30"
  },
  "AzureAd": {
    "tenant": "",
    "identifier": ""
  }
}
```

## Local Running

### In memory database
It is possible to run the whole of the API using the InMemory database. To do this the environment variable in appsettings.json should be set to DEV. Once done, start the application as normal. You will then be able to query the API as per the operations listed in swagger.

### SQL Server database
You are able to run the API by doing the following:

* Run the database deployment publish command to create the database SFA.DAS.EmployerDemand or create the database manually and run in the table creation scripts
* In your Azure Storage Account, create the configuration as detailed above.
* Start the API project - this will load the swagger definition with all endpoints defined.

## 🔗 External Dependencies

Authentication is managed via Azure Managed Identity when not running locally.

## Technologies

* .NetCore 3.1
* EF Core
* SQL Server
* NLog
* Azure Table Storage
* NUnit
* Moq
* FluentAssertions

## 🐛 Known Issues

* Do not run in IISExpress