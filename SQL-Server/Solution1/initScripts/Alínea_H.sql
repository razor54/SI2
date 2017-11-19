SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'criarEstadaParaUmPeríodoDeTempo'
            and type = 'P'
		)
		DROP PROCEDURE dbo.criarEstadaParaUmPeríodoDeTempo

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'inserirEstadaComResponsávelExistente'
			and type = 'P'
		)
		DROP PROCEDURE dbo.inserirEstadaComResponsávelExistente

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'inserirEstadaSemResponsávelExistente'
            and type = 'P'
	    )
		DROP PROCEDURE dbo.inserirEstadaSemResponsávelExistente

go

create procedure inserirEstadaComResponsávelExistente
	@id numeric, @data_início Date, 
	@data_fim Date, @nif_hóspede numeric
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Hóspede where nif = @nif_hóspede)
			insert into Estada(id, data_início, data_fim, nif_hóspede)
				values(@id, @data_início, @data_fim, @nif_hóspede)
		else
			raiserror
			(N'Não existe nenhum hóspede com o nif pedido',
			10,
			1);
	commit
go

create procedure inserirEstadaSemResponsávelExistente
	@id numeric, @data_início Date, @data_fim Date, 
	@nif_hóspede numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64)
as
	begin tran
		insert into Hóspede(email, morada, nome, bi, nif)
			values(@email, @morada, @nome, @bi, @nif_hóspede)
		insert into Estada(id, data_início, data_fim, nif_hóspede)
			values(@id, @data_início, @data_fim, @nif_hóspede)
	commit
go

/** CRIAR ESTADA PARA UM PERÍODO DE TEMPO **/
create procedure criarEstadaParaUmPeríodoDeTempo
	@id numeric, @data_início Date, @data_fim Date, @nif_hóspede numeric,
	@bi numeric, @nome_hóspede varchar(128), @morada varchar(128), @email varchar(64),
	@preço_base money, @descrição_alojamento varchar(256), @localização varchar(20), 
	@nome_alojamento varchar(128), @max_pessoas numeric, @nome_parque varchar(56), 
	@tipologia varchar(256), @id_extra_alojamento numeric, @descrição_extra_alojamento varchar(256),
	@preço_extra_alojamento money, @tipo_extra varchar(15), @id_fatura numeric, @id_extra_pessoal numeric,
	@descrição_extra_pessoal varchar(256), @preço_extra_pessoal numeric
as
	begin tran
		begin try
			-- criar estada dado o nif do responsável e o período de tempo
			if exists (select * from Hóspede where nif = @nif_hóspede)
				exec inserirEstadaComResponsávelExistente @id, @data_início, @data_fim, @nif_hóspede
			else
				exec inserirEstadaSemResponsávelExistente @id, @data_início, @data_fim, @nif_hóspede,
					@bi, @nome_hóspede, @morada, @email
			-- adicionar fatura
			insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
				values(@id_fatura, @id, @nome_hóspede, @nif_hóspede)
			-- adicionar um alojamento a essa estada
			exec inserirBungalowNumParque @preço_base, @descrição_alojamento, @localização, @nome_alojamento, 
				@max_pessoas, @nome_parque, @tipologia, @id_fatura
			-- adicionar um hóspede a essa estada
			exec inserirHóspedeComEstadaExistente @nif_hóspede, @bi, @nome_hóspede, 
				@morada, @email, @id
			-- adicionar um extra de alojamento a essa estada
			exec inserirExtraDeAlojamento @id_extra_alojamento, @id, @descrição_extra_alojamento, 
				@preço_extra_alojamento, N'Alojamento', @id_fatura
			-- adicionar um extra pessoal a essa estada
			exec inserirExtraPessoal @id_extra_pessoal, @id, @descrição_extra_pessoal, 
				@preço_extra_pessoal, N'Hóspede', @id_fatura
		end try
		begin catch
			rollback
		end catch
	commit
go