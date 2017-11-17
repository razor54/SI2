SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'inserirBungalowNumParque'
            and type = 'P'
		)
		DROP PROCEDURE dbo.inserirBungalowNumParque

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'inserirTendaNumParque'
			and type = 'P'
		)
		DROP PROCEDURE dbo.inserirTendaNumParque

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'removerBungalowNumParque'
            and type = 'P'
		)
		DROP PROCEDURE dbo.removerBungalowNumParque

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'removerTendaNumParque'
			and type = 'P'
		)
		DROP PROCEDURE dbo.removerTendaNumParque

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'atualizarBungalowNumParque'
            and type = 'P'
		)
		DROP PROCEDURE dbo.atualizarBungalowNumParque

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'atualizarTendaNumParque'
			and type = 'P'
		)
		DROP PROCEDURE dbo.atualizarTendaNumParque

go

/** INSERIR ALOJAMENTO NUM PARQUE**/

create procedure inserirBungalowNumParque
	@preço_base numeric, @descrição varchar(256),
	@localização varchar(20), @nome varchar(56),
	@max_pessoas numeric, @nome_parque varchar(56),
	@tipologia varchar(2), @id_fatura numeric
as
	if exists (select * from Parque where nome = @nome_parque)
		begin
			begin tran
				insert into Alojamento(nome, nome_parque, preço_base, descrição, localização, max_pessoas)
					values(@nome, @nome_parque, @preço_base, @descrição, @localização, @max_pessoas)
				insert into Bungalow(nome_alojamento, tipologia)
					values(@nome, @tipologia)
				insert into ComponenteFatura(id_fatura, descrição, preço, tipo)
					values(@id_fatura, @descrição, @preço_base, 'Alojamento')
			commit
		end
	else
		raiserror
		(N'Não existe nenhum parque com o nome pedido',
		10,
		1);
go

begin tran
	--inserir estada, fatura e hóspede
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirBungalowNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'T1', N''
	select * from Alojamento
	select * from Bungalow
	go
rollback

go

create procedure inserirTendaNumParque
	@preço_base numeric, @descrição varchar(256),
	@localização varchar(20), @nome varchar(56),
	@max_pessoas numeric, @nome_parque varchar(56),
	@tipo varchar(25), @área numeric
as
	if exists (select * from Parque where nome = @nome_parque)
		begin
			begin tran
				insert into Alojamento(nome, nome_parque, preço_base, descrição, localização, max_pessoas)
					values(@nome, @nome_parque, @preço_base, @descrição, @localização, @max_pessoas)
				insert into Tenda(nome_alojamento, área, tipo)
					values(@nome, @área, @tipo)
			commit
		end
	else
		raiserror
		(N'Não existe nenhum parque com o nome pedido',
		10,
		1);
go

begin tran
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirTendaNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'yurt', N'35'

	select * from Alojamento
	select * from Tenda
	go
rollback

go

/** REMOVER ALOJAMENTO NUM PARQUE**/

create procedure removerBungalowNumParque
	@nome varchar(56)
as
	if exists (select * from Bungalow where nome_alojamento = @nome)
		begin
			begin tran
				delete from Bungalow where nome_alojamento = @nome
				delete from Alojamento where nome = @nome
			commit
		end
	else
		raiserror
		(N'Não existe nenhum alojamento com o nome pedido',
		10,
		1);
go

begin tran
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirBungalowNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'T1'
	exec removerBungalowNumParque N'Primeiro Alojamento'

	select * from Alojamento
	select * from Bungalow
	go
rollback

go

create procedure removerTendaNumParque
	@nome varchar(56)
as
	if exists (select * from Tenda where nome_alojamento = @nome)
		begin
			begin tran
				delete from Tenda where nome_alojamento = @nome
				delete from Alojamento where nome = @nome
			commit
		end
	else
		raiserror
		(N'Não existe nenhum alojamento com o nome pedido',
		10,
		1);
go

begin tran
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirTendaNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'yurt', N'35'
	exec removerTendaNumParque N'Primeiro Alojamento'

	select * from Alojamento
	select * from Tenda
	go
rollback

go

/** ATUALIZAR ALOJAMENTO NUM PARQUE**/

create procedure atualizarBungalowNumParque
	@preço_base numeric, @descrição varchar(256),
	@localização varchar(20), @nome varchar(56),
	@max_pessoas numeric, @tipologia varchar(2)
as
	if exists (select * from Bungalow where nome_alojamento = @nome)
		begin
			begin tran
				update Bungalow
					set tipologia = @tipologia
					where nome_alojamento = @nome
				update Alojamento
					set preço_base = @preço_base, descrição = @descrição,
						localização = @localização, max_pessoas =  @max_pessoas	-- sem nome_parque
			commit
		end
	else
		raiserror
		(N'Não existe nenhum parque com o nome pedido',
		10,
		1);
go

begin tran
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirBungalowNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'T1'
	exec atualizarBungalowNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'T2'
	select * from Alojamento
	select * from Bungalow
	go
rollback

go

create procedure atualizarTendaNumParque
	@preço_base numeric, @descrição varchar(256),
	@localização varchar(20), @nome varchar(56),
	@max_pessoas numeric, @tipo varchar(25), @área numeric
as
	if exists (select * from Bungalow where nome_alojamento = @nome)
		begin
			begin tran
				update Tenda
					set área = @área, tipo = @tipo
					where nome_alojamento = @nome
				update Alojamento
					set preço_base = @preço_base, descrição = @descrição,
						localização = @localização, max_pessoas =  @max_pessoas	-- sem nome_parque
			commit
		end
	else
		raiserror
		(N'Não existe nenhum parque com o nome pedido',
		10,
		1);
go

begin tran
	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirTendaNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'yurt', N'35'

	select * from Alojamento
	select * from Tenda
	go
rollback

go
