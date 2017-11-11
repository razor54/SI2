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

go
/** INSERIR ALOJAMENTO NUM PARQUE**/
-- falta remover alojamento e fazer update segundo as condições

create procedure inserirBungalowNumParque
	@preço_base numeric, @descrição varchar(256),
	@localização varchar(20), @nome varchar(56),
	@max_pessoas numeric, @nome_parque varchar(56),
	@tipologia varchar(2)
as
	if exists (select * from Parque where nome = @nome_parque)
		begin
			begin tran
				insert into Alojamento(nome, nome_parque, preço_base, descrição, localização, max_pessoas)
					values(@nome, @nome_parque, @preço_base, @descrição, @localização, @max_pessoas)
				insert into Bungalow(nome_alojamento, tipologia)
					values(@nome, @tipologia)
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
