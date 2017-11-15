SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'inserirAtividade'
			and type = 'P'
		)
		DROP PROCEDURE dbo.inserirAtividade

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'removerAtividade'
            and type = 'P'
		)
		DROP PROCEDURE dbo.removerAtividade

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'atualizarAtividade'
			and type = 'P'
		)
		DROP PROCEDURE dbo.atualizarAtividade

go

/** INSERIR ATIVIDADE **/

create procedure inserirAtividade
	@data_atividade Date, @lotação numeric,
	@preço money, @nome_atividade varchar(56),
	@nome_parque varchar(56), @descrição varchar(256)
as
	if exists (select * from Parque where nome = @nome_parque)
		insert into Atividade(data_atividade, preço, lotação, nome_atividade, nome_parque, descrição)
			values(@data_atividade, @preço, @lotação, @nome_atividade, @nome_parque, @descrição)
	else
		raiserror
		(N'Não existe nenhum parque com o nome pedido',
		10,
		1);
go

begin tran
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'

	select * from Atividade
	go
rollback

go

/** REMOVER ATIVIDADE **/

create procedure removerAtividade
	@nome_atividade varchar(56), @nome_parque varchar(56)
as
	if exists (select * from Atividade where nome_atividade = @nome_atividade
		and nome_parque = @nome_parque)
		delete from Atividade where nome_atividade = @nome_atividade
			and nome_parque = @nome_parque
	else
		raiserror
		(N'Não existe nenhum parque com o nome pedido',
		10,
		1);
go

begin tran
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'
	exec removerAtividade N'Canoagem', N'Marechal Carmona'

	select * from Atividade
	go
rollback

go

/** ATUALIZAR ATIVIDADE **/

create procedure atualizarAtividade
	@data_atividade Date, @lotação numeric,
	@preço money, @nome_atividade varchar(56),
	@nome_parque varchar(56), @descrição varchar(256)
as
	if exists (select * from Atividade where nome_parque = @nome_parque
		and nome_atividade = @nome_atividade)
		update Atividade
		set data_atividade = @data_atividade,  preço = @preço,
			lotação = @lotação, descrição = @descrição
		where nome_atividade = @nome_atividade and nome_parque = @nome_parque
	else
		raiserror
		(N'Não existe nenhum parque com o nome pedido',
		10,
		1);
go

begin tran
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'
	exec atualizarAtividade N'01-01-2000', N'25', N'10', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'

	select * from Atividade
	go
rollback

go