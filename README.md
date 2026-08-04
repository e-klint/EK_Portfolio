# Portfolio

Här samlar jag projekt jag bygger under mina studier inom C# och .NET. 
Varje projekt innehåller en egen README med mer information. Fler projekt kommer!

## Projekt:

### [ContactList_Adressbok](./Contactlist_Adressbok)
Ett konsolprogram i C# för att hantera kontakter. Användaren kan lägga till, söka, uppdatera och ta bort kontakter som sparas i en textfil.  

### [Shotgun_Game](./Game_Shotgun)
Ett spel byggt i C# där användaren möter datorn i en variant av "sten, sax, påse" kallad *Shotgun*.  

### [APIBankApp (Bankapplikation) – Backend (ASP.NET Web API)](./ApiBankApp)
Backend för bankapplikation med **ASP.NET Web API**, **Database First EF**, JWT-baserad autentisering och **Swagger** för dokumentation.
Två användartyper: **Admin** och **Kund**, med olika behörigheter.

### [WebPortfolio - Frontend](./EK_WebPortfolio)
En personlig webbportfolio som är byggd med HTML, CSS och JavaScript från grunden utan ramverk. Syftet är att presentera vem jag är, mina projekt och mina kunskaper.

Live demo: https://e-klint.github.io/EK_Portfolio/

### [TheBookParlour (Bokhandel) – Backend (ASP.NET Web API)](./TheBookParlour)
Backend för en engelsk bokhandel med ASP.NET Web API, Code First EF, 
JWT-baserad autentisering och Scalar för dokumentation. Två användartyper: 
Admin och Kund, med olika behörigheter. API:et hanterar böcker, genrer, 
författare och varukorg.

Projektet innehåller även tillhörande guider som beskriver hur applikationen sätts upp och driftsätts i **Azure**. Guiderna täcker hela flödet från CI/CD-pipeline med Azure DevOps och **Azure App Service**, till konfiguration av **Azure SQL Database**, **Managed Identity**, **Azure Key Vault** för säker hantering av hemligheter samt **Application Insights** för övervakning och loggning.

Guiderna visar steg för steg hur infrastrukturen skapas, hur applikationen automatiskt byggs och deployas vid nya ändringar, samt hur säkerhet och driftmiljö konfigureras i Azure.

## Tekniker

### Backend
- C# (.NET)
- Objektorienterad programmering (OOP)
- ASP.NET Core Web API
- Entity Framework Core (Code First & Database First)
- LINQ
- SQL Server
- JWT Authentication & Authorization
- Dependency Injection
- DTO-mappning med AutoMapper och Mapster
- Lagerindelning (Controller, Service, Repository)
- Loggning med ILogger
- Enhetstester med xUnit och Moq

### Frontend
- HTML
- CSS
- Flexbox
- JavaScript

### Moln & DevOps
- Azure App Service
- Azure SQL Database
- Azure DevOps Pipelines (CI/CD)
- YAML pipelines
- Azure Key Vault
- Managed Identity
- Application Insights

### Verktyg
- Git & GitHub
- Visual Studio
- Swagger
- Scalar
- Postman

  Externa API:er och tjänster
- FormSubmit [https://formsubmit.co/] (formulärhantering för Webportfolio)
- OpenWeather API [https://openweathermap.org/api] ((väderdata för Webportfolio)

