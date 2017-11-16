SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'inscreverH�spedeNumaAtividade'
			and type = 'P'
		)
		DROP PROCEDURE dbo.inscreverH�spedeNumaAtividade

go

/** INSCREVER H�SPEDE NUMA ATIVIDADE **/

create procedure inscreverH�spedeNumaAtividade
	@nif_h�spede numeric, @nome_atividade varchar(56),
	@nome_parque varchar(56)
as
	if not exists (select * from H�spede where nif = @nif_h�spede)
		raiserror
		(N'N�o existe nenhum h�spede com o nif pedido',
		10,
		1);
	else if not exists (select * from Atividade where nome_atividade = @nome_atividade
		and nome_parque = @nome_parque)
		raiserror
		(N'N�o existe nenhuma atividade com o n�mero pedido',
		10,
		1);
	else
		begin
			begin tran
				insert into H�spedeAtividade(nif_h�spede, nome_atividade, nome_parque)
					values(@nif_h�spede, @nome_atividade, @nome_parque)
				declare @id_estada numeric, @descri��o varchar(256), 
					@pre�o numeric, @id_fatura numeric
				-- preparar vari�veis
				select @id_estada = id_estada from EstadaH�spede where nif_h�spede = @nif_h�spede
				select @descri��o = descri��o, @pre�o = pre�o from Atividade
					where nome_atividade = @nome_atividade and nome_parque = @nome_parque
				select @id_fatura = id from Fatura where nif_h�spede = @nif_h�spede

				insert into ComponenteFatura(id_fatura, descri��o, pre�o, tipo)
					values(@id_fatura, @descri��o, @pre�o, 'Atividade')
			commit
		end

go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_h�spede varchar(128)
	select * from H�spede
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel B�sico de Canoagem'

	exec inscreverH�spedeNumaAtividade N'111', N'Canoagem', N'Marechal Carmona'

	--select * from Estada
	--select * from H�spede
	--select * from Parque
	select * from Atividade
	select * from H�spedeAtividade
	select * from Fatura
	select * from ComponenteFatura
rollback