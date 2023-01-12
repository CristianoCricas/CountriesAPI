#### _Created by Cristiano Santana (Cristiano Cricas)_

# CountriesAPI
> This is an API developed in ASP.NET Core utilizing the C# Web API Template.


## ABOUT
> **[It's works like a simple Country ISO 3166-1 Application](https://en.wikipedia.org/wiki/ISO_3166-1)**


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


## Issues and Improvements
- [Known issues](https://github.com/CristianoCricas/CountriesAPI/issues/1)
- [Necessary improvements](https://github.com/CristianoCricas/CountriesAPI/issues/2)


# REQUIRED APPS
* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [Docker and Docker compose](https://docs.docker.com/compose/) 



## INSTALATION

### 1. Install Visual Studio
**1.1.** If don't have it, I recomend Community version: https://visualstudio.microsoft.com/vs/community

**1.2.** Must have, at least, **ASP.NET web development** (all recommended packages of the first installation such as .NET 6.0, .NET SDK, etc)
![example_install01](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install01.png?raw=true)


**1.3. Plus:** install GIT for Windows (on Individual Components)
![example_install02](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install02.png?raw=true)


### 2. Install and configure docker compose 
**2.1.** If using Windows, I recommend Docker Desktop, because it's easy to use: https://docs.docker.com/desktop/install/windows-install


### 3. Clone the repository on Visual Studio


### 4. After clone, open ``Powershell`` on the project's root folder (or use *Command Windows* on Visual Studio), and run the commands below:

**4.1.** Script to create a Dev Certificate to HTTPS works

````
.\InitializeDevCertificate.ps1
````

**WARNING:** it may show a confirmation dialogue. Click ``YES`` to create the **Dev trusted certificate**. It'll be created in the ``/src`` directory (**needed** for Docker Compose to use it later.)
![example_install03](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install03.png?raw=true)

**4.2.** After configure and start Docker, build and start API container:

````
docker-compose build
````
````
docker-compose up
````
![example_install04](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install04.png?raw=true)

**TIP:** before run ``docker-compose up``, you can change the **PORTS** of API, PostgreSQL and Adminer.
On the root folder is the ``.env`` file where you can change the Environment variables used to set apps Ports.

The Default is:
````
DOCKER_API_PORT=9443
DOCKER_POSTGRES_PORT=5432
DOCKER_ADMINER_PORT=9080
````
![example_install05](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install05.png?raw=true)


### 5. Confirm if the container is OK, with the command:
````
docker container ls
````

**5.1.** - if it's OK, will show the containers:
- **adminer**
- **countriesapi**
- **postgres**

![example_install06](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install06.png?raw=true)

**5.2.** When using **Docker Desktop**, te containers will be visible at **"Containers"** section.


### 6. Testing local access to CountriesAPI

**6.1.** Access https://localhost:9443 to see the Homepage of CountriesAPI (or other PORT, if you change ``DOCKER_API_PORT`` previously)

**6.2.** You can go straigh to https://localhost:9443/swagger to see API auto-documentation genereted by **swagger**.
![example_install07](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install07.png?raw=true)


### 7. Checking sample data

**7.1** Access **Adminer** through https://localhost:9080 to view the **SAMPLE data** created with ``docker-compose up``, use the following credentials:
````
Server: postgres
User: postgres
Pass: Cric@s
Database: countries_db
````

**NOTICE:** Adminer is on a docker container. So, the server will be the name of the container where PostgreSQL is, in this case ``postgres``. For other DBMSs installed on your PC, the server will be ``localhost``
![example_install08](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install08.png?raw=true)
![example_install09](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_install09.png?raw=true)


## RUNNING AUTOMATED TESTS

1. After **building by docker**, it is usually already possible to run the project in Visual Studio. Also, the ***postgres container is necessary to run the TIs*** (Tests of Integration).

2. Access the **"Test Explorer"** on Visual Studio and run the Tests.
![example_tests01](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_tests01.png?raw=true)

3. Other way of Run the tests is open ``Powershell`` on the project's root folder and run the command script:
````
.\RunXUnitTests.ps1
````
![example_tests02](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_tests02.png?raw=true)


# USING API


One way for use the API is tests it's functions via **Swagger**, where all the methods are listed. But, another way is consume the API with another application. For this example, let's use **[Postman](https://www.postman.com/downloads)**.

The 2 items below describes how to use the **"CRUD"** os this API


## 1. Operations (CUD: Create, Update, Delete)

### 1.1. Register a country (name, alpha code, numeric code, etc)
- Use ``HTTP POST`` on https://localhost:9443/api/countries
- The JSON must have: ``name, alpha2code, alpha3code, numericcode, independent``

After send Request, the response will have the **Country Properties and it's ID that was generated.**
![example_postman01](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman01.png?raw=true)


### 1.2. Register a country subdivision
- Use ``HTTP POST`` on https://localhost:9443/api/countries/{countryId}/subdivisions
- The ``countryId`` param must have the **Country ID of the new subdivision**.
- And the JSON must have: ``name, category, subcode``

The response will have the **Subdivision Properties and it's ID that was generated.**
![example_postman02](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman02.png?raw=true)


### 1.3. Update a country information
- Use ``HTTP PUT`` on https://localhost:9443/api/countries/{id}
- The ``id`` param must have the **Country ID that will be Updated.**
- The JSON must have: ``name, alpha2code, alpha3code, numericcode, independent``

After  send Request, the response will be **204 "No content".**
![example_postman03](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman03.png?raw=true)


### 1.4. Update a country subdivisions information
- Use ``HTTP PUT`` on https://localhost:9443/api/countries/{countryId}/subdivisions/{id}
- The ``countryId`` param must have the **Country ID of the subdivision.**
- The ``id`` param must have the **Subdivision ID that will be Updated.**
- And the JSON must have: ``name, category, subcode``

After  send Request, the response will be **204 "No content".**
![example_postman04](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman04.png?raw=true)


### 1.5. Delete a country
- Use ``HTTP DELETE`` on https://localhost:9443/api/countries/{id}
- The ``id`` param must have the **Country ID that will be DELETED.**

After send Request, the response will be **204 "No content".**

**WARNING:** when delete a Country, all of it's Subdivisions WILL BE DELETED too!
![example_postman05](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman05.png?raw=true)


### 1.6. Delete a country subdivision
- Use ``HTTP DELETE`` on https://localhost:9443/api/countries/{countryId}/subdivisions/{id}
- The ``countryId`` param must have the **Country ID of the subdivision.**
- The ``id`` param must have the **Subdivision ID that will be DELETED.**

After send Request, the response will be **204 "No content".**
![example_postman06](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman06.png?raw=true)


## 2. Querying data (R: Read and searches) 

### 2.1. List multiple countries
- Use ``HTTP POST`` on https://localhost:9443/api/countries/search
- The JSON must have: **list of Country IDs**, in a simple StringList. 
- Example:
````
[
    "419c1c15-240f-458f-8cc7-f1e54016b6be",
    "132d4238-1b79-49de-b042-52d800bdd9a2",
    "4012a996-2c08-406e-810d-475d7a325f46"
]
````

After send Request, the response will **list the Countries according to IDs.**
![example_postman07](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman07.png?raw=true)


### 2.2. Filter by Name and AlphaCode
- Use ``HTTP GET`` on https://localhost:9443/api/countries/+QueryStrings
- Available query strings: ``name, alpha2code, alpha3code``
- Example: https://localhost:9443/api/Countries?alpha2code=R

After send Request, the response will list the Countries according to the filters.

**TIP:** if there is no query string, the API will **list all countries on Database.**
![example_postman08](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman08.png?raw=true)


### 2.3. Totalize the amount of obtained countries
- Use ``HTTP GET`` on https://localhost:9443/api/countries/total

The response will list the **TOTAL of Countries on Database.**
![example_postman09](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman09.png?raw=true)


### 2.4. Consult the information of a country subdivision
- Use ``HTTP GET`` on https://localhost:9443/api/countries/{countryId}/subdivisions/{id}
- The ``countryId`` param must have the **Country ID of the subdivision.**
- The ``id`` param must have the **Subdivision ID that will be consulted.**

The response will show the **Subdivision properties of the Country.**

**TIP:** if there is no ID for subdivisions, the API will list all subdivisions of the current Country.
![example_postman10](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman10.png?raw=true)


### 2.5. List of multiple subdivisions of a country 
- Use ``HTTP POST`` on https://localhost:9443/api/countries/{countryId}/subdivisions/search
- The ``countryId`` param must have the **Country ID of the subdivision.**
- And, like item **2.1**, the JSON must have: **list of Subdivisions IDs**, in a simple StringList. 
- Example:
````
[
    "543285f0-6e35-4499-96a0-b224c8b757e6",
    "6f9e41ba-7b53-4cc0-8b95-4b7082976385",
    "ddef3e84-556a-4c10-96e1-51ce3194cf1c",
    "366335a7-bdf5-488c-a137-0af4de78cc45"
]
````

The response will **list the Subdivisions of a Country according to IDs.**
![example_postman11](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman11.png?raw=true)


### 2.6. Totalize the amount of obtained country subdivisions 
Use ``HTTP GET`` on https://localhost:9443/api/countries/{countryId}/subdivisions/total

The ``countryId`` param must have the **Country ID of the subdivision.**

The response will list the **TOTAL of the Country Subdivisions.**
![example_postman12](https://github.com/CristianoCricas/CountriesAPI/blob/main/imgs/example_postman12.png?raw=true)

