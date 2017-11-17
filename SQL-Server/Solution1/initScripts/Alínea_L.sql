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

		--select count(nif_hóspede) from HóspedeAtividade

		select	número ,
				data_atividade ,
				preço ,
				lotação ,
				nome_atividade ,
				descrição 
			from dbo.Atividade 
			--DECLARE @NomeAtividade varchar(56);
			-- Initialize the variable.
		--	SET @NomeAtividade = 'atirar pau ao gato';
			where (select count(*) as i
					from 
						( select nif_hóspede from HóspedeAtividade 
								as hospedeAct where hospedeAct.nome_atividade = dbo.Atividade.nome_atividade ) a
					--group by nif_hóspede 
					having count(*)<lotação
				)<lotação and data_atividade between @dataInit and @dataFim
	end
go

--inserir hospede
begin tran
	--SET DATEFORMAT dmy; 
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(123456, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'
	exec atualizarHóspede N'111', N'567', N'Jaquim', N'Praceta Sem Nome',
		N'jaquim@gmail.com'

	exec inserirHóspedeComEstadaExistente N'112', N'4562', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'
	exec inserirHóspedeComEstadaExistente N'113', N'4566', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'123456'

	select * from Hóspede
	go

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


rollback
