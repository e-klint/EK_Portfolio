# The Book Parlour – Backend (ASP.NET Web API)

## Om Projektet
Ett REST API för en engelsk bokhandel med tema kring klassisk 
litteratur – Brontë, Shakespeare, Harry Potter m.fl. Byggt med 
ASP.NET Web API och Clean Architecture med tydliga lager för 
controller, service och repository. API:et hanterar böcker, 
genrer, författare och varukorg med JWT-baserad autentisering.

## Dokumentation och guider
Projektet innehåller steg-för-steg-guider för uppsättning, driftsättning och säker konfiguration i Azure.

Guiderna beskriver:
* **CI/CD Pipeline** – hur projektet byggs, testas och deployas automatiskt till Azure med Azure DevOps.
* **Azure Infrastructure** – skapande och konfigurering av Azure App Service, Azure SQL Database och Managed Identity.
* **Azure Key Vault** – säker hantering av hemligheter och JWT-nycklar utan hårdkodade värden.
* **Application Insights** – loggning, övervakning och diagnostik i produktion.

## Features
* JWT-autentisering med två roller: Admin och Kund
* CRUD för böcker och genrer
* PATCH-endpoint med JsonPatchDocument för partiell uppdatering
* Automatisk slug-generering från titel
* Varukorg kopplad till inloggad användare via JWT
* Paginering och filtrering på böcker och genrer
* Loggning med ILogger i både controller- och service-lager
* Seed-data för admin- och kundanvändare
* Enhetstester med xUnit och Moq
* CI/CD-pipeline med Azure
* Automatiskt genererad API-dokumentation med Scalar

## Tekniker
* C# / ASP.NET Web API
* Entity Framework Core (Code First)
* SQL Server
* JWT (JSON Web Tokens)
* Mapster
* xUnit och Moq
* GitHub Actions
* Azure App Service
* Azure SQL Database
* Git och GitHub

## Arkitektur
Projektet följer Clean Architecture med separation of concerns:
* **Controllers** – hanterar HTTP-requests och responses
* **Services** – affärslogik, t.ex. slug-generering och prisvalidering
* **Repositories** – databasanrop via Entity Framework Core
* **DTOs** – separata modeller för request och response

## Installation
Kör projektet lokalt i Visual Studio.

1. Klona repot
2. Uppdatera connection string i `appsettings.json`
3. Kör migrations: Update-Database
4. Starta projektet – Scalar öppnas automatiskt

### Inloggningsuppgifter (seed-data)
| Användare | Lösenord | Roll |
|-----------|----------|------|
| admin | admin123 | Admin |
| customer | customer123 | Customer |

## Best Practices
* Clean Architecture med tydliga lager
* Repository Pattern
* DTO-pattern – entities exponeras aldrig direkt
* Nullable reference types där det är motiverat
* Loggning på rätt nivå (Information, Warning, Error)
* Säker lösenordshantering med PBKDF2-hashning
* Prisvalidering – användaren kan inte manipulera priser
* Git commits med tydliga meddelanden
