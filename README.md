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
* Koden overholder TDD principper og der udvikles Unit Tests til alt kode
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

### Links
* [Skabelon til admin template](https://justboil.me/bulma-admin-template/free-html-dashboard/)
* [Bruge SCSS fra Visual Studio](https://www.mikesdotnetting.com/article/367/working-with-sass-in-an-asp-net-core-application#google_vignette)
