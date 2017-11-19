SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'mediaDosPreçosDasFaturas'
			and type = 'P'
		)
		DROP PROCEDURE dbo.mediaDosPreçosDasFaturas
go

-- prevenir que alguém apague uma estada durante a execução
create procedure mediaDosPreçosDasFaturas
@year numeric, @low int, @top int
as
	set transaction isolation level repeatable read
	begin tran
		begin try
			declare @media_preços decimal
			select @media_preços=AVG(valor_final)from Fatura inner join Estada on id_estada = Estada.id and pagamento= 'pago'
			Print concat('Média do preço das várias faturas: ', @media_preços)
		end try

		begin catch
			rollback
		end catch
	commit
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(67890, '04-01-2000', '08-01-2000', 222)
	exec inserirHóspedeComEstadaExistente N'222', N'789', N'Pedro', 
		N'Praceta Sem Nome', N'pedro@gmail.com', N'67890'

	declare @nome_hóspede varchar(128)
	
	select @nome_hóspede = nome from Hóspede where nif = 111
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(9999, 12345, @nome_hóspede, 111)
	
	select @nome_hóspede = nome from Hóspede where nif = 222
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(99, 67890, @nome_hóspede, 222)

	insert into dbo.Parque( email , nome, morada ,estrelas)
				values( 'aprqueAQUA@aprqueAQUA.com','Marechal Carmona','Rua sem nome',1)

	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Hipismo', 
		N'Marechal Carmona', N'Nivel Básico de Hipismo'

	exec inscreverHóspedeNumaAtividade N'111', N'Canoagem', N'Marechal Carmona'
	exec inscreverHóspedeNumaAtividade N'222', N'Hipismo', N'Marechal Carmona'

	declare @preço_total numeric
	exec pagamentoEstadaComFatura N'12345', @preço_total output
	exec pagamentoEstadaComFatura N'67890', @preço_total output

	exec mediaDosPreçosDasFaturas 2000, 0, 5

rollback