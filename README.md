# Betting Engine - Shane Eardley

## Preface

A new prototype sports betting engine is needed. It should be able to allow for configuring Events and their Markets, 
as well as allowing bets to be placed and resulted.

## Prerequisites 
* [git](https://git-scm.com/downloads) Installed - To clone the code repository from github

### Server Specific
* [.NET Core SDK](https://dotnet.microsoft.com/download) Installed - To build and execute the project
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

* Alternatively, open the BettingEngineServer.sln solution file, and use a supporting IDE to build and run the test project

## How to Startup Server 
* If not done already, clone the project from github, to a directory on your PC:
   ``` 
  git clone https://github.com/shaneeardley/betting-engine 
  ```
* Navigate to the ['BettingEngineServer'](./BettingEngineServer/BettingEngineServer) directory, inside the cloned betting-engine directory
* Compile the project, using the dotnet build command:
    ```
    dotnet build
    ```
* Once successful, start up the project using the 'dotnet run' command:
  ```
  dotnet run
  ```
* The web service will now be running locally, at (usually) https://localhost:5001

* Alternatively, open the BettingEngineServer.sln solution file, and use a supporting IDE to build and run the project


## How to Startup Client 
* If not done already, clone the project from github, to a directory on your PC:
   ``` 
  git clone https://github.com/shaneeardley/betting-engine 
  ```
* Navigate to the ['betting-engine-client'](./betting-engine-client) directory, inside the cloned betting-engine directory
* Using NPM, install all dependencies required by the project
    ```
    npm i
    ```
* Once successful, start up the project using the 'ng serve' command:
  ```
  ng serve
  ```
* The web site will now be running locally, at http://localhost:4200. If something else is running there, you'll be 
instructed on how to proceed.

* NB - If your web service did not default to localhost:5001, please update the default config for the web client, to 
point to your server url, located int betting-engine-client/src/assets/config.json


## Assumptions
* Bets can be placed once an event has started, but not after it has finished 
* No items can be deleted once created

## Notes
* The DB is not currently persisted outside of memory, but to convert to persisted storage, all that would need to be 
done is creating and implementing of a DB Context
* The client is far from perfect, and does not have any tests, nor has it been hardened for stability. It does however
provide and easy point of access to the web server, demonstrating (I think) all of the required functionality
* The tests (should) all run AND pass :)  




## Supported OS's
* Debian Linux (elementaryOS 5.0 Juno) - Confirmed
* Microsoft Windows 10 - Confirmed
* macOS - Untested, but should be fine
