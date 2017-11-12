SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'inserirExtraPessoal'
            and type = 'P'
		)
		DROP PROCEDURE dbo.inserirExtraPessoal

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'removerExtraPessoal'
			and type = 'P'
		)
		DROP PROCEDURE dbo.removerExtraPessoal

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'atualizarExtraPessoal'
            and type = 'P'
	    )
		DROP PROCEDURE dbo.atualizarExtraPessoal

go

/** INSERIR EXTRA PESSOAL **/

create procedure inserirExtraPessoal
	@id numeric, @id_estada numeric, @descrição varchar(256),
	@preço_dia money, @tipo varchar(15)
as
	if exists (select * from Estada where id = @id_estada)
		begin
			begin tran
				insert into Extra(id, id_estada, descrição, preço_dia, tipo)
					values(@id, @id_estada, @descrição, @preço_dia, @tipo)
				insert into ExtraEstada(id_extra, id_estada)
					values(@id, @id_estada)
			commit
		end
	else
		raiserror
		(N'Não existe nenhuma estada com o id pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
		N'15', N'Hóspede'

	select * from Extra
	select * from ExtraEstada
	go
rollback

go

/** REMOVER EXTRA PESSOAL **/

create procedure removerExtraPessoal
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
		(N'Não existe nenhum extra com o id pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
		N'15', N'Hóspede'
	exec removerExtraPessoal N'999'

	select * from Extra
	select * from ExtraEstada
	go
rollback

go

/** ATUALIZAR EXTRA PESSOAL **/

create procedure atualizarExtraPessoal
	@id numeric, @id_estada numeric, @descrição varchar(256),
	@preço_dia money, @tipo varchar(15)
as
	if exists (select * from Extra where id = @id)
		begin
			begin tran
				update Extra
				set descrição = @descrição, preço_dia = @preço_dia
				where id = @id
			commit
		end
	else
		raiserror
		(N'Não existe nenhum extra com o id pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
		N'15', N'Hóspede'
	exec atualizarExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
		N'20', N'Hóspede'

	select * from Extra
	select * from ExtraEstada
	go
rollback

go