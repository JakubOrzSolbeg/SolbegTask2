# Who want to be a KROMERnaire
Parody of popular tv show but with more Kromers.

## About

Solbeg trainee Task 2

### Main application features
- the game (working)
- question database on external server
- reward configuration
- shuffling answer order
- saving game progress using cookies

### Rules of the game

- You have to choose correct answer 
- You win when answered correctly for all of them
- You can surrender and keep your kromers
- When answered incorrectly you will only receive kromers from last checkpoint

### Project structure

<a href="https://ibb.co/vzMHDNv"><img src="https://i.ibb.co/124LbCJ/Solbeg-Task2-Struture-drawio.png" alt="Solbeg-Task2-Struture-drawio" border="0"></a>

### DataConfig

App uses 3 sources of data; SqlServer, json file, memory.

SqlServer is used to store Question info

Jsonfile is used for storing game config

Memory is used to store frequently used variables in order to reduce query time

There is no need to manually configure any sql connection string;