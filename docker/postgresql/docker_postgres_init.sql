CREATE TABLE "__EFMigrationsHistory" (
	"MigrationId" character varying(150) NOT NULL,
	"ProductVersion" character varying(32) NOT NULL,
CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230108142841_InitialDB', '7.0.1');
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230109225611_TabSubdivisions', '7.0.1');


CREATE TABLE "Countries" (
    "Id" uuid NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Alpha2Code" character varying(2) NOT NULL,
    "Alpha3Code" character varying(3) NOT NULL,
    "NumericCode" integer NOT NULL,
    "Independent" boolean NOT NULL,
    "DateUpdated" timestamp without time zone NOT NULL,
    CONSTRAINT "PK_Countries" PRIMARY KEY ("Id")
);

INSERT INTO "Countries"
("Id", "Name", "Alpha2Code", "Alpha3Code", "NumericCode", "Independent", "DateUpdated")
VALUES('c4d6904a-e7e6-41a2-b7ed-63b1aba71348'::uuid, 'Brazil', 'BR', 'BRA', 76, true, '2023-01-08 15:26:53.282');
INSERT INTO "Countries"
("Id", "Name", "Alpha2Code", "Alpha3Code", "NumericCode", "Independent", "DateUpdated")
VALUES('4012a996-2c08-406e-810d-475d7a325f46'::uuid, 'Argentina', 'AR', 'ARG', 32, true, '2023-01-08 15:30:42.199');
INSERT INTO "Countries"
("Id", "Name", "Alpha2Code", "Alpha3Code", "NumericCode", "Independent", "DateUpdated")
VALUES('c08a7adc-9b75-4501-86d5-35a5210666c7'::uuid, 'United States of America', 'US', 'USA', 840, true, '2023-01-08 15:31:00.745');
INSERT INTO "Countries"
("Id", "Name", "Alpha2Code", "Alpha3Code", "NumericCode", "Independent", "DateUpdated")
VALUES('132d4238-1b79-49de-b042-52d800bdd9a2'::uuid, 'Isle of Man', 'IM', 'IMN', 833, false, '2023-01-08 15:31:03.461');
INSERT INTO "Countries"
("Id", "Name", "Alpha2Code", "Alpha3Code", "NumericCode", "Independent", "DateUpdated")
VALUES('419c1c15-240f-458f-8cc7-f1e54016b6be'::uuid, 'Congo', 'CG', 'COG', 178, true, '2023-01-09 16:34:42.760');


CREATE TABLE "Subdivisions" (
    "Id" uuid NOT NULL,
	"Name" character varying(100) NOT NULL,
	"Category" character varying(100) NOT NULL,
	"SubCode" character varying(5) NOT NULL,
	"CountryId" uuid NOT NULL,
    "DateUpdated" timestamp without time zone NULL,
    CONSTRAINT "PK_Subdivisions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Subdivisions_Countries_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Countries" ("Id") ON DELETE CASCADE
);

INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('543285f0-6e35-4499-96a0-b224c8b757e6'::uuid, 'São Paulo', 'state', 'SP', 'c4d6904a-e7e6-41a2-b7ed-63b1aba71348'::uuid, '2023-01-09 20:05:47.572');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('c70f7a93-fac0-45a6-a63d-fec7b0ed2441'::uuid, 'Rio de Janeiro', 'state', 'RJ', 'c4d6904a-e7e6-41a2-b7ed-63b1aba71348'::uuid, '2023-01-09 20:11:29.075');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('6f9e41ba-7b53-4cc0-8b95-4b7082976385'::uuid, 'Bahia', 'state', 'BA', 'c4d6904a-e7e6-41a2-b7ed-63b1aba71348'::uuid, '2023-01-09 20:12:02.772');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('ef18a9ef-8189-44ee-a716-8774e2d81ce4'::uuid, 'Amazonas', 'state', 'AM', 'c4d6904a-e7e6-41a2-b7ed-63b1aba71348'::uuid, '2023-01-09 20:12:11.710');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('8e439000-9619-4823-b37f-a2cede398d2c'::uuid, 'Distrito Federal', 'federal district', 'DF', 'c4d6904a-e7e6-41a2-b7ed-63b1aba71348'::uuid, '2023-01-09 20:12:41.196');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('fcef3673-b42d-43aa-aa79-d4560ae9ce39'::uuid, 'Buenos Aires', 'province', 'B', '4012a996-2c08-406e-810d-475d7a325f46'::uuid, '2023-01-09 20:14:28.367');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('933cb522-40f6-4923-a8e8-f772d99ae13f'::uuid, ' Ciudad Autónoma de Buenos Aires', 'city', 'C', '4012a996-2c08-406e-810d-475d7a325f46'::uuid, '2023-01-09 20:14:45.991');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('f2db0de0-2cf5-48f6-a744-648f1bc6b6f7'::uuid, 'Córdoba', 'province', 'X', '4012a996-2c08-406e-810d-475d7a325f46'::uuid, '2023-01-09 20:15:18.390');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('a9f59082-4d8f-4db1-8587-e94410c88b2a'::uuid, 'Town districts', '?', '?', '132d4238-1b79-49de-b042-52d800bdd9a2'::uuid, '2023-01-09 20:17:29.795');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('bdcd8733-f0d9-4ef3-83f6-6ab93f070b87'::uuid, 'Joint boards', '?', '?', '132d4238-1b79-49de-b042-52d800bdd9a2'::uuid, '2023-01-09 20:17:44.713');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('4ea75452-00c3-4043-862b-445610acb90d'::uuid, 'Bouenza', 'department', '11', '419c1c15-240f-458f-8cc7-f1e54016b6be'::uuid, '2023-01-09 20:19:12.063');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('7565808b-2650-4986-8234-5f555312f9a4'::uuid, 'Cuvette', 'department', '8', '419c1c15-240f-458f-8cc7-f1e54016b6be'::uuid, '2023-01-09 20:19:41.616');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('a068be52-3381-4ec2-8d7d-8a1802d3305c'::uuid, 'Cuvette-Ouest', 'department', '15', '419c1c15-240f-458f-8cc7-f1e54016b6be'::uuid, '2023-01-09 20:19:52.664');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('fc026223-05c0-49a0-845d-74595952df6d'::uuid, 'Sangha', 'department', '13', '419c1c15-240f-458f-8cc7-f1e54016b6be'::uuid, '2023-01-09 20:20:08.631');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('ca245cc9-27b7-4116-a7fc-260e6731384b'::uuid, 'Brazzaville', 'department', 'BZV', '419c1c15-240f-458f-8cc7-f1e54016b6be'::uuid, '2023-01-09 20:20:55.977');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('6acdda3a-9c93-44cc-b117-45d648bc7d1f'::uuid, 'Alaska', 'State', 'AK', 'c08a7adc-9b75-4501-86d5-35a5210666c7'::uuid, '2023-01-09 20:21:58.764');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('944f52be-3cd8-4d18-9dc6-5f440840459d'::uuid, 'Florida', 'State', 'FL', 'c08a7adc-9b75-4501-86d5-35a5210666c7'::uuid, '2023-01-09 20:22:53.723');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('68287139-c82d-472e-9c12-db320ad8e3ee'::uuid, 'District of Columbia', 'District', 'DC', 'c08a7adc-9b75-4501-86d5-35a5210666c7'::uuid, '2023-01-09 20:23:18.569');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('35fd34c7-5384-4cc7-b635-b8e6ed607cde'::uuid, 'Puerto Rico', 'Outlying area', 'PR', 'c08a7adc-9b75-4501-86d5-35a5210666c7'::uuid, '2023-01-09 20:24:04.005');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('d0b07a29-eb67-4a43-b5f2-0c99b28cbf20'::uuid, 'Nevada', 'State', 'NV', 'c08a7adc-9b75-4501-86d5-35a5210666c7'::uuid, '2023-01-09 20:24:42.870');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('33d7c437-23d6-439c-a8ee-a4e01bc9d31d'::uuid, 'New York', 'State', 'NY', 'c08a7adc-9b75-4501-86d5-35a5210666c7'::uuid, '2023-01-09 20:25:30.531');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('ddef3e84-556a-4c10-96e1-51ce3194cf1c'::uuid, 'Minas Gerais', 'State', 'MG', 'c4d6904a-e7e6-41a2-b7ed-63b1aba71348'::uuid, '2023-01-09 20:26:22.186');
INSERT INTO "Subdivisions"
("Id", "Name", "Category", "SubCode", "CountryId", "DateUpdated")
VALUES('366335a7-bdf5-488c-a137-0af4de78cc45'::uuid, 'Parana', 'State', 'PR', 'c4d6904a-e7e6-41a2-b7ed-63b1aba71348'::uuid, '2023-01-09 20:26:41.379');

