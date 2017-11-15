SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF OBJECT_ID('Bungalow') IS NOT NULL
	DROP TABLE Bungalow

IF OBJECT_ID('Tenda') IS NOT NULL
	DROP TABLE Tenda

IF OBJECT_ID('ComponenteFatura') IS NOT NULL
	DROP TABLE ComponenteFatura

IF OBJECT_ID('Fatura') IS NOT NULL
	DROP TABLE Fatura

IF OBJECT_ID('Atividade') IS NOT NULL
	DROP TABLE Atividade
	
IF OBJECT_ID('ExtraEstada') IS NOT NULL
	DROP TABLE ExtraEstada

IF OBJECT_ID('EstadaH�spede') IS NOT NULL
	DROP TABLE EstadaH�spede

IF OBJECT_ID('EstadaAlojamento') IS NOT NULL
	DROP TABLE EstadaAlojamento

IF OBJECT_ID('Extra') IS NOT NULL
	DROP TABLE Extra

IF OBJECT_ID('H�spedeAtividade') IS NOT NULL
	DROP TABLE H�spedeAtividade

IF OBJECT_ID('H�spede') IS NOT NULL
	DROP TABLE H�spede

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
	nome varchar(56) primary key not null,
	nome_parque varchar(56) not null references Parque(nome),
	max_pessoas numeric not null
)

create table Bungalow(
	tipologia varchar(256) not null check (tipologia = 'T0' or tipologia = 'T1' or
								tipologia = 'T2' or tipologia = 'T3' or tipologia = 'T4'),
	nome_alojamento varchar(56)not null references Alojamento(nome)
)

create table Tenda(
	�rea numeric not null,
	nome_alojamento varchar(56) not null references Alojamento(nome),
	tipo varchar(25) not null check (tipo = 'yurt' or tipo = 'tipi' or tipo = 'safari')
)

create table Estada(
	id numeric primary key not null,
	data_in�cio Date not null,
	data_fim Date not null,
	nif_h�spede numeric not null,
	pagamento varchar(12) check(pagamento = 'pago')
)

create table H�spede(
   email varchar(64) ,
   morada varchar(128) not null,
   nome varchar(128)not null,
   bi numeric unique not null,
   nif numeric primary key not null,
   --id_estada numeric not null references Estada(id)
)

create table Atividade(
	n�mero numeric not null identity,
	data_atividade Date not null,
	pre�o money not null,
	lota��o numeric not null,
	nome_atividade varchar(56) not null,
	nome_parque varchar(56) not null references Parque(nome),
	descri��o varchar(256),
	primary key (n�mero, nome_atividade, nome_parque)
)

create table Extra(
	id  numeric primary key not null,
	--id_estada numeric references estada(id),
	descri��o varchar(256),
	pre�o_dia money not null,
	tipo varchar(15) check (tipo = 'Alojamento' or tipo = 'H�spede')
)

create table Fatura(
	id numeric not null primary key,
	id_estada numeric not null references Estada(id),
	nome_h�spede varchar(128) not null,
	nif_h�spede numeric not null references H�spede(nif),
)

create table ComponenteFatura(
	id_fatura numeric primary key not null references Fatura(id),
	descri��o varchar(256) not null,
	pre�o numeric not null,
	tipo varchar(30) not null check (tipo = 'Alojamento' or tipo = 'Extra' or tipo = 'Atividade')
)

create table ExtraEstada(
	id_extra numeric not null references Extra(id),
	id_estada numeric not null references Estada(id),
	pre�o_dia money not null,-- references Extra(pre�o_dia),
	descri��o varchar(256) not null,-- references Extra(descri��o),
	primary key (id_extra, id_estada)
)

create table EstadaH�spede(
	nif_h�spede numeric not null references H�spede(nif),
	id_estada numeric not null references Estada(id),
	primary key (nif_h�spede, id_estada)
)

create table EstadaAlojamento(
	nome_alojamento varchar(56) not null references Alojamento(nome),
	id_estada numeric not null references Estada(id),
	pre�o_base money not null,-- references Alojamento(pre�o_base),
	descri��o varchar(256) not null, -- references Alojamento(descri��o),
	primary key (nome_alojamento, id_estada)
)

create table H�spedeAtividade(
	nif_h�spede numeric not null references H�spede(nif),
	nome_atividade varchar(56) not null,-- references Atividade(nome_atividade),
	nome_parque varchar(56) not null,-- references Atividade(nome_parque),
	primary key(nif_h�spede, nome_atividade, nome_parque)
)