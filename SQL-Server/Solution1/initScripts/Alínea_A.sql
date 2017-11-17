SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

-- ser� necess�rio inscrever h�spede sem estada?

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'inserirH�spedeComEstadaExistente'
            and type = 'P'
		)
		DROP PROCEDURE dbo.inserirH�spedeComEstadaExistente

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'removerH�spede'
			and type = 'P'
		)
		DROP PROCEDURE dbo.removerH�spede

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'atualizarH�spede'
            and type = 'P'
	    )
		DROP PROCEDURE dbo.atualizarH�spede

go
/** INSERIR H�SPEDE **/
create procedure inserirH�spedeComEstadaExistente 
	@nif numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64), @id_estada numeric
as
	if exists (select * from Estada where id = @id_estada) 
		begin
			begin tran
				insert into H�spede(nif, bi, nome, morada, email)
					values(@nif, @bi, @nome, @morada, @email)
				insert into EstadaH�spede(nif_h�spede, id_estada)
					values(@nif, @id_estada)
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
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	select * from H�spede
	select * from EstadaH�spede
	go
rollback

go
/** REMOVER H�SPEDE **/
create procedure removerH�spede 
	@nif numeric
as
	if exists (select * from H�spede where nif = @nif)
		begin
			begin tran
				declare @id_estada numeric
				select @id_estada = id_estada from EstadaH�spede 
					where nif_h�spede = @nif
				delete from EstadaH�spede where nif_h�spede = @nif
				delete from H�spede where nif = @nif
				delete from Estada where id = @id_estada
			commit
		end
	else
		raiserror
		(N'N�o existe nenhum h�spede com o nif pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'
	exec removerH�spede N'111'

	select * from H�spede
	select * from EstadaH�spede
	select * from Estada
	go
rollback

go
/** ATUALIZAR H�SPEDE **/
create procedure atualizarH�spede 
	@nif numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64)
as
	if exists (select * from H�spede where nif = @nif)
		update H�spede
		set bi = @bi, nome = @nome, morada = @morada, 
			email = @email
		where nif = @nif
	else
		raiserror
		(N'N�o existe nenhum h�spede com o nif pedido',
		10,
		1);
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'
	exec atualizarH�spede N'111', N'567', N'Jaquim', N'Praceta Sem Nome',
		N'jaquim@gmail.com'

	select * from H�spede
	go
rollback