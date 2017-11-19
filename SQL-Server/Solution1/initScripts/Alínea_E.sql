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
-- prevenir que alguém apague a estada a meio da execução
create procedure inserirExtraDeAlojamento
	@id numeric, @id_estada numeric, @descrição varchar(256),
	@preço_dia money, @tipo varchar(15), @id_fatura numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Estada where id = @id_estada)
			begin
				begin try
					insert into Extra(id, descrição, preço_dia, tipo)
						values(@id, @descrição, @preço_dia, @tipo)
					insert into ExtraEstada(id_extra, id_estada, preço_dia, descrição)
						values(@id, @id_estada, @preço_dia, @descrição)
					insert into ComponenteFatura(id_fatura, descrição, preço, tipo)
						values(@id_fatura, @descrição, @preço_dia, 'Extra Alojamento')
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'Não existe nenhuma estada com o id pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_hóspede varchar(128)
	select @nome_hóspede = nome from Hóspede where nif = 111
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(9999, 12345, @nome_hóspede, 111)

	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento', '9999'

	select * from Extra
	select * from ExtraEstada
rollback

go

/** REMOVER EXTRA DE ALOJAMENTO **/
-- prevenir que alguém apague o extra a meio da execução
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
					-- não devo poder remover extra se estiver em estada ativa
					if (@data_fim >= getdate())
						throw 10, 'Estada ainda está ativa', 1
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
			(N'Não existe nenhuma estada com o id pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_hóspede varchar(128)
	select @nome_hóspede = nome from Hóspede where nif = 111
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(9999, 12345, @nome_hóspede, 111)

	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento', N'9999'
	exec removerExtraDeAlojamento N'999', N'9999'

	select * from Extra
	select * from ExtraEstada
rollback

go

/** ATUALIZAR EXTRA DE ALOJAMENTO **/
-- prevenir que alguém apague o extra a meio da execução
-- não devemos atualizar um extra que já está incluído numa fatura
create procedure atualizarExtraDeAlojamento
	@id numeric, @id_estada numeric, @descrição varchar(256),
	@preço_dia money, @tipo varchar(15)
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Extra where id = @id)
			begin
				begin try
					update Extra
						set descrição = @descrição, preço_dia = @preço_dia
						where id = @id
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'Não existe nenhuma estada com o id pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_hóspede varchar(128)
	select @nome_hóspede = nome from Hóspede where nif = 111
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(9999, 12345, @nome_hóspede, 111)

	exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'15', N'Alojamento', N'9999'
	exec atualizarExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
		N'20', N'Alojamento'

	select * from Extra
	select * from ExtraEstada
rollback

go