#### _Created by Cristiano Santana (Cristiano Cricas)_

# CountriesAPI
> This is an API developed in ASP.NET Core utilizing the C# Web API Template.


## ABOUT
> **[It's works like a simple Country ISO 3166-1 Cad/Search](https://en.wikipedia.org/wiki/ISO_3166-1)**


## Architecture
- JSON RESTful Web API
- ASP.NET Core
- Entity Framework with PostgreSQL
- Docker containers for API and Database


## Available features in this project:
- Register a country (name, alpha code, numeric code, etc)
- Register a country subdivision
- Update a countrys information
- Update a country subdivisions information
- Delete a country
- Delete a country subdivision
- List multiple countries
- Filter by name and Alpha Code
- Totalize the amount of obtained countries
- Consult the information of a country subdivision
- List of multiple subdivisions of a country
- Totalize the amount of obtained country subdivisions


# REQUIRED APPS
* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [Docker and Docker compose](https://docs.docker.com/compose/) 



## INSTALATION

### 1. Install Visual Studio
**1.1.** If don't have it, I recomend Community version: https://visualstudio.microsoft.com/vs/community

**1.2.** Must have, at least, **ASP.NET web development** (all recommended packages on the first instalacion such as .NET 6.0, .NET SDK, etc)

[install01]

**1.3.** Plus: install GIT for Windows (on Individual Components)

[install02]


### 2. Install and configure docker compose 
**2.1.** If using Windows, I recommend Docker Desktop, because it's easy to use: https://docs.docker.com/desktop/install/windows-install


### 3. Clone the repository on VS (Visual Studio)


### 4. Open ``Powershell`` on the project's root folder (or use *Command Windows* on VS), and run the commands below:

**4.1.** Script to create a Dev Certificate to HTTPS works

```
.\InitializeDevCertificate.ps1
```

**WARNING:** it may show a confirmation dialogue. Click ``YES`` to create the **Dev trusted certificate**. It'll be created in the ``/src`` directory (**needed** for Docker Compose to use it later.)

[install03]

**4.2.** After configure and start Docker, build and start API container:

```
docker-compose build
```
```
docker-compose up
```
[install04]

**TIPS:** before run ``docker-compose up``, you can change the **PORTS** of API, PostgreSQL and Adminer.
On the root folder is the ``.env`` file where you can change the Environment variables used to set apps Ports.

The Default is:
```
DOCKER_API_PORT=9443
DOCKER_POSTGRES_PORT=5432
DOCKER_ADMINER_PORT=9080
```
[install05]

### 5. Confirm if the container is OK, running the command:
```
docker container ls
```

**5.1.** - if it's OK, will show the containers:
- adminer
- countriesapi
- postgres

[install06]

**5.2.** When using **Docker Desktop**, te containers will be visible at "Containers" section


### 6. Testing local access to CountriesAPI

**6.1.** Access https://localhost:9443 to see the Homepage of CountriesAPI (or other PORT, if you change ``DOCKER_API_PORT`` previously)

**6.2.** You can go straigh to https://localhost:9443/swagger to see API auto-documentation genereted by **swagger**.

[install07]


### 7. Checking sample data

**7.1** Access **Adminer** through https://localhost:9080 to view the **SAMPLE data created** with ``docker-compose up``
```
Credentials:
Server: postgres
User: postgres
Pass: Cric@s
Database: countries_db
```

**NOTICE:** Adminer is on a docker container. So, the server will be the name of the container where PostgreSQL is, in this case ``postgres``. For other DBMSs installed on your PC, the server will be ``localhost``

[install08]

[install09]

.
