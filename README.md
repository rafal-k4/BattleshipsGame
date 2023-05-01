## BattleshipsGame
This project is a .Net console one-sided version of battleship game

## Usage
Preferable choice is to use this application on <b>Windows</b>
<br />
To use this application:
 - build and run using your IDE
 - build and run using .NET CLI
 - use [Release (v1.0)](https://github.com/rafal-k4/BattleshipsGame/releases/tag/v1.0) self-containted package 
<br />
In order to use .NET CLI, first of all <b>.NET 6 SDK</b> package is needed
<br />
Once installed, use this command inside your cmd 

`dotnet run --project ".\BattleshipsGame\BattleshipsGame.csproj"`

## Tests
This project includes suite of tests - unit and functional,
<br />
Functional tests are implemented using package <b>SpecFlow</b>
<br /> 
In order to run tests, use IDE of your choice, or run this .NET CLI command:
`dotnet test --logger "console;verbosity=normal"`
