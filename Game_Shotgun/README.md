# Shotgun
En konsolapplikation där spelaren möter datorn i en variant av “sten, sax, påse”. 

## Tekniker och Verktyg
C# och .NET
OOP
Visual Studio

## Installation
### Krav
* Visual Studio 2022 eller senare
* .NET 6.0 eller senare

### 📥 Ladda ner projektet

1. Klona repot eller ladda ner som ZIP
2. Packa upp filerna (om ZIP)

### 🚀 Starta projektet

1. Öppna Visual Studio
2. Klicka på "Open a project or solution"
3. Öppna `.sln`-filen

### ▶️ Kör programmet

1. Tryck på **Start (F5)** eller klicka på den gröna play-knappen
2. Konsolfönstret öppnas
3. Följ instruktionerna i spelet

## Bakgrund / Syfte
Detta projekt skapades som ett övningsprojekt under min första C#-kurs. Syftet var att träna if-satser, loopar, slumpgenerering, användarinmatning och spelloopar i konsolmiljö.

## Funktioner
- Spela “Shotgun” mot datorn
- Datorn väljer sitt drag slumpmässigt
- Logik för laddning, blockering, skott och specialattack (“Shotgun”)
- Resultat visas efter varje omgång

## Regler / Spelidé
- Varje spelare kan välja mellan Ladda, Blocka eller Skjuta.
- Man måste ladda innan man kan skjuta.
- För att vinna krävs att man skjuter när motståndaren inte blockar eller skjuter.
- När en spelare har samlat 3 skott blir det "Shotgun" och spelaren vinner direkt.

### Möjliga förbättringar / framtida idéer
- GUI istället för konsol
- Flerspelarläge
- Olika "strategier"/"smartare logik" för datorn
