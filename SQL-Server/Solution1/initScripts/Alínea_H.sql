SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'criarEstadaParaUmPer�odoDeTempo'
            and type = 'P'
		)
		DROP PROCEDURE dbo.criarEstadaParaUmPer�odoDeTempo

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'inserirEstadaComRespons�velExistente'
			and type = 'P'
		)
		DROP PROCEDURE dbo.inserirEstadaComRespons�velExistente

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'inserirEstadaSemRespons�velExistente'
            and type = 'P'
	    )
		DROP PROCEDURE dbo.inserirEstadaSemRespons�velExistente

go

create procedure inserirEstadaComRespons�velExistente
	@id numeric, @data_in�cio Date, 
	@data_fim Date, @nif_h�spede numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from H�spede where nif = @nif_h�spede)
			insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
				values(@id, @data_in�cio, @data_fim, @nif_h�spede)
		else
			raiserror
			(N'N�o existe nenhum h�spede com o nif pedido',
			10,
			1);
	commit
go

create procedure inserirEstadaSemRespons�velExistente
	@id numeric, @data_in�cio Date, @data_fim Date, 
	@nif_h�spede numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64)
as
	begin tran
		insert into H�spede(email, morada, nome, bi, nif)
			values(@email, @morada, @nome, @bi, @nif_h�spede)
		insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
			values(@id, @data_in�cio, @data_fim, @nif_h�spede)
	commit
go

/** CRIAR ESTADA PARA UM PER�ODO DE TEMPO **/
create procedure criarEstadaParaUmPer�odoDeTempo
	@id numeric, @data_in�cio Date, @data_fim Date, @nif_h�spede numeric,
	@bi numeric, @nome_h�spede varchar(128), @morada varchar(128), @email varchar(64),
	@pre�o_base money, @descri��o_alojamento varchar(256), @localiza��o varchar(20), 
	@nome_alojamento varchar(128), @max_pessoas numeric, @nome_parque varchar(56), 
	@tipologia varchar(256), @id_extra_alojamento numeric, @descri��o_extra_alojamento varchar(256),
	@pre�o_extra_alojamento money, @tipo_extra varchar(15), @id_fatura numeric, @id_extra_pessoal numeric,
	@descri��o_extra_pessoal varchar(256), @pre�o_extra_pessoal numeric
as
	begin tran
		begin try
			-- criar estada dado o nif do respons�vel e o per�odo de tempo
			if exists (select * from H�spede where nif = @nif_h�spede)
				exec inserirEstadaComRespons�velExistente @id, @data_in�cio, @data_fim, @nif_h�spede
			else
				exec inserirEstadaSemRespons�velExistente @id, @data_in�cio, @data_fim, @nif_h�spede,
					@bi, @nome_h�spede, @morada, @email
			-- adicionar fatura
			insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
				values(@id_fatura, @id, @nome_h�spede, @nif_h�spede)
			-- adicionar um alojamento a essa estada
			exec inserirBungalowNumParque @pre�o_base, @descri��o_alojamento, @localiza��o, @nome_alojamento, 
				@max_pessoas, @nome_parque, @tipologia, @id_fatura
			-- adicionar um h�spede a essa estada
			exec inserirH�spedeComEstadaExistente @nif_h�spede, @bi, @nome_h�spede, 
				@morada, @email, @id
			-- adicionar um extra de alojamento a essa estada
			exec inserirExtraDeAlojamento @id_extra_alojamento, @id, @descri��o_extra_alojamento, 
				@pre�o_extra_alojamento, N'Alojamento', @id_fatura
			-- adicionar um extra pessoal a essa estada
			exec inserirExtraPessoal @id_extra_pessoal, @id, @descri��o_extra_pessoal, 
				@pre�o_extra_pessoal, N'H�spede', @id_fatura
		end try
		begin catch
			rollback
		end catch
	commit
go