CREATE DATABASE BaseVotacaoEventHub
GO

USE BaseVotacaoEventHub
GO

CREATE TABLE dbo.HistoricoProcessamento(
    Id INT IDENTITY(1,1) NOT NULL,
    IdVoto VARCHAR(50) NOT NULL,
    Horario DATETIME NOT NULL,
    Producer VARCHAR(120) NOT NULL,
    Consumer VARCHAR(120) NOT NULL,
    CONSTRAINT PK_HistoricoProcessamento PRIMARY KEY (Id)
)
GO

CREATE TABLE dbo.VotoTecnologia(
    Id INT IDENTITY(1,1) NOT NULL,
    IdVoto VARCHAR(50) NOT NULL,
    Horario DATETIME NOT NULL,
    Tecnologia VARCHAR(50) NOT NULL,
    Consumer VARCHAR(120) NOT NULL,
    CONSTRAINT PK_VotoTecnologia PRIMARY KEY (Id)
)
GO