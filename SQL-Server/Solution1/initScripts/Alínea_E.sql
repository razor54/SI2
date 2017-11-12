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

create procedure inserirExtraDeAlojamento
	@id numeric, @id_estada numeric, @descri��o varchar(256),
	@pre�o_dia money, @tipo varchar(15)
as
	if exists (select * from Estada where id = @id_estada)
		begin
			begin tran
				insert into Extra(id, id_estada, descri��o, pre�o_dia, tipo)
					values(@id, @id_estada, @descri��o, @pre�o_dia, @tipo)
				insert into ExtraEstada(id_extra, id_estada)
					values(@id, @id_estada)
			commit
		end
	else
		raiserror
		(N'N�o existe nenhuma estada com o id pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento'

	select * from Extra
	select * from ExtraEstada
	go
rollback

go

/** REMOVER EXTRA DE ALOJAMENTO **/

create procedure removerExtraDeAlojamento
	@id numeric
as
	if exists (select * from Extra where id = @id)
		begin
			begin tran
				delete from ExtraEstada where id_extra = @id
				delete from Extra where id = @id
			commit
		end
	else
		raiserror
		(N'N�o existe nenhuma estada com o id pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento'
	exec removerExtraDeAlojamento N'999'

	select * from Extra
	select * from ExtraEstada
	go
rollback

go

/** ATUALIZAR EXTRA DE ALOJAMENTO **/

create procedure atualizarExtraDeAlojamento
	@id numeric, @id_estada numeric, @descri��o varchar(256),
	@pre�o_dia money, @tipo varchar(15)
as
	if exists (select * from Extra where id = @id)
		begin
			begin tran
				update Extra
				set descri��o = @descri��o, pre�o_dia = @pre�o_dia
				where id = @id
			commit
		end
	else
		raiserror
		(N'N�o existe nenhuma estada com o id pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento'
	exec atualizarExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'20', N'Alojamento'

	select * from Extra
	select * from ExtraEstada
	go
rollback

go