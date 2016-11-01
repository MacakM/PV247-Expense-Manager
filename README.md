# Expense-Manager
School project for PV247 (FI MUNI Brno)

## Team members ##
* Martin Macák
* Jaroslav Davídek
* Marek Turis
* Ondřej Gasior

## Coding Rules ##
### Formatting ###
* Use default formatting of Visual Studio
* After **if**, **while**, **else**, **using**, etc. always use braces
* Braces should always be on the new line (except from empty constructor body)
* Every LINQ clause have to be put on new line
* Use only maximum one empty line between code

### Naming ###
* Every identifier must be named in english
* Classes, structures, delegates, interfaces, namespaces, events, properties and methods are in PascalCase
* Fields are in camelCase, they start with underscore (\_skillLevel)
* Local variables, arguments are in camelCase
* Don't use very short names that does not clearly imply meaning (exceptions: using (var sb = new StringBuilder()) or LINQ lambdas)
* Don't use very long names, they should not contain prepositions, conjunctions and similar words (instead of numberOfPlayersThatAreOnline use onlinePlayersCount)

## Tools ##
### Productivity ###
* Resharper Ultimate 2016.2.2

### Static code analysis & profiling ###
* In-built VS 2015 Update 3 tools

## Use cases ##
### User ###
* add new expenses
  - expense can be add as periodic
* filter expenses
* create new plans
   - based on spending or saving
* create current paste of expenses and share it with other people
* manage my pastes

### Time ###
* plans are automatically marked as finished when the given goal is achieved
* user automatically receives badge when he fulfil the condition
