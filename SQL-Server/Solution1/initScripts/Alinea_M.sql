SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'somaDosPreçosDasFaturas'
			and type = 'P'
		)
		DROP PROCEDURE dbo.somaDosPreçosDasFaturas
go


create procedure somaDosPreçosDasFaturas
@year numeric, @low int, @top int
as
	begin tran
		begin try
			declare @preço_total numeric = 0
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
				declare @preço numeric
				--Do something with Id here
				exec pagamentoEstadaComFatura
					@id_estada = @id, @total = @preço output

				set @preço_total = @preço_total + @preço

				FETCH NEXT FROM MY_CURSOR INTO @id
			END
			CLOSE MY_CURSOR
			DEALLOCATE MY_CURSOR
			select @preço_total
			Print concat('Preço total das varias faturas: ', @preço_total)
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
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2016', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(67890, '04-01-2000', '08-01-2016', 222)
	exec inserirHóspedeComEstadaExistente N'222', N'789', N'Pedro', 
		N'Praceta Sem Nome', N'pedro@gmail.com', N'67890'

	--select * from Hóspede

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
	
	declare @média numeric, @preço_estada numeric
	--exec pagamentoEstadaComFatura N'12345', @preço_estada output -- ou passar nif?
	--set @média = @preço_estada
	--exec pagamentoEstadaComFatura N'67890', @preço_estada output -- ou passar nif?
	--set @média += @preço_estada

	--select @média

	exec somaDosPreçosDasFaturas
		2016 , 0 , 5

rollback