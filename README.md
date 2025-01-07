# uHelpDesk (Micro Help Desk)
Simple helpdesk service system for small companies, developed by software students on Professionshøjskolen UCN. For any questions please refer to Brian Hvarregaard (brhv@ucn.dk)

## Indledende Scope
Der skal udvikles et system til at håndtere support tickets med. Selve systemet er et selvstændigt system med brugernavn og password adgang. Det skal være muligt for kunder/medarbejdere at oprette tickets (fx. en computer eller printer som er i stykker) hvor en Supportafdeling (eller lignende) så arbejder på at løse denne opgave. Nedenstående er de første tanker om features i systemet:

* Det skal være muligt at logge ind i systemet som administrator (roller = Administrator, Supporter, Normal), en "Normal" rolle er ekstern, fx. en kunde, kollega eller lignende
* CRUD brugere og roller
* Det skal være muligt at arbejde med tickets (supportsager)
* CRUD Ticket
* Det skal være muligt at assigne en bruger til en bestemt ticket
* Det skal være muligt at skifte status på en ticket (fx. afvist, måske der skal være nogle faste status - "Oprettet" og "Afsluttet", så kan man selv tilføje der mellem?)
* CRUD Status, Rolle = Administrator
* Det skal være muligt at inddele tickets i forskellige kategorier
* CRUD Kategorier, Rolle = Administrator
* Det skal være muligt at besvare en ticket (Rolle = Supporter)
* Det skal være muligt at logge tidspunkt på hvornår en ticket er oprettet, hvor lang tid den har været åben, dette er et KPI for "Administrator"
* Når der er nye besvarelser til en ticket skal opretteren (Rolle = Normal) have en besked, fx. email

* Det skal være muligt at tilføje tickets via API
* CRUD API Nøgle til forskellige API'er således andre kan indsende Tickets via API
* CRUD Webhooks - til ekstern integration
* Statistik
* Mulighed for at indsende ønsker til features eller hvis man ser en fejl. Dette skal naturligvis ind i `uHelpDesk`

### Teknologi
* Bulma til frontend
* ASP.NET Core MVC
* CQRS arkitektur
* Clean Architecture (? afklar lige hvad vi gør med database og bagudkompatabilitet)
* MSSQL, EF Core
* nUnit unit testing, Moq, TestContainers, kontrakttests
* GitHub Actions til CI/CD
