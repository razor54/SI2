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

IF OBJECT_ID('EstadaHóspede') IS NOT NULL
	DROP TABLE EstadaHóspede

IF OBJECT_ID('EstadaAlojamento') IS NOT NULL
	DROP TABLE EstadaAlojamento

IF OBJECT_ID('Extra') IS NOT NULL
	DROP TABLE Extra

IF OBJECT_ID('HóspedeAtividade') IS NOT NULL
	DROP TABLE HóspedeAtividade

IF OBJECT_ID('Hóspede') IS NOT NULL
	DROP TABLE Hóspede

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
	preço_base numeric not null ,
	descrição varchar(256) ,
	localização varchar(20)not null,
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
	área numeric not null,
	nome_alojamento varchar(56) not null references Alojamento(nome),
	tipo varchar(25) not null check (tipo = 'yurt' or tipo = 'tipi' or tipo = 'safari')
)

create table Estada(
	id numeric primary key not null,
	data_início Date not null,
	data_fim Date not null,
	nif_hóspede numeric not null,
	pagamento varchar(12) check(pagamento = 'pago')
)

create table Hóspede(
   email varchar(64) ,
   morada varchar(128) not null,
   nome varchar(128)not null,
   bi numeric unique not null,
   nif numeric primary key not null,
   --id_estada numeric not null references Estada(id)
)

create table Atividade(
	número numeric not null identity,
	data_atividade Date not null,
	preço money not null,
	lotação numeric not null,
	nome_atividade varchar(56) not null,
	nome_parque varchar(56) not null references Parque(nome),
	descrição varchar(256),
	primary key (número, nome_atividade, nome_parque)
)

create table Extra(
	id  numeric primary key not null,
	--id_estada numeric references estada(id),
	descrição varchar(256),
	preço_dia money not null,
	tipo varchar(15) check (tipo = 'Alojamento' or tipo = 'Hóspede')
)

create table Fatura(
	id numeric not null primary key,
	id_estada numeric not null references Estada(id),
	nome_hóspede varchar(128) not null,
	nif_hóspede numeric not null references Hóspede(nif),
)

create table ComponenteFatura(
	id_fatura numeric primary key not null references Fatura(id),
	descrição varchar(256) not null,
	preço numeric not null,
	tipo varchar(30) not null check (tipo = 'Alojamento' or tipo = 'Extra' or tipo = 'Atividade')
)

create table ExtraEstada(
	id_extra numeric not null references Extra(id),
	id_estada numeric not null references Estada(id),
	preço_dia money not null,-- references Extra(preço_dia),
	descrição varchar(256) not null,-- references Extra(descrição),
	primary key (id_extra, id_estada)
)

create table EstadaHóspede(
	nif_hóspede numeric not null references Hóspede(nif),
	id_estada numeric not null references Estada(id),
	primary key (nif_hóspede, id_estada)
)

create table EstadaAlojamento(
	nome_alojamento varchar(56) not null references Alojamento(nome),
	id_estada numeric not null references Estada(id),
	preço_base money not null,-- references Alojamento(preço_base),
	descrição varchar(256) not null, -- references Alojamento(descrição),
	primary key (nome_alojamento, id_estada)
)

create table HóspedeAtividade(
	nif_hóspede numeric not null references Hóspede(nif),
	nome_atividade varchar(56) not null,-- references Atividade(nome_atividade),
	nome_parque varchar(56) not null,-- references Atividade(nome_parque),
	primary key(nif_hóspede, nome_atividade, nome_parque)
)