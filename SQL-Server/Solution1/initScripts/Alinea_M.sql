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
		values(123456, '01-01-2000', '05-02-2016', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'
	exec atualizarHóspede N'111', N'567', N'Jaquim', N'Praceta Sem Nome',
		N'jaquim@gmail.com'

	exec inserirHóspedeComEstadaExistente N'112', N'4562', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'
	exec inserirHóspedeComEstadaExistente N'113', N'4566', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'

	select * from Hóspede

	
	declare @nome_hóspede varchar(128)
	
	select @nome_hóspede = nome from Hóspede where nif = 111
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(9999, 123456, @nome_hóspede, 111)
	
	select @nome_hóspede = nome from Hóspede where nif = 112
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(99, 123456, @nome_hóspede, 112)

		select @nome_hóspede = nome from Hóspede where nif = 113
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(999, 123456, @nome_hóspede, 113)

	insert into dbo.Parque( email , nome, morada ,estrelas)
				values( 'aprqueAQUA@aprqueAQUA.com','aprqueAQUA','Rua sem nome',1)

	insert into Atividade(
							data_atividade ,
							preço ,
							lotação ,
							nome_atividade ,
							nome_parque ,
							descrição
						 )
			values('01-01-2017',200,2,'atirar pau ao gato','aprqueAQUA','sem descriçao'),
				  ('01-02-2016',2020,3,'nataçao sincronizada','aprqueAQUA','sem descriçao')
--
	insert into dbo.HóspedeAtividade (nif_hóspede ,nome_atividade ,nome_parque)
		values(113,'atirar pau ao gato','aprqueAQUA'),
			  (112,'atirar pau ao gato','aprqueAQUA')
	
	delete from dbo.HóspedeAtividade where nif_hóspede = 112
	
	select * from HóspedeAtividade
	exec listarAtividadesComlugares N'01-01-2016', N'01-01-2018'

	declare @preço_total numeric
	exec pagamentoEstadaComFatura N'123456', @preço_total output -- ou passar nif?

	select @preço_total

	exec somaDosPreçosDasFaturas
		2016 , 0 , 5 

rollback

