SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF OBJECT_ID('Bungalow') IS NOT NULL
	DROP TABLE Bungalow

IF OBJECT_ID('Tenda') IS NOT NULL
	DROP TABLE Tenda

IF OBJECT_ID('Fatura') IS NOT NULL
	DROP TABLE Fatura

IF OBJECT_ID('Atividade') IS NOT NULL
	DROP TABLE Atividade

IF OBJECT_ID('Extra') IS NOT NULL
	DROP TABLE Extra

IF OBJECT_ID('Hospede') IS NOT NULL
	DROP TABLE Hospede

IF OBJECT_ID('Estada') IS NOT NULL
	DROP TABLE Estada

IF OBJECT_ID('Alojamento') IS NOT NULL
	DROP TABLE Alojamento

IF OBJECT_ID('Parque') IS NOT NULL
	DROP TABLE Parque



create table Parque (
   email varchar(50) not null,
   nome varchar(56)primary key not null,
   morada varchar(256) not null,
   estrelas numeric
   -- telefone?????

)

create table Alojamento(
	pre�o_base numeric not null ,
	descri��o varchar(256) ,
	localiza��o varchar(20)not null,
	nome_alojamento varchar (56) primary key not null,
	nome_parque varchar (56) not null references Parque(nome),
	max_pessoas numeric not null
)

create table Bungalow(
	tipologia varchar(256) not null,
	nome varchar(56)not null references alojamento(nome_alojamento)
)


create table Tenda(
	area numeric not null,
	nome varchar(56) not null references alojamento(nome_alojamento),
	tipo varchar(25) not null
)

create table Hospede(
   email varchar(64) ,
   morada varchar(128) not null,
   nome varchar(128)not null,
   bi numeric unique not null,
   nif numeric primary key not null
)

create table Estada(
	id numeric primary key not null,
	data_inicio Date not null,
	data_fim Date not null,
	nome_alojamento varchar(56) not null references alojamento(nome_alojamento),
	respons�vel varchar(128) not null
)

create table Atividade(
	n�mero numeric primary key not null identity,
	data_atividade Date not null,
	pre�o money not null,
	lota�ao numeric not null,
	nome_atividade varchar(56) not null,
	nome_parque varchar(56) not null references Parque(nome),
	descri��o varchar(526)
)

create table Extra(
	id  numeric primary key not null,
	id_estada numeric references estada(id),
--	nif numeric references hospede(nif),
--	nome_alojamento varchar(56) references alojamento(nome_alojamento),
	descri�ao varchar(64),
	pre�o_dia money not null,
	tipo varchar(15) --RI extra de hospede ou alojamento

)

create table Fatura(
	id numeric primary key not null,
	nome_hospede varchar(128) not null,
	nif_hospede numeric not null references Hospede(nif),
	nr_atividade numeric not null references atividade(n�mero),
	nome_alojamento varchar(56) not null references Alojamento(nome_alojamento),
	id_extra numeric not null references Extra(id)
) --n�o est� correto