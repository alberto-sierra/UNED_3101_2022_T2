create database velocidad

use velocidad

CREATE TABLE velocidad.dbo.carrera (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	Descripcion nvarchar(100)
)

CREATE TABLE velocidad.dbo.corredor (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	IdCarrera int NOT NULL FOREIGN KEY REFERENCES carrera(Id),
	Tiempo time
)
