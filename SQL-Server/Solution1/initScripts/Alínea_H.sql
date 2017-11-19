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
-- INCOMPLETO ???

/** CRIAR ESTADA PARA UM PERÍODO DE TEMPO **/
-- verificar de novo (NÃO ALTERADO)
create procedure criarEstadaParaUmPeríodoDeTempo
	@id numeric, @data_início Date, @data_fim Date, @nif_hóspede numeric,
	@bi numeric, @nome_hóspede varchar(128), @morada varchar(128), @email varchar(64),
	@preço_base money, @descrição_alojamento varchar(256), @localização varchar(20), 
	@nome_alojamento varchar(128), @max_pessoas numeric, @nome_parque varchar(56), 
	@tipologia varchar(256), @id_extra_alojamento numeric, @descrição_extra_alojamento varchar(256),
	@preço_dia_extra money, @tipo_extra varchar(15)
as
	begin tran
		-- criar estada dado o nif do responsável e o período de tempo
		if exists (select * from Hóspede where nif = @nif_hóspede)
			exec inserirEstadaComResponsávelExistente @id, @data_início, @data_fim, @nif_hóspede
		else
			exec inserirEstadaSemResponsávelExistente @id, @data_início, @data_fim, @nif_hóspede,
				@bi, @nome_hóspede, @morada, @email
		-- adicionar um alojamento a essa estada
		exec inserirBungalowNumParque @preço_base, @descrição_alojamento, @localização, @nome_alojamento, 
			@max_pessoas, @nome_parque, @tipologia
		-- adicionar um hóspede a essa estada
		exec inserirHóspedeComEstadaExistente @nif_hóspede, @bi, @nome_hóspede, 
			@morada, @email, @id
		-- adicionar um extra de alojamento a essa estada
		exec inserirExtraDeAlojamento N'999', N'12345', N'Animal de Companhia', 
			N'15', N'Alojamento'
		-- adicionar um extra pessoal a essa estada
		exec inserirExtraPessoal N'999', N'12345', N'Pequeno Almoço', 
			N'15', N'Hóspede'
	commit
go

create procedure inserirEstadaComResponsávelExistente
	@id numeric, @data_início Date, 
	@data_fim Date, @nif_hóspede numeric
as
	if exists (select * from Hóspede where nif = @nif_hóspede)
		insert into Estada(id, data_início, data_fim, nif_hóspede)
			values(@id, @data_início, @data_fim, @nif_hóspede)
	else
		raiserror
		(N'Não existe nenhum hóspede com o nif pedido',
		10,
		1);
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