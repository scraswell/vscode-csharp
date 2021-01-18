# Getting Started with CSharp and .NET Core Development
## Requirements
* Platform specific .NET or .NET Core SDK
* Visual Studio Code

## Checking that you're ready to start
Open a terminal and enter `dotnet`.

You should see a result similar to this:
```
Usage: dotnet [options]
Usage: dotnet [path-to-application]

Options:
  -h|--help         Display help.
  --info            Display .NET Core information.
  --list-sdks       Display the installed SDKs.
  --list-runtimes   Display the installed runtimes.

path-to-application:
  The path to an application .dll file to execute.
```

# Basic Information
.NET application structure is typically done by creating a `solution` that is comprised of one or more `project` that reference `assemblies` or other projects in the solution.

To create any application component, you use the `dotnet` application with the argument `new`.  You can always get context-sensitive help with the `-h` parameter.

Here is example output from the command `dotnet new -h`:
```
Usage: new [options]

Options:
  -h, --help          Displays help for this command.
  -l, --list          Lists templates containing the specified name. If no name is specified, lists all templates.
  -n, --name          The name for the output being created. If no name is specified, the name of the current directory is used.
  -o, --output        Location to place the generated output.
  -i, --install       Installs a source or a template pack.
  -u, --uninstall     Uninstalls a source or a template pack.
  --nuget-source      Specifies a NuGet source to use during install.
  --type              Filters templates based on available types. Predefined values are "project", "item" or "other".
  --dry-run           Displays a summary of what would happen if the given command line were run if it would result in a template creation.
  --force             Forces content to be generated even if it would change existing files.
  -lang, --language   Filters templates based on language and specifies the language of the template to create.
  --update-check      Check the currently installed template packs for updates.
  --update-apply      Check the currently installed template packs for update, and install the updates.


Templates                                         Short Name               Language          Tags                                 
----------------------------------------------------------------------------------------------------------------------------------
Console Application                               console                  [C#], F#, VB      Common/Console                       
Class library                                     classlib                 [C#], F#, VB      Common/Library                       
WPF Application                                   wpf                      [C#]              Common/WPF                           
WPF Class library                                 wpflib                   [C#]              Common/WPF                           
WPF Custom Control Library                        wpfcustomcontrollib      [C#]              Common/WPF                           
WPF User Control Library                          wpfusercontrollib        [C#]              Common/WPF                           
Windows Forms (WinForms) Application              winforms                 [C#]              Common/WinForms                      
Windows Forms (WinForms) Class library            winformslib              [C#]              Common/WinForms                      
Worker Service                                    worker                   [C#]              Common/Worker/Web                    
Unit Test Project                                 mstest                   [C#], F#, VB      Test/MSTest                          
NUnit 3 Test Project                              nunit                    [C#], F#, VB      Test/NUnit                           
NUnit 3 Test Item                                 nunit-test               [C#], F#, VB      Test/NUnit                           
xUnit Test Project                                xunit                    [C#], F#, VB      Test/xUnit                           
Razor Component                                   razorcomponent           [C#]              Web/ASP.NET                          
Razor Page                                        page                     [C#]              Web/ASP.NET                          
MVC ViewImports                                   viewimports              [C#]              Web/ASP.NET                          
MVC ViewStart                                     viewstart                [C#]              Web/ASP.NET                          
Blazor Server App                                 blazorserver             [C#]              Web/Blazor                           
Blazor WebAssembly App                            blazorwasm               [C#]              Web/Blazor/WebAssembly               
ASP.NET Core Empty                                web                      [C#], F#          Web/Empty                            
ASP.NET Core Web App (Model-View-Controller)      mvc                      [C#], F#          Web/MVC                              
ASP.NET Core Web App                              webapp                   [C#]              Web/MVC/Razor Pages                  
ASP.NET Core with Angular                         angular                  [C#]              Web/MVC/SPA                          
ASP.NET Core with React.js                        react                    [C#]              Web/MVC/SPA                          
ASP.NET Core with React.js and Redux              reactredux               [C#]              Web/MVC/SPA                          
Razor Class Library                               razorclasslib            [C#]              Web/Razor/Library/Razor Class Library
ASP.NET Core Web API                              webapi                   [C#], F#          Web/WebAPI                           
ASP.NET Core gRPC Service                         grpc                     [C#]              Web/gRPC                             
dotnet gitignore file                             gitignore                                  Config                               
global.json file                                  globaljson                                 Config                               
NuGet Config                                      nugetconfig                                Config                               
Dotnet local tool manifest file                   tool-manifest                              Config                               
Web Config                                        webconfig                                  Config                               
Solution File                                     sln                                        Solution                             
Protocol Buffer File                              proto                                      Web/gRPC                             

Examples:
    dotnet new mvc --auth Individual
    dotnet new razorcomponent
    dotnet new --help
```

# Creating a New Solution with a New Project
To create a new `solution` called `MyFirstSolution`:

```dotnet new sln -n MyFirstSolution```

To create a new `project` called `MyClassLibrary` using .NET Core 3.1:

```dotnet new classlib -n MyClassLibrary -f netcoreapp3.1```

To add the `project` to the `solution`:

```dotnet sln add MyClassLibrary```

The newly created project will always include a single class called `Class1`.  CSharp source files always have the extension `.cs`.

# Setting Up Standards and "Guard Rails"
## Requirements
The .NET and .NET Core ecosystems have a number of tools available to perform code-analysis to make sure you are adhereing to coding standards and not doing anything that isn't reasonable in your code.  There are three packages that should be added to your project to perform code analysis:
* `Microsoft.CodeAnalysis.CSharp`
* `Microsoft.CodeAnalysis.NetAnalyzers`
* `StyleCop.Analyzers`

## Setup
To add a package to a project from the command-line, first change to the project folder and then execute the command `dotnet add package ...`.
```sh
cd MyClassLibrary
dotnet add package Microsoft.CodeAnalysis.CSharp
dotnet add package Microsoft.CodeAnalysis.NetAnalyzers
dotnet add package StyleCop.Analyzers
```

In order to enforce the rules and make builds fail when the standards are not followed, we will add an attribute to the project to treat warnings as errors.  This should be done in all cases, even when not using these rule providers.  Warnings should not be ignored and should be addressed.  Not doing so creates technical debt.

To make this configuration change, we will edit the `project` file, "`MyClassLibrary.csproj`" and add a PropertyGroup element.
```xml
<Project Sdk="Microsoft.NET.Sdk">

  ...

  <PropertyGroup>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  </PropertyGroup>

  ...

</Project>
```

We can see how this will affect the build (it will fail) by now attempting a build.  Change to the solution directory and build...
```sh
cd ..
dotnet build
```

```
Microsoft (R) Build Engine version 16.7.0+7fb82e5b2 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
Class1.cs(1,1): error SA1633: The file header is missing or not located at the top of the file. [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
Class1.cs(1,1): error SA1200: Using directive should appear within a namespace declaration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
CSC : error SA0001: XML comment analysis is disabled due to project configuration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]

Build FAILED.

Class1.cs(1,1): error SA1633: The file header is missing or not located at the top of the file. [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
Class1.cs(1,1): error SA1200: Using directive should appear within a namespace declaration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
CSC : error SA0001: XML comment analysis is disabled due to project configuration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
    0 Warning(s)
    3 Error(s)

Time Elapsed 00:00:01.17
```

From the output, we have three problems to correct.  Let us now correct the problems, one at a time.

Error [`SA1633`](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1633.md) requires that a file header be added to each source file.  This is a StyleCop rule.  The expected type of header is described at the above link.  I will add the following text to the top of the file `Class1.cs`:
```cs
//-----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Medavie Inc">
// Copyright (c) Medavie Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
```

Now when we rebuild, we actually still have three errors but one has changed.
```
Microsoft (R) Build Engine version 16.7.0+7fb82e5b2 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
Class1.cs(6,1): error SA1200: Using directive should appear within a namespace declaration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
Class1.cs(2,4): error SA1636: The file header copyright text should match the copyright text from the settings. [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
CSC : error SA0001: XML comment analysis is disabled due to project configuration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]

Build FAILED.

Class1.cs(6,1): error SA1200: Using directive should appear within a namespace declaration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
Class1.cs(2,4): error SA1636: The file header copyright text should match the copyright text from the settings. [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
CSC : error SA0001: XML comment analysis is disabled due to project configuration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
    0 Warning(s)
    3 Error(s)

```

Error [`SA1636`](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1636.md) now tells us that the copyright text does not match.  This is because the company name is not properly configured.  StyleCop is configurable via a file called `stylecop.json`.  All configuration is [documented](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/Configuration.md).  I will add this file to the solution directory and to the project.

The content of this file, located at `{slndir}/stylecop.json` will, for now, be set to:
```json
{
    "settings": {
      "documentationRules": {
          "companyName" : "Medavie Inc"
      }
    }
  }
```

In addition, we need to tell the project where to find the `stylecop.json` file.  In the `.csproj` file we will add a new section:
```xml
<Project Sdk="Microsoft.NET.Sdk">

  ...

  <ItemGroup>
    <AdditionalFiles Include="../stylecop.json" />
  </ItemGroup>

  ...

</Project>
```

Notice that we have set this AdditionalFiles element Include attribute to be the relative path to `stylecop.json`.  This tells the compiler where to find the file and it is included in the build context.

Now we rebuild (`dotnet build`) and the [`SA1633`](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1633.md) or [`SA1636`](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1636.md) error no longer appears.
```
Microsoft (R) Build Engine version 16.7.0+7fb82e5b2 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
Class1.cs(6,1): error SA1200: Using directive should appear within a namespace declaration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
CSC : error SA0001: XML comment analysis is disabled due to project configuration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]

Build FAILED.

Class1.cs(6,1): error SA1200: Using directive should appear within a namespace declaration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
CSC : error SA0001: XML comment analysis is disabled due to project configuration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
    0 Warning(s)
    2 Error(s)

Time Elapsed 00:00:00.96
```

At this time it is worth mentioning that the command `dotnet build` creates .NET IL assembly files in `./bin/` and object files in `./obj/`.  These files can be automatically cleaned up by invoking the `dotnet clean` command.  Invoking `dotnet clean && dotnet build` would do a clean build of the application.

Now we will address StyleCop error [`SA1200`](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA1200.md).  This error states that "`using`" directives should always be located inside of a namespace block.  In the file `Class1.cs` we will move the `using` directives into the `namespace` block at the top.

Before:
```cs
//-----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Medavie Inc">
// Copyright (c) Medavie Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace MyClassLibrary
{
    public class Class1
    {
    }
}
```

After:
```cs
//-----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Medavie Inc">
// Copyright (c) Medavie Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MyClassLibrary
{
    using System;

    public class Class1
    {
    }
}
```

Now we rebuild to find only one error left to correct:
```
Microsoft (R) Build Engine version 16.7.0+7fb82e5b2 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
CSC : error SA0001: XML comment analysis is disabled due to project configuration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]

Build FAILED.

CSC : error SA0001: XML comment analysis is disabled due to project configuration [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
    0 Warning(s)
    1 Error(s)

Time Elapsed 00:00:00.93
```

Error [`SA0001`](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/documentation/SA0001.md) tells us that the project is not configured to create its own documentation.  We would really like self-documenting code.  Let's turn it on.  We turn it on by setting yet another property in the `.csproj` file.  We will add the property `DocumentationFile` to the PropertyGroup we created earlier.  Note that the dotnet build tools will interpret and replace variables, delimited by `$(` and `)` at compile time.  You can see a list of the well-known MSBuild properties [here](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2019).  In this example, I'm placing the generated documentation file in the build output directory.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  ...

  <PropertyGroup>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <DocumentationFile>bin/$(Configuration)/$(TargetFramework)/$(MSBuildProjectName).xml</DocumentationFile>
  </PropertyGroup>

  ...

</Project>
```

Now when we save the `.csproj` file and rebuild all the errors should be fixed!  Right?!?!

WRONG!

By generating the documentation file, we've actually created more files for our code analysis tools to scan.  This is what happens when we rebuild:
```
Microsoft (R) Build Engine version 16.7.0+7fb82e5b2 for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  All projects are up-to-date for restore.
Class1.cs(11,18): error CS1591: Missing XML comment for publicly visible type or member 'Class1' [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
Class1.cs(11,18): error SA1600: Elements should be documented [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]

Build FAILED.

Class1.cs(11,18): error CS1591: Missing XML comment for publicly visible type or member 'Class1' [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
Class1.cs(11,18): error SA1600: Elements should be documented [/Users/00005309/development/code/csharp/MyClassLibrary/MyClassLibrary.csproj]
    0 Warning(s)
    2 Error(s)
```

We now have two new errors.  Compiler warning [`CS1591`](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/cs1591) wants all public members to be documented.  We can fix this by adding a valid member comment.  In `Class1.cs`, add a valid XML comment above Class1's declaration:
```cs

...

namespace MyClassLibrary
{
    using System;

    /// <summary>
    /// This is our first class.
    /// </summary>
    public class Class1
    {
    }
}
```

Once this is complete, our build succeeds (YAY!) and our code analysis tools are configured for this project.

# Getting Started with Test-Driven Development with C#
## What is it?
Now let's take this project a little further.  Test-Driven development means we actually write tests first before any implementation.

## A simple design
We'll use a very simple example of how this might work.  Consider a rudimentary calculator that only knows how to add two numbers.  It has one method that takes two arguments.  Lets design the interface.

## The interface
In C#, an interface is a contract.  Anything that implements the interface must implement all of it's methods.

Add a new file to the MyClassLibrary project called, `ICalculator.cs`.  In CSharp, it is a convention that interfaces are prefixed with the letter `I`.  The content of this file:
```cs
//-----------------------------------------------------------------------
// <copyright file="ICalculator.cs" company="Medavie Inc">
// Copyright (c) Medavie Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MyClassLibrary
{
    using System;

    /// <summary>
    /// The rudimentary calculator interface.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Adds two integers and returns the result.
        /// </summary>
        /// <param name="x">The first integer.</param>
        /// <param name="y">The second integer.</param>
        /// <returns>The result of the operation to add the integers.</returns>
        public int Add(int x, int y);
    }
}
```

Notice that there is no method body.  That is because the interface only describes the contract that all implementations must follow.

## The interface implementation (not really)
Now lets create a new file called `Calculator.cs`.  The content of this file:
```cs
//-----------------------------------------------------------------------
// <copyright file="Calculator.cs" company="Medavie Inc">
// Copyright (c) Medavie Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MyClassLibrary
{
    using System;

    /// <summary>
    /// The rudimentary calculator implementation.
    /// </summary>
    public class Calculator : ICalculator
    {
        /// <summary>
        /// Adds two integers and returns the result.
        /// </summary>
        /// <param name="x">The first integer.</param>
        /// <param name="y">The second integer.</param>
        /// <returns>The result of the operation to add the integers.</returns>
        public int Add(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
```

Notice how in the implementation of the `Add` method, I have asked the machine to throw an instance of the NotImplementedException.  This allows me to implement the method at a later date, but the code will still compile.  This is one way that I can compile code but write the tests first.  Note the colon between `Calculator` and `ICalculator`; read the colon as the word "implements".

Running a clean build should pass at this point and I have done absolutely no implementation yet.  Only design.

## Creating unit tests
Typically in C# projects, unit test code is written in a unit test project.  This keeps the test code out of the implementation assemblies.  We're going to create a new project, reference our class library and write a test.

What is a unit test?  It is a simple test that works ONLY with YOUR code.  A unit test does NOT test other libraries or third party code.  You should always strive to plan out your design such that any code YOU write can be easily tested without the requirement of external services.  One versatile library for this is `Moq`.  `Moq` would allow you to use the interface from a third party service to mock a return.  You can use this mocked object to simulate the result of a service call and ensure that your logic for handling said response works as intended.

[`XUnit`](https://xunit.net/) is the most commonly used unit testing library for .NET Core projects.  We will use this.

### Adding a unit test project
From the Solution Directory at your shell, create the unit test project:
```sh
dotnet new classlib -n MyClassLibrary.UnitTests -f netcoreapp3.1
```

Add the unit test project to the solution:
```sh
dotnet sln add MyClassLibrary.UnitTests
```

Change to the unit test project folder:
```sh
cd MyClassLibrary.UnitTests
```

Add a reference to our project:
```sh
dotnet add reference ../MyClassLibrary
```

Add the assemblies required for `XUnit` testing:
```
dotnet add package xunit
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit.runner.visualstudio
```

Now we have a unit test project that references our implementation project.

### Writing unit tests
Inside the folder structure of that new `MyClassLibrary.UnitTests` project, delete the `Class1.cs` file and create a `CalculatorUnitTests.cs` file.  The content of that file as follows:
```cs
using System;
using Xunit;
using MyClassLibrary;

namespace MyClassLibrary.UnitTests
{
    public class CalculatorUnitTests
    {
        [Fact]
        public void CanAdd() {
            ICalculator calc = new Calculator();

            // Tests that the expected value equals the actual value when adding 3 and 4.
            Assert.Equal(
                7,
                calc.Add(3,4));
        }
    }
}
```

Things to note here:
* `[Fact]` is an attribute that tells the test runner that this method should be executed when testing.
* `Assert` is an object that actually verifies values for you.  Typically the signature for a method is `(expectedValue, actualValue)`.  Refer to the documentation on [`Xunit.Assert`](https://xunit.net/) for more info.

### Running unit tests
Now we should be able to build and run the test (from the solution directory):
```
dotnet clean && dotnet test
```

Note here that the `test` target will build and run tests.  We will see that the test fails:
```
Test run for /Users/00005309/development/code/csharp/MyClassLibrary.UnitTests/bin/Debug/netcoreapp3.1/MyClassLibrary.UnitTests.dll(.NETCoreApp,Version=v3.1)
Microsoft (R) Test Execution Command Line Tool Version 16.7.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.
[xUnit.net 00:00:00.69]     MyClassLibrary.UnitTests.CalculatorUnitTests.CanAdd [FAIL]
  X MyClassLibrary.UnitTests.CalculatorUnitTests.CanAdd [1ms]
  Error Message:
   System.NotImplementedException : The method or operation is not implemented.
  Stack Trace:
     at MyClassLibrary.Calculator.Add(Int32 x, Int32 y) in /Users/00005309/development/code/csharp/MyClassLibrary/Calculator.cs:line 24
   at MyClassLibrary.UnitTests.CalculatorUnitTests.CanAdd() in /Users/00005309/development/code/csharp/MyClassLibrary.UnitTests/CalculatorUnitTests.cs:line 14

Test Run Failed.
Total tests: 1
     Failed: 1
 Total time: 1.2425 Seconds
/usr/local/share/dotnet/sdk/3.1.402/Microsoft.TestPlatform.targets(32,5): error MSB4181: The "Microsoft.TestPlatform.Build.Tasks.VSTestTask" task returned false but did not log an error. [/Users/00005309/development/code/csharp/MyClassLibrary.UnitTests/MyClassLibrary.UnitTests.csproj]
```

The test fails, obviously, because we haven't implemented the calculator yet.  The error message is: `System.NotImplementedException : The method or operation is not implemented.`.  This makes a whole lot of sense because we explicitly told the compiler to throw this exception when we call the Add() method.

### Finally, Class method implementation
However, now that our test is written, we can implement the Add() method.

Back in our `Calculator` implementation file `Calculator.cs` change the Add() method body:
Before:
```cs
        public int Add(int x, int y)
        {
            throw new NotImplementedException();
        }
```

After:
```cs
        public int Add(int x, int y)
        {
            return x + y;
        }
```

### A successful test
Finally, test again and you'll see the result from the unit test:
```
Test run for /Users/00005309/development/code/csharp/MyClassLibrary.UnitTests/bin/Debug/netcoreapp3.1/MyClassLibrary.UnitTests.dll(.NETCoreApp,Version=v3.1)
Microsoft (R) Test Execution Command Line Tool Version 16.7.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.

Test Run Successful.
Total tests: 1
     Passed: 1
 Total time: 1.1781 Seconds
```

## Code Coverage
Code coverage reports tell us how much of our code is covered by unit-tests.  It is a great metric understand how likely it is that there are bugs in your code.  More coverage means it's less likely to have bugs.  Coverage doesn't mean you can't have bugs.  Obviously if there are test cases you haven't considered, then you can still have problems.

[Coverlet](https://github.com/coverlet-coverage/coverlet) is a well-known tool for generating code coverage reports.  We will add Coverlet to our Unit Test project.

From the Unit Test project folder:
```sh
dotnet add package coverlet.msbuild
dotnet add package coverlet.collector
```

Once it is added, we will configure out Unit Test project to generate the coverage results by adding a property group to the `.csproj` configuration file:
```xml
<Project Sdk="Microsoft.NET.Sdk">

  ...

  <PropertyGroup>
    <CollectCoverage>true</CollectCoverage>
    <CoverletOutputFormat>opencover</CoverletOutputFormat>
    <CoverletOutput>../Metrics.CodeCoverage/$(MSBuildProjectName).opencover.xml</CoverletOutput>
  </PropertyGroup>

  ...

</Project>
```

The next time we run a test (`dotnet test`), we will see the following output:
```
Test run for /Users/00005309/development/code/csharp/MyClassLibrary.UnitTests/bin/Debug/netcoreapp3.1/MyClassLibrary.UnitTests.dll(.NETCoreApp,Version=v3.1)
Microsoft (R) Test Execution Command Line Tool Version 16.7.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...

A total of 1 test files matched the specified pattern.

Test Run Successful.
Total tests: 1
     Passed: 1
 Total time: 1.0779 Seconds

Calculating coverage result...
  Generating report '../Metrics.CodeCoverage/MyClassLibrary.UnitTests.opencover.xml'

+----------------+------+--------+--------+
| Module         | Line | Branch | Method |
+----------------+------+--------+--------+
| MyClassLibrary | 100% | 100%   | 100%   |
+----------------+------+--------+--------+

+---------+------+--------+--------+
|         | Line | Branch | Method |
+---------+------+--------+--------+
| Total   | 100% | 100%   | 100%   |
+---------+------+--------+--------+
| Average | 100% | 100%   | 100%   |
+---------+------+--------+--------+
```

We can also generate a pretty `HTML` report using another dotnet tool.  From the solution directory:
```sh
dotnet tool install dotnet-reportgenerator-globaltool --tool-path tools
tools/reportgenerator -reports:./Metrics.CodeCoverage/*.opencover.xml -targetdir:./CodeCoverage.Reports/
```
These commands will generate a nice `HTML` report of your code coverage in the folder `CodeCoverage.Reports`.

# Summary
You now have a very basic understanding of how to get started with CSharp development using free and open source tools.  Documentation is your friend.  Use it.

# Appendix
## Plugins Useful for VSCode when Writing C# Code
See [this](https://code.visualstudio.com/docs/languages/csharp) page.
* C# for Visual Studio Code
* C# XML Documentation Comments (helpful for automatically adding comments to public members)
* File Header Comment (helpful for inserting file header comments in new files)