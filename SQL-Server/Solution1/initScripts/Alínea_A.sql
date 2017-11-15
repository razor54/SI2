SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'inserirHóspedeComEstadaExistente'
            and type = 'P'
		)
		DROP PROCEDURE dbo.inserirHóspedeComEstadaExistente

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'removerHóspede'
			and type = 'P'
		)
		DROP PROCEDURE dbo.removerHóspede

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'atualizarHóspede'
            and type = 'P'
	    )
		DROP PROCEDURE dbo.atualizarHóspede

go
/** INSERIR HÓSPEDE **/
create procedure inserirHóspedeComEstadaExistente 
	@nif numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64), @id_estada numeric
as
	if exists (select * from Estada where id = @id_estada) 
		begin
			begin tran
				insert into Hóspede(nif, bi, nome, morada, email)
					values(@nif, @bi, @nome, @morada, @email)
				insert into EstadaHóspede(nif_hóspede, id_estada)
					values(@nif, @id_estada)
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
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	select * from Hóspede
	select * from EstadaHóspede
	go
rollback

go
/** REMOVER HÓSPEDE **/
create procedure removerHóspede 
	@nif numeric
as
	if exists (select * from Hóspede where nif = @nif)
		begin
			begin tran
				declare @id_estada numeric
				select @id_estada = id_estada from EstadaHóspede 
					where nif_hóspede = @nif
				delete from EstadaHóspede where nif_hóspede = @nif
				delete from Hóspede where nif = @nif
				delete from Estada where id = @id_estada
			commit
		end
	else
		raiserror
		(N'Não existe nenhum hóspede com o nif pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'
	exec removerHóspede N'111'

	select * from Hóspede
	select * from EstadaHóspede
	select * from Estada
	go
rollback

go
/** ATUALIZAR HÓSPEDE **/
create procedure atualizarHóspede 
	@nif numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64)
as
	if exists (select * from Hóspede where nif = @nif)
		update Hóspede
		set bi = @bi, nome = @nome, morada = @morada, 
			email = @email
		where nif = @nif
	else
		raiserror
		(N'Não existe nenhum hóspede com o nif pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'
	exec atualizarHóspede N'111', N'567', N'Jaquim', N'Praceta Sem Nome',
		N'jaquim@gmail.com'

	select * from Hóspede
	go
rollback