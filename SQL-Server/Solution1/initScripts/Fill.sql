SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

/** INSERIR EXTRA DE ALOJAMENTO **/
create procedure criarExtraAlojamento
	@id numeric, @id_estada numeric, @descri�ao varchar(64),
	@pre�o_dia money, @tipo varchar(15)
as
	if exists (select * from Estada where id = @id_estada)
		insert into Extra(id, id_estada, descri�ao, pre�o_dia, tipo)
			values(@id, @id_estada, @descri�ao, @pre�o_dia, @tipo)
	else
		raiserror
		(N'N�o existe nenhuma estada com o id pedido',
		10,
		1);
go

exec criarExtraAlojamento N'1', N'111', N'Pessoa Extra', N'20', N'Alojamento'
exec criarExtraAlojamento N'1', N'22222', N'Pessoa Extra', N'20', N'Alojamento'

select * from Extra where tipo = 'Alojamento'
go

/** INSERIR EXTRA H�SPEDE **/
create procedure criarExtraHospede
	@id numeric, @id_estada numeric, @descri�ao varchar(64),
	@pre�o_dia money, @tipo varchar(15)
as
	if exists (select * from Estada where id = @id_estada)
		insert into Extra(id, id_estada, descri�ao, pre�o_dia, tipo)
			values(@id, @id_estada, @descri�ao, @pre�o_dia, @tipo)
	else
		raiserror
		(N'N�o existe nenhuma estada com o id pedido',
		10,
		1);
go

exec criarExtraAlojamento N'2', N'111', N'Pequeno Almo�o', N'5', N'H�spede'
exec criarExtraAlojamento N'2', N'22222', N'Pequeno Almo�o', N'5', N'H�spede'

select * from Extra where tipo = 'H�spede'
go

/** INSERIR ATIVIDADE **/
create procedure criarAtividade
	@data_atividade Date, @pre�o money, @lota�ao numeric,
	@nome_atividade varchar(56), @nome_parque varchar(56),
	@descri��o varchar(526)
as
	insert into Atividade(nome_atividade, nome_parque, data_atividade, pre�o, lota�ao, descri��o)
		values(@nome_atividade, @nome_parque, @data_atividade, @pre�o, @lota�ao, @descri��o)
go

exec criarAtividade N'02-01-2000', N'20', N'10', N'Canoagem', N'Marechal Carmona', N'Pequeno circuito de canoagem'

select * from Atividade
go


insert into Parque(email,nome,morada,estrelas)
			values('parque1@email.com','parque1','sem morada', 2)

select * from Alojamento
select *from Parque
select * from H�spede
select * from extra
select *from estada
select *from fatura
select *from ComponenteFatura
select * from EstadaH�spede
select *from EstadaAlojamento
select * from Atividade
select *from H�spedeAtividade


delete H�spedeAtividade
delete Atividade
delete EstadaAlojamento
delete EstadaH�spede
delete ExtraEstada
delete ComponenteFatura
delete fatura
delete extra
delete H�spede
delete Alojamento
delete Parque
delete estada

