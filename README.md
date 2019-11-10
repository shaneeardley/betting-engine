# Betting Engine - Shane Eardley

## Preface
A new prototype sports betting engine is needed. This needs to cater for configuring Events and their Markets, as well
as allowing bets to be placed and resulted. 


## Prerequisites 
* [git](https://git-scm.com/downloads) Installed - To clone the code repository from github

### Server Specific
* [.NET Core SDK](https://dotnet.microsoft.com/download) Installed - To build and execute the project OR
* [.NET Core Runtime](https://dotnet.microsoft.com/download) Installed - To run pre-compiled binaries [(available here)]()
### Client Specific
* [nodeJS](https://nodejs.org/en/download) Installed - Used to run NPM commands for the client
* [Angular CLI](https://angular.io/cli) Installed - To build and execute the front end client. Simply run:
``` 
npm install -g @angular/cli
```

## Instructions
## How to execute test cases
* Clone the project from github, to a directory on your PC:
   ``` 
  git clone https://github.com/shaneeardley/betting-engine 
  ```
* Navigate to the ['BettingEngineServerTests'](./BettingEngineServer/BettingEngineServerTests) directory, inside the cloned betting-engine directory
* Compile the project, using the dotnet build command:
    ```
    dotnet build
    ```
* Once successful, execute the tests by using the 'dotnet test' command:
  ```
  dotnet test
  ```
* Ensure that all tests run and pass correctly

* Alternatively, open the Next45.Rover.sln solution file, and use a supporting IDE to build and run the test project

## How to Use 
* If not done already, clone the project from github, to a directory on your PC:
   ``` 
  git clone https://github.com/shaneeardley/Next45-Rover.git 
  ```
* Navigate to the ['Next45.Rover'](./Next45.Rover) directory, inside the cloned Next45.Rover directory
* Compile the project, using the dotnet build command:
    ```
    dotnet build
    ```
* Once successful, start up the project using the 'dotnet run' command:
  ```
  dotnet run
  ```
* Follow the instructions displayed in the terminal/console, using your own, or some of the inputs shown below

* Alternatively, open the Next45.Rover.sln solution file, and use a supporting IDE to build and run the project

## Example Input, and output values
### From provided example 
* Please note that I had to modify the commands from the original question, as it seemed to be missing the final 2 commands 

Provided Input's
```
Surface Boundaries: 88
Start Location: 12 E
Rover Movements: MMLMRMMRRMMLMM
```
Final Output
```
Map showing the course followed by the rover: 

    1  2  3  4  5  6  7  8 
 1  .  .  X  X  X  .  .  . 
 2  S  X  X  .  .  .  .  . 
 3  .  .  E  .  .  .  .  . 
 4  .  .  .  .  .  .  .  . 
 5  .  .  .  .  .  .  .  . 
 6  .  .  .  .  .  .  .  . 
 7  .  .  .  .  .  .  .  . 
 8  .  .  .  .  .  .  .  . 

Rover completed exploration successfully
Rover's start position: 12 E
Rover's Ending Position: 33 S

```

### Additional Example

Provided Input's
```
Surface Boundaries: 59
Start Location: 32 S
Rover Movements: MMMLMMLMMLM
```
Final Output:
```
Map showing the course followed by the rover: 

    1  2  3  4  5 
 1  .  .  .  .  . 
 2  .  .  S  .  . 
 3  .  .  X  E  X 
 4  .  .  X  .  X 
 5  .  .  X  X  X 
 6  .  .  .  .  . 
 7  .  .  .  .  . 
 8  .  .  .  .  . 
 9  .  .  .  .  . 

Rover completed exploration successfully
Rover's start position: 32 S
Rover's Ending Position: 43 W
```

## Development Approach
* Write out basic ideas and simple program flow on notes app / pad
* Set up project shell, Console App and Test project
* Created Surface class/model, write out tests against model, implemented unit tests, continue until all work
* Same process for Rover model 
* Implement shell logic / interacting with user and model
* By following TDD as above, all of the debugging and error fixing was done while designing the models and test cases, with no issues or bugs left
 to face when implementing the actual process.
* I decided to write a .NET Core console application, because of it's cross compatibility across all operating systems
* Since all moves made by the rover were tracked, I figured it would be easy enough for Rover to send a map of it's route followed, along with the 
ending coordinates


## Supported OS's
* Microsoft Windows 10 - Confirmed
* Debian Linux (elementaryOS 5.0 Juno) - Confirmed
* macOS - Untested, but should be fine
