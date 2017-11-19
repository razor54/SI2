SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'inserirAtividade'
			and type = 'P'
		)
		DROP PROCEDURE dbo.inserirAtividade

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'removerAtividade'
            and type = 'P'
		)
		DROP PROCEDURE dbo.removerAtividade

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'atualizarAtividade'
			and type = 'P'
		)
		DROP PROCEDURE dbo.atualizarAtividade

go

/** INSERIR ATIVIDADE **/
-- prevenir que alguém apague o parque a meio da execução
create procedure inserirAtividade
	@data_atividade Date, @lotação numeric,
	@preço money, @nome_atividade varchar(56),
	@nome_parque varchar(56), @descrição varchar(256)
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Parque where nome = @nome_parque)
			begin
				begin try
					insert into Atividade(data_atividade, preço, lotação, nome_atividade, nome_parque, descrição)
						values(@data_atividade, @preço, @lotação, @nome_atividade, @nome_parque, @descrição)
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'Não existe nenhum parque com o nome pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'

	select * from Atividade
rollback

go

/** REMOVER ATIVIDADE **/
-- prevenir que alguém apague a atitivade a meio da execução
-- pode-se apagar uma atividade mas terá que se reembolsar os inscritos
-- se já estiver a decorrer, não se pode remover
create procedure removerAtividade
	@nome_atividade varchar(56), @nome_parque varchar(56)
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Atividade where nome_atividade = @nome_atividade
			and nome_parque = @nome_parque)
			begin
				begin try
					declare @data_atividade date
					select @data_atividade = data_atividade from Atividade where nome_atividade = @nome_atividade
					if (@data_atividade = getdate())
						throw 10, 'Atividade a decorrer', 1
					delete from Atividade where nome_atividade = @nome_atividade
						and nome_parque = @nome_parque
				end try
				begin catch
					rollback
				end catch
			end

		else
			raiserror
			(N'Não existe nenhum parque com o nome pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'
	exec removerAtividade N'Canoagem', N'Marechal Carmona'

	select * from Atividade
	go
rollback

go

/** ATUALIZAR ATIVIDADE **/
-- prevenir que alguém apague a atividade a meio da execução
create procedure atualizarAtividade
	@data_atividade Date, @lotação numeric,
	@preço money, @nome_atividade varchar(56),
	@nome_parque varchar(56), @descrição varchar(256)
as
	set transaction isolation level repeatable read
	begin tran
		if exists (select * from Atividade where nome_parque = @nome_parque
			and nome_atividade = @nome_atividade)
			begin
				begin try
					update Atividade
						set data_atividade = @data_atividade,  preço = @preço,
							lotação = @lotação, descrição = @descrição
						where nome_atividade = @nome_atividade and nome_parque = @nome_parque
				end try
				begin catch
					rollback
				end catch
			end
		else
			raiserror
			(N'Não existe nenhum parque com o nome pedido',
			10,
			1);
	commit
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'
	exec atualizarAtividade N'01-01-2000', N'25', N'10', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'

	select * from Atividade
rollback