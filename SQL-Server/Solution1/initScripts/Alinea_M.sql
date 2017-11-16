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
			  LOCAL STATIC READ_ONLY FORWARD_ONLY

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
		values(123456, '01-01-2000', '05-02-2016', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'
	exec atualizarH�spede N'111', N'567', N'Jaquim', N'Praceta Sem Nome',
		N'jaquim@gmail.com'

	exec inserirH�spedeComEstadaExistente N'112', N'4562', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'
	exec inserirH�spedeComEstadaExistente N'113', N'4566', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'

	select * from H�spede

	
	declare @nome_h�spede varchar(128)
	
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 123456, @nome_h�spede, 111)
	
	select @nome_h�spede = nome from H�spede where nif = 112
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(99, 123456, @nome_h�spede, 112)

		select @nome_h�spede = nome from H�spede where nif = 113
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(999, 123456, @nome_h�spede, 113)

	insert into dbo.Parque( email , nome, morada ,estrelas)
				values( 'aprqueAQUA@aprqueAQUA.com','aprqueAQUA','Rua sem nome',1)

	insert into Atividade(
							data_atividade ,
							pre�o ,
							lota��o ,
							nome_atividade ,
							nome_parque ,
							descri��o
						 )
			values('01-01-2017',200,2,'atirar pau ao gato','aprqueAQUA','sem descri�ao'),
				  ('01-02-2016',2020,3,'nata�ao sincronizada','aprqueAQUA','sem descri�ao')
--
	insert into dbo.H�spedeAtividade (nif_h�spede ,nome_atividade ,nome_parque)
		values(113,'atirar pau ao gato','aprqueAQUA'),
			  (112,'atirar pau ao gato','aprqueAQUA')
	
	delete from dbo.H�spedeAtividade where nif_h�spede = 112
	
	select * from H�spedeAtividade
	exec listarAtividadesComlugares N'01-01-2016', N'01-01-2018'

	declare @pre�o_total numeric
	exec pagamentoEstadaComFatura N'123456', @pre�o_total output -- ou passar nif?

	select @pre�o_total

	exec somaDosPre�osDasFaturas
		2016 , 0 , 5 

rollback

