CREATE TABLE "Countries" (
	"Id" uuid NOT NULL,
	"Name" varchar(100) NULL,
	"Alpha2Code" varchar(2) NULL,
	"Alpha3Code" varchar(3) NULL,
	"NumericCode" int4 NOT NULL,
	"Independent" bool NOT NULL,
	"DateUpdated" timestamp NOT NULL
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



