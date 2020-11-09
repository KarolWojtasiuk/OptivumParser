# OptivumParser
Optivum lesson plans parser, the project consists of a core library, tests and api.

![Nuget](https://img.shields.io/nuget/v/OptivumParser?style=for-the-badge)
![Build Status](https://img.shields.io/github/workflow/status/KarolWojtasiuk/OptivumParser/.NET%20Core/master?style=for-the-badge)
![License](https://img.shields.io/github/license/KarolWojtasiuk/OptivumParser?style=for-the-badge)

## Usage
These instructions will help you use this library in your project.

### Prerequisites
> [.NET Standard 2.0](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

### Installing
Go to the root directory of your project.
> cd MyProject

Then install nuget package.
> dotnet add package OptivumParser


### Docker
> docker pull karolwojtasiuk/optivum-parser:latest  
> docker run --name optivum-parser -p 80:80 karolwojtasiuk/optivum-parser:latest

### Example
```csharp
var provider = new PlanProvider("https://mySchool.pl/lessonPlan");
var classId = ListParser.GetClass(provider, "3bt");
var lessons = LessonParser.GetLessonsForClass(provider, classId);
var lesson = lessons.Where(l => l.Number == 1 && l.DayOfWeek == 2).First(); 
```

## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
> [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/current)

### Installing
Firstly clone this repository to your computer.
> git clone https://github.com/KarolWojtasiuk/OptivumParser

Then go to the root directory of the repository.
> cd OptivumParser

Finally run the Api.
> dotnet run -p OptivumParser.Api

### Running the tests
Tests for this application is provided by xUnit.
.NET Core contains a Test Runner, so you don't have to download anything.
Just run the Test Runner.
> dotnet test OptivumParser.Tests

## Built With
* [AngleSharp](https://github.com/AngleSharp/AngleSharp) - HTML Parsing Library.
* [xUnit](https://github.com/xunit/xunit) - Testing framework.
* [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - Swagger documents generating library.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
