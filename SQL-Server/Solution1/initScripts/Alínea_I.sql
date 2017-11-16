SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'inscreverHóspedeNumaAtividade'
			and type = 'P'
		)
		DROP PROCEDURE dbo.inscreverHóspedeNumaAtividade

go

/** INSCREVER HÓSPEDE NUMA ATIVIDADE **/

create procedure inscreverHóspedeNumaAtividade
	@nif_hóspede numeric, @nome_atividade varchar(56),
	@nome_parque varchar(56)
as
	if not exists (select * from Hóspede where nif = @nif_hóspede)
		raiserror
		(N'Não existe nenhum hóspede com o nif pedido',
		10,
		1);
	else if not exists (select * from Atividade where nome_atividade = @nome_atividade
		and nome_parque = @nome_parque)
		raiserror
		(N'Não existe nenhuma atividade com o número pedido',
		10,
		1);
	else
		begin
			begin tran
				insert into HóspedeAtividade(nif_hóspede, nome_atividade, nome_parque)
					values(@nif_hóspede, @nome_atividade, @nome_parque)
				declare @id_estada numeric, @descrição varchar(256), 
					@preço numeric, @id_fatura numeric
				-- preparar variáveis
				select @id_estada = id_estada from EstadaHóspede where nif_hóspede = @nif_hóspede
				select @descrição = descrição, @preço = preço from Atividade
					where nome_atividade = @nome_atividade and nome_parque = @nome_parque
				select @id_fatura = id from Fatura where nif_hóspede = @nif_hóspede

				insert into ComponenteFatura(id_fatura, descrição, preço, tipo)
					values(@id_fatura, @descrição, @preço, 'Atividade')
			commit
		end

go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_hóspede varchar(128)
	select * from Hóspede
	select @nome_hóspede = nome from Hóspede where nif = 111
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(9999, 12345, @nome_hóspede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'

	exec inscreverHóspedeNumaAtividade N'111', N'Canoagem', N'Marechal Carmona'

	--select * from Estada
	--select * from Hóspede
	--select * from Parque
	select * from Atividade
	select * from HóspedeAtividade
	select * from Fatura
	select * from ComponenteFatura
rollback