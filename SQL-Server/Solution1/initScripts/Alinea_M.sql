SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'somaDosPre�osDasFaturas'
			and type = 'P'
		)
		DROP PROCEDURE dbo.somaDosPre�osDasFaturas
go


create procedure somaDosPre�osDasFaturas
@year numeric, @low int, @top int
as
	begin tran
		begin try
			declare @pre�o_total numeric = 0
			DECLARE @id numeric

			DECLARE MY_CURSOR CURSOR 
FOR 
			SELECT DISTINCT id 
			FROM (select * from Estada where YEAR(data_fim) = @year 
						order by id
						OFFSET     @low ROWS       
						FETCH NEXT @top ROWS ONLY
				)a
			OPEN MY_CURSOR
			FETCH NEXT FROM MY_CURSOR INTO @id
			WHILE @@FETCH_STATUS = 0
			BEGIN 
				declare @pre�o numeric
				--Do something with Id here
				exec pagamentoEstadaComFatura
					@id_estada = @id, @total = @pre�o output

				set @pre�o_total = @pre�o_total + @pre�o

				FETCH NEXT FROM MY_CURSOR INTO @id
			END
			CLOSE MY_CURSOR
			DEALLOCATE MY_CURSOR
			select @pre�o_total
			Print concat('Pre�o total das varias faturas: ', @pre�o_total)
			commit
		end try

		begin catch
			rollback
		end catch
go


---TESTES

--inserir hospede
begin tran
	--SET DATEFORMAT dmy; 
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2016', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(67890, '04-01-2000', '08-01-2016', 222)
	exec inserirH�spedeComEstadaExistente N'222', N'789', N'Pedro', 
		N'Praceta Sem Nome', N'pedro@gmail.com', N'67890'

	--select * from H�spede

	declare @nome_h�spede varchar(128)
	
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)
	
	select @nome_h�spede = nome from H�spede where nif = 222
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(99, 67890, @nome_h�spede, 222)

	insert into dbo.Parque( email , nome, morada ,estrelas)
				values( 'aprqueAQUA@aprqueAQUA.com','Marechal Carmona','Rua sem nome',1)

	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel B�sico de Canoagem'
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Hipismo', 
		N'Marechal Carmona', N'Nivel B�sico de Hipismo'

	exec inscreverH�spedeNumaAtividade N'111', N'Canoagem', N'Marechal Carmona'
	exec inscreverH�spedeNumaAtividade N'222', N'Hipismo', N'Marechal Carmona'
	
	declare @m�dia numeric, @pre�o_estada numeric
	--exec pagamentoEstadaComFatura N'12345', @pre�o_estada output -- ou passar nif?
	--set @m�dia = @pre�o_estada
	--exec pagamentoEstadaComFatura N'67890', @pre�o_estada output -- ou passar nif?
	--set @m�dia += @pre�o_estada

	--select @m�dia

	exec somaDosPre�osDasFaturas
		2016 , 0 , 5

rollback