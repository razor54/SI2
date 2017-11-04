SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF OBJECT_ID('Parque') IS NOT NULL
	DROP TABLE Parque

IF OBJECT_ID('Alojamento') IS NOT NULL
	DROP TABLE Alojamento

IF OBJECT_ID('Bungalow') IS NOT NULL
	DROP TABLE Bungalow

IF OBJECT_ID('Tenda') IS NOT NULL
	DROP TABLE Tenda


create table Parque (
   email varchar(50) not null,
   nome varchar(56)primary key not null,
   morada varchar(256) not null,
   estrelas numeric
   -- telefone?????

)

create table Alojamento(
	preço_base numeric not null ,
	descrição varchar(256) ,
	localização varchar(20)not null,
	nome varchar (56) primary key not null,
	max_pessoas numeric not null

)

create table Bungalow(
	tipologia varchar(256) not null,
	nome varchar(56)not null references alojamento(nome)
)


create table Tenda(
	area numeric not null,
	nome varchar(56)not null references alojamento(nome),
	tipo varchar(25)not null
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
	data_fim Date not null
)



create table Atividade(
	número numeric primary key not null identity,
	data_atividade Date not null,
	preço money not null,
	lotaçao numeric not null,
	nome varchar(56) not null,
	descrição varchar(526)
)

create table extra(
	id  numeric  not null,
	id_estada numeric references estada(id),
	nif numeric references hospede(nif),
	nome_alojamento varchar(56) references alojamento(nome),
	descriçao varchar(64),
	preço_dia money not null,
	tipo varchar --RI extra de hospede ou alojamento

)

create table Fatura(
	id numeric primary key not null,
	nome_hospede varchar(128) not null,
	nif_hospede numeric not null references Hospede(nif),
	nr_atividade numeric not null references atividade(número),
	nome_alojamento varchar(56) not null references Alojamento(nome),
	id_extra numeric not null references extra(id)
)