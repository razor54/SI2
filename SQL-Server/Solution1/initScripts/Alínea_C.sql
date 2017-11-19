SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

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
-- prevenir que algu�m apague a estada a meio da execu��o
create procedure inserirH�spedeComEstadaExistente 
	@nif numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64), @id_estada numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Estada where id = @id_estada) 
			begin
				begin try
					insert into H�spede(nif, bi, nome, morada, email)
						values(@nif, @bi, @nome, @morada, @email)
					insert into EstadaH�spede(nif_h�spede, id_estada)
						values(@nif, @id_estada)
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

	select * from H�spede
	select * from EstadaH�spede
rollback

go
/** REMOVER H�SPEDE **/
-- prevenir que algu�m apague o h�spede a meio da execu��o
create procedure removerH�spede 
	@nif numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from H�spede where nif = @nif)
			begin
				begin try
					declare @id_estada numeric, @respons�vel numeric
					select @id_estada = id_estada from EstadaH�spede 
						where nif_h�spede = @nif
					if not exists (select nif_h�spede from Estada where nif_h�spede = @nif) 
						throw 10, 'N�o pode remover o h�spede respons�vel por uma estada', 1
					delete from EstadaH�spede where nif_h�spede = @nif
					delete from H�spede where nif = @nif
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'N�o existe nenhum h�spede com o nif pedido',
			10,
			1);
	commit

go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'
	exec removerH�spede N'111'

	select * from H�spede
	select * from EstadaH�spede
rollback

go
/** ATUALIZAR H�SPEDE **/
-- prevenir que algu�m apague o h�spede a meio da execu��o
create procedure atualizarH�spede 
	@nif numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64)
as
	set transaction isolation level repeatable read
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
rollback