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
-- prevenir que algu�m apague o parque a meio da execu��o
create procedure inserirBungalowNumParque
	@pre�o_base numeric, @descri��o varchar(256),
	@localiza��o varchar(20), @nome varchar(56),
	@max_pessoas numeric, @nome_parque varchar(56),
	@tipologia varchar(2), @id_fatura numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Parque where nome = @nome_parque)
			begin
				begin try
					insert into Alojamento(nome, nome_parque, pre�o_base, descri��o, localiza��o, max_pessoas)
						values(@nome, @nome_parque, @pre�o_base, @descri��o, @localiza��o, @max_pessoas)
					insert into Bungalow(nome_alojamento, tipologia)
						values(@nome, @tipologia)
					insert into ComponenteFatura(id_fatura, descri��o, pre�o, tipo)
						values(@id_fatura, @descri��o, @pre�o_base, 'Alojamento')
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhum parque com o nome pedido',
			10,
			1);
	commit

go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_h�spede varchar(128)
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirBungalowNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'T1', N'9999'
	select * from Alojamento
	select * from Bungalow
rollback

go

-- prevenir que algu�m apague o parque a meio da execu��o
create procedure inserirTendaNumParque
	@pre�o_base numeric, @descri��o varchar(256),
	@localiza��o varchar(20), @nome varchar(56),
	@max_pessoas numeric, @nome_parque varchar(56),
	@tipo varchar(25), @�rea numeric, @id_fatura numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Parque where nome = @nome_parque)
			begin
				begin try
					insert into Alojamento(nome, nome_parque, pre�o_base, descri��o, localiza��o, max_pessoas)
						values(@nome, @nome_parque, @pre�o_base, @descri��o, @localiza��o, @max_pessoas)
					insert into Tenda(nome_alojamento, �rea, tipo)
						values(@nome, @�rea, @tipo)
					insert into ComponenteFatura(id_fatura, descri��o, pre�o, tipo)
						values(@id_fatura, @descri��o, @pre�o_base, 'Alojamento')
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhum parque com o nome pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_h�spede varchar(128)
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirTendaNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'yurt', N'35', N'9999'

	select * from Alojamento
	select * from Tenda
rollback

go

/** REMOVER ALOJAMENTO NUM PARQUE**/
-- prevenir que algu�m apague o bungalow a meio da execu��o
create procedure removerBungalowNumParque
	@nome varchar(56)
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Bungalow where nome_alojamento = @nome)
			begin
				begin try
					-- pode remover se estiver numa estada, mas ter� que reembolsar
					delete from Bungalow where nome_alojamento = @nome
					delete from Alojamento where nome = @nome
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhum alojamento com o nome pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_h�spede varchar(128)
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirBungalowNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'T1', N'9999'
	exec removerBungalowNumParque N'Primeiro Alojamento'

	select * from Alojamento
	select * from Bungalow
rollback

go

-- prevenir que algu�m apague a tenda a meio da execu��o
create procedure removerTendaNumParque
	@nome varchar(56)
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Tenda where nome_alojamento = @nome)
			begin
				begin try
					-- pode remover se estiver numa estada, mas ter� que reembolsar
					delete from Tenda where nome_alojamento = @nome
					delete from Alojamento where nome = @nome
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhum alojamento com o nome pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_h�spede varchar(128)
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirTendaNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'yurt', N'35', N'9999'
	exec removerTendaNumParque N'Primeiro Alojamento'

	select * from Alojamento
	select * from Tenda
rollback

go

/** ATUALIZAR ALOJAMENTO NUM PARQUE**/
-- prevenir que algu�m apague o bungalow a meio da execu��o
create procedure atualizarBungalowNumParque
	@pre�o_base numeric, @descri��o varchar(256),
	@localiza��o varchar(20), @nome varchar(56),
	@max_pessoas numeric, @tipologia varchar(2)
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Bungalow where nome_alojamento = @nome)
			begin
				begin try
					update Bungalow
						set tipologia = @tipologia
						where nome_alojamento = @nome
					update Alojamento
						set pre�o_base = @pre�o_base, descri��o = @descri��o,
							localiza��o = @localiza��o, max_pessoas =  @max_pessoas	-- sem nome_parque
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhum parque com o nome pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_h�spede varchar(128)
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirBungalowNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'T1', N'9999'
	exec atualizarBungalowNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'T2'
	select * from Alojamento
	select * from Bungalow
rollback

go

-- prevenir que algu�m apague a tenda a meio da execu��o
create procedure atualizarTendaNumParque
	@pre�o_base numeric, @descri��o varchar(256),
	@localiza��o varchar(20), @nome varchar(56),
	@max_pessoas numeric, @tipo varchar(25), @�rea numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Tenda where nome_alojamento = @nome)
			begin
				begin try
					update Tenda
						set �rea = @�rea, tipo = @tipo
						where nome_alojamento = @nome
					update Alojamento
						set pre�o_base = @pre�o_base, descri��o = @descri��o,
							localiza��o = @localiza��o, max_pessoas =  @max_pessoas	-- sem nome_parque
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhum parque com o nome pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_h�spede varchar(128)
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirTendaNumParque N'125', N'Alojamento pequeno com bela vista', 
		N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona',
		N'yurt', N'35', N'9999'

	select * from Alojamento
	select * from Tenda
rollback

go
