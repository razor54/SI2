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
-- prevenir que alguém apague a estada a meio da execução
create procedure inserirExtraPessoal
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
						values(@id_fatura, @descrição, @preço_dia, 'Extra Hóspede')
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

	exec inserirExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
		N'15', N'Hóspede', N'9999'

	select * from Extra
	select * from ExtraEstada
rollback

go

/** REMOVER EXTRA PESSOAL **/
-- prevenir que alguém apague o extra a meio da execução
-- não devemos remover um extra de uma estada ativa
create procedure removerExtraPessoal
	@id numeric, @id_fatura numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Extra where id = @id)
			begin
				begin try
					declare @data_início date, @data_fim date, @id_estada numeric
					select @id_estada = id_estada from ExtraEstada where id_extra = @id
					select @data_fim = data_fim, @data_início = data_início
						from Estada where id = @id_estada
					-- não devo poder remover extra se estiver em estada ativa
					if (@data_fim >= getdate() and @data_início <= getdate())
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
			(N'Não existe nenhum extra com o id pedido',
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

	exec inserirExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
		N'15', N'Hóspede', N'9999'
	exec removerExtraPessoal N'999', N'9999'

	select * from Extra
	select * from ExtraEstada
rollback

go

/** ATUALIZAR EXTRA PESSOAL **/
-- prevenir que alguém apague o extra a meio da execução
-- não atualizamos a fatura porque o cliente paga o valor com que comprou
create procedure atualizarExtraPessoal
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
			(N'Não existe nenhum extra com o id pedido',
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

	exec inserirExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
		N'15', N'Hóspede', N'9999'
	exec atualizarExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
		N'20', N'Hóspede'

	select * from Extra
	select * from ExtraEstada
rollback