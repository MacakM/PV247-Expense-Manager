# Expense-Manager
School project for PV247 (FI MUNI Brno)

## Team members ##
* Martin Macák - Managing, DAL Design, PL Support, BL Support, Tests
* Jaroslav Davídek - Architecture Design, BL Support
* Marek Turis - UI Design, PL
* Ondřej Gasior - BL, Tests

## Coding Rules ##
### Formatting ###
* Use default formatting of Visual Studio
* After **if**, **while**, **else**, **using**, etc. always use braces
* Braces should always be on the new line (except from empty constructor body)
* Every LINQ clause have to be put on new line
* Use only maximum one empty line between code within methods, there is no empty line between closing bracket of block and its last expression
* There is exactly one empty line between usings and namespace
* There is exactly one empty line between namespace and class -> documentation is part of class
* There is exactly one empty line between each class member(properties, methods...) -> documentation and anotations are part of class member 

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
* add other user to the account

### Time ###
* plans are automatically marked as finished when the given goal is achieved
* user automatically receives badge when he fulfil the condition
