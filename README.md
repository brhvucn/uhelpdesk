# uHelpDesk (Micro Help Desk)
Simple helpdesk service system for small companies, developed by software students on Professionshøjskolen UCN. For any questions please refer to Brian Hvarregaard (brhv@ucn.dk)

## Team
Projektleder: Brian Hvarregaard (brhv@ucn.dk)

Deltagere:

## Arbejdsmetode
Som altid anbefaler vi at man sidder sammen og udvikler. Der er flere patterns i brug og i nogle tilfælde er der behov for avanceret arkitektur. Der er også brug for at man har et godt flow i sin udvikling. Vi ser de bedste resultater når man sidder og udvikler sammen.

* Definition of Ready (DoR): En task har et estimat på 3 timer eller derunder. En task har en titel, en kort beskrivelse og en kort beskrivelse af acceptkriterier.

## Krav
* Kode overholder standarder for at bruge abstraktioner (projektlederen laver eksempler på dette)
* Koden overholder TDD principper og der udvikles Unit Tests til alt kode (kode med logik)
* Master branch beskyttes mod pull requests og alle pull requests skal godkendes inden merge
* Der bruges feature branching. En branch skal indeholder id nummeret på den tilhørende user story / issue.
* Der skal bruges "Estimater" på en task som skal vise antal timer det tager at implementere en task. Dette punkt skal indgå i `Definition of Ready`


## Indledende Scope
Der skal udvikles et system til at håndtere support tickets med. Selve systemet er et selvstændigt system med brugernavn og password adgang. Det skal være muligt for kunder/medarbejdere at oprette tickets (fx. en computer eller printer som er i stykker) hvor en Supportafdeling (eller lignende) så arbejder på at løse denne opgave. Nedenstående er de første tanker om features i systemet:

* Det skal være muligt at logge ind i systemet som administrator (roller = Administrator, Supporter, Normal), en "Normal" rolle er ekstern, fx. en kunde, kollega eller lignende

### Teknologi
* Bulma til frontend
* ASP.NET Core MVC
* 3 lagsarkitektur
* MSSQL, EF Core
* nUnit unit testing, Moq, TestContainers, kontrakttests
* GitHub Actions til CI/CD

### UI Regler
Disse regler er således at der udvikles en konsistent brugergrænseflade for systemet. Hvis der skal afviges sker dette efter aftale. Bemærk at denne tabel altid er under udarbejdelse.
* Siden skal kunne fungere på både telefon, tablet og desktop
* Opret, vis og rediger et element har altid sin egen side
* Indhold på en side vises altid inden i en box, `<div class='box'>`
* Indhold i en box har overskriften inden i box elementet
* Tabeller er alt `is-fullwidth is-bordered is-striped is-hoverable`
* Der gives visuelt feedback ved opret, opdater, slet i form af en toast (dette er bygget ind i BaseController)
  * Ved slet navigeres tilbage til en oversigt eller lignende (hvor man kom fra)
* (skal afklares) der laves en custom style til `H1` og `H2` således de passer til designet i størrelse
* Knapper i tabeller er `is-small` fritstående knapper er grupperede og i normal størrelse
* Opret knapper (fx. opret ny Ticket, er typisk placeret i toppen af siden, ved siden af overskriften - trukket ud til højre. Er der behov for flere knapper er de grupperede i en enkelt række.
* Destruktive handlinger (fx. slet) har en bekræftelses popup
* Alt UI tekst er på engelsk (britisk)

### EFCore og Migrations
Dette projekt kører "code first", det vil sige at databasen oprettes af koden. Det betyder at når der kommer ændringer til mængden af klasser eller der ændres properties på en klasse, skal databasen opdateres til at afspejle dette. Dette kaldes "migrations", dvs. at migrere fra nuværende version af databasen til næste version af databasen. Alt dette er gemt i kode, således koden styrer databasen. Hvis ikke databasen er up to date, så får man en runtime exception.

#### Ny installation (ingen database)
* Start med at kigge i appsettings.json og se hvad databasen skal hedde, opret en ny tom database, ret evt. appsettings til.
* I visual studio åbnes "Package Manager Console" vinduet (det ligger typisk nede i bunden af debug vinduet)
* I Dropdown i toppen af "Package Manager Console" under "Default project" vælges der "uHelpDesk.DAL"
* Kør kommandoen: `update-database` - dette vil skrive tabeller i databasen ud køre alle migrations på databasen

#### Rette i en klasse (eller tilføje ny)
Under udvikling kan man komme ud for at man skal ændre en klasse, således den får nye properties. Det kan også være at der tilføjes flere modelklasser. Dette gøres ved at lave en ny klasse og nedarve fra `BaseModel`. For at kunne udnytte databaseadgangen skal databasekonteksten opdateres: `uHelpDeskDbContent`. Her skal der laves et nyt DbSet<..> og der skal evt. laves fremmednøgler og andre constrains ved at tilføje under metoden: `OnModelCreating`. Herefter skal man tilføje en migration og opdatere databasen.
* I visual studio åbnes "Package Manager Console" vinduet (det ligger typisk nede i bunden af debug vinduet)
* I Dropdown i toppen af "Package Manager Console" under "Default project" vælges der "uHelpDesk.DAL"
* Tilføj en migration ved at køre `add-migration xxx` hvor xxx er navnet på den migration der køres, kig på eksisterende migrations i "Migrations" mappen efter navngivning.
* Kør kommandoen: `update-database` - dette vil skrive tabeller i databasen ud køre alle migrations på databasen. Nu er databasen opdateret.

### Links
* [Skabelon til admin template](https://justboil.me/bulma-admin-template/free-html-dashboard/)
* [Bruge SCSS fra Visual Studio](https://www.mikesdotnetting.com/article/367/working-with-sass-in-an-asp-net-core-application#google_vignette)
