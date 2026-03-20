# Bankapplikation – Backend (ASP.NET Web API)

## Beskrivning

Backend för bankapplikation med **ASP.NET Web API**, **Database First EF**, JWT-baserad autentisering och **Swagger** för dokumentation.
Två användartyper: **Admin** och **Kund**, med olika behörigheter.

---

## Funktionalitet

* **Admin**: Skapa kunder, konton, lån.
* **Kund**: Se konton, transaktioner, skapa konton, överföra pengar.
* Alla operationer kräver JWT-token.

---

## Tekniker

* ASP.NET Web API
* Entity Framework Core (Database First)
* SQL Server (.bak-fil)
* JWT authentication & authorization
* **Swagger** för API-dokumentation
* **AutoMapper** för DTO-mappning
* **Alla Endpoints är testade med Postman

---

## Installation

### 🔧 Krav

För att köra projektet behöver du:

* Visual Studio 2022 eller senare
* .NET 6.0 eller senare
* SQL Server
* SQL Server Management Studio (SSMS)

---

### 📥 Ladda ner databasen

1. Gå till länken: https://www.dropbox.com/scl/fo/i8eyfwcc2ie5elcn8x8hp/AAXhJZA1TKbIOs64qE_0tR0?rlkey=6lbp1e25hlj32gtqb7ozjn05e&st=ahi80z57&dl=0
2. Ladda ner filen
3. Packa upp `.bak`-filen

---

### 🗄️ Återställ databasen (.bak)

1. Öppna SQL Server Management Studio (SSMS)
2. Högerklicka på **Databases**
3. Välj **Restore Database**
4. Välj **Device** → klicka på `...`
5. Lägg till din `.bak`-fil
6. Klicka **OK**

---

### 🔗 Konfigurera connection string

1. Öppna projektet i Visual Studio
2. Gå till `Program.cs`
3. Uppdatera connection string så den matchar din SQL Server.

---

### 🚀 Starta projektet

1. Öppna `.sln`-filen i Visual Studio
2. Tryck på **Start (F5)**

---

### 📄 Swagger (testa API)

När applikationen startar öppnas Swagger automatiskt i webbläsaren.
Annars gå till:

```
https://localhost:xxxx/swagger
```

---

### 🔐 Autentisering (JWT)

1. Använd login-endpointen i Swagger eller Postman
2. Kopiera token du får tillbaka
3. Klicka på **Authorize** i Swagger
4. Klistra in: `Bearer DIN_TOKEN`

---

### 🧪 Testa API

* Alla endpoints kan testas via Swagger
* Alternativt via Postman

---

### ⚠️ Vanliga problem

* Databasen hittas inte
  → Kontrollera connection string

* Fel vid start
  → Kör "Rebuild Solution" i Visual Studio

* Swagger visas inte
  → Kontrollera att rätt port används

---

