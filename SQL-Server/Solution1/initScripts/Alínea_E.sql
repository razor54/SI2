SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'inserirExtraDeAlojamento'
            and type = 'P'
		)
		DROP PROCEDURE dbo.inserirExtraDeAlojamento

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'removerExtraDeAlojamento'
			and type = 'P'
		)
		DROP PROCEDURE dbo.removerExtraDeAlojamento

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'atualizarExtraDeAlojamento'
            and type = 'P'
	    )
		DROP PROCEDURE dbo.atualizarExtraDeAlojamento

go

/** INSERIR EXTRA DE ALOJAMENTO**/
-- prevenir que algu�m apague a estada a meio da execu��o
create procedure inserirExtraDeAlojamento
	@id numeric, @id_estada numeric, @descri��o varchar(256),
	@pre�o_dia money, @tipo varchar(15), @id_fatura numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Estada where id = @id_estada)
			begin
				begin try
					insert into Extra(id, descri��o, pre�o_dia, tipo)
						values(@id, @descri��o, @pre�o_dia, @tipo)
					insert into ExtraEstada(id_extra, id_estada, pre�o_dia, descri��o)
						values(@id, @id_estada, @pre�o_dia, @descri��o)
					insert into ComponenteFatura(id_fatura, descri��o, pre�o, tipo)
						values(@id_fatura, @descri��o, @pre�o_dia, 'Extra Alojamento')
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhuma estada com o id pedido',
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

	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento', '9999'

	select * from Extra
	select * from ExtraEstada
rollback

go

/** REMOVER EXTRA DE ALOJAMENTO **/
-- prevenir que algu�m apague o extra a meio da execu��o
-- verificar novamente
create procedure removerExtraDeAlojamento
	@id numeric, @id_fatura numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Extra where id = @id)
			begin
				begin try
					declare @data_fim date, @id_estada numeric
					select @id_estada = id_estada from ExtraEstada where id_extra = @id
					select @data_fim = data_fim from Estada where id = @id_estada
					-- n�o devo poder remover extra se estiver em estada ativa
					if (@data_fim >= getdate())
						throw 10, 'Estada ainda est� ativa', 1
					delete from ComponenteFatura where id_fatura = @id_fatura
					delete from ExtraEstada where id_extra = @id
					delete from Extra where id = @id
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhuma estada com o id pedido',
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

	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento', N'9999'
	exec removerExtraDeAlojamento N'999', N'9999'

	select * from Extra
	select * from ExtraEstada
rollback

go

/** ATUALIZAR EXTRA DE ALOJAMENTO **/
-- prevenir que algu�m apague o extra a meio da execu��o
-- n�o devemos atualizar um extra que j� est� inclu�do numa fatura
create procedure atualizarExtraDeAlojamento
	@id numeric, @id_estada numeric, @descri��o varchar(256),
	@pre�o_dia money, @tipo varchar(15)
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Extra where id = @id)
			begin
				begin try
					update Extra
						set descri��o = @descri��o, pre�o_dia = @pre�o_dia
						where id = @id
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhuma estada com o id pedido',
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

	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento', N'9999'
	exec atualizarExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'20', N'Alojamento'

	select * from Extra
	select * from ExtraEstada
rollback

go