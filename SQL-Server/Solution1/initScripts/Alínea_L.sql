SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'listarAtividadesComlugares'
			and type = 'P'
		)
		DROP PROCEDURE dbo.listarAtividadesComlugares
go

create procedure listarAtividadesComlugares
	@dataInit Date,@dataFim Date
as
	begin 

		--select count(nif_h�spede) from H�spedeAtividade

		select	n�mero ,
				data_atividade ,
				pre�o ,
				lota��o ,
				nome_atividade ,
				descri��o 
			from dbo.Atividade 
			--DECLARE @NomeAtividade varchar(56);
			-- Initialize the variable.
		--	SET @NomeAtividade = 'atirar pau ao gato';
			where (select count(*) as i
					from 
						( select nif_h�spede from H�spedeAtividade 
								as hospedeAct where hospedeAct.nome_atividade = dbo.Atividade.nome_atividade ) a
					--group by nif_h�spede 
					having count(*)<lota��o
				)<lota��o and data_atividade between @dataInit and @dataFim
	end
go

--inserir hospede
begin tran
	--SET DATEFORMAT dmy; 
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(123456, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'
	exec atualizarH�spede N'111', N'567', N'Jaquim', N'Praceta Sem Nome',
		N'jaquim@gmail.com'

	exec inserirH�spedeComEstadaExistente N'112', N'4562', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'
	exec inserirH�spedeComEstadaExistente N'113', N'4566', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'

	select * from H�spede
	go

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


rollback
