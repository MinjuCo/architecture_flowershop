# architecture_flowershop

## Introduction
This project contains a basic API for flowershops to view their sales.

## Installation
You will need the following software:

* [.NET Core](https://dotnet.microsoft.com/download)
* An editor/IDE of your choice. [Visual Studio](https://code.visualstudio.com/) was used for this project, because you can access the command line directly from it and you can install extensions very easily.

## Usage
You can use 'dotnet watch run' to run the project; after you get the notification that the application is started, go to http://Localhost:5000/swagger/index.html; you will get an overview with all the API methods and a quick method to execute them.

### Explanation

* 'dotnet watch run' is a file monitor; it compiles and whatches your code.

## TO DO
Maury: 
> Ik heb mijn basiskennis proberen te maken door innformatie en meer op te zoeken,  omdat ik de basis kennis nog niet heb. 
> De sales pagina was nog niet gelukt, dus deze moet nog worden gefixt en worden verwerkt.

We hebben problemen ondervonden met de data bank connectie dus het doorsturen en opvragen van data was niet echt gelukt.
Update: na het opzoeken en bekijken van filmpjes hebben we ondervonden dat we de XAMPP niet correct hadden opgezet.

- [X] Endpoints om bloemen en winkels toe te voegen
- [X] Async en swagger implementeren
- [ ] Relevante testen voor alle containers toevoegen: er is op dit moment enkel één dummy-test waarbij 'GetAllShops' wordt getest.
- [ ] Endpoints/migratie voor verkoop van bloemen moet nog geïmplementeerd worden -> nogal vaag bij ons hoe we dat gaan doen
- [ ] Code documenteren/verduidelijken met comments
- [ ] Dependency injection moet nog dubbel gecheckt worden.

### Bronnen:
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/

http://asp.net-informations.com/collection/asp-collection.htm

https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/getting-started-with-mvc/getting-started-with-mvc-part2

https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-3.1&tabs=visual-studio-code

https://docs.microsoft.com/en-us/learn/modules/build-web-api-net-core/

https://channel9.msdn.com/Blogs/dotnet/Get-started-VSCode-Csharp-NET-Core-Windows