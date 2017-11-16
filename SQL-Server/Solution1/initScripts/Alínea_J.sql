SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'pagamentoEstadaComFatura'
			and type = 'P'
		)
		DROP PROCEDURE dbo.pagamentoEstadaComFatura

go

/** PAGAMENTO DE UMA ESTADA COM EMISS�O DE FATURA **/

create procedure pagamentoEstadaComFatura
	@id_estada numeric, @total numeric output
as
	if exists(select * from Estada where id = @id_estada)
		begin
			declare @id_fatura numeric, @pre�o_total numeric,
			@descri��o varchar(256), @pre�o numeric,
			@tipo varchar(30), @numH�spedes numeric,
			@texto_fatura varchar(1024)
			--
			set @pre�o_total = 0
			-- declare numH�spedes as count from select by nif
			select @numH�spedes = count(nif_h�spede) from EstadaH�spede
				where id_estada = @id_estada
			-- vamos buscar a fatura respetiva � estada
			select @id_fatura = id from Fatura where id_estada = @id_estada
			-- iniciamos um cursor para iterar sobre os componentes da fatura
			declare cursor_fatura cursor for
			select descri��o, pre�o, tipo from ComponenteFatura where id_fatura = @id_fatura
			open cursor_fatura
			fetch next from cursor_fatura into @descri��o, @pre�o, @tipo
			while @@FETCH_STATUS = 0 -- ??
			begin
				-- concat n�o funciona
				select @texto_fatura = concat(@texto_fatura, 
					concat(char(13), concat(@descri��o, concat(' ', @pre�o))))
				if(@tipo = 'Extra H�spede')
					set @pre�o = @pre�o * @numH�spedes
				set @pre�o_total = @pre�o_total + @pre�o
				fetch next from cursor_fatura into @descri��o, @pre�o, @tipo
			end
			close cursor_fatura
			deallocate cursor_fatura
			-- imprimimos a fatura
			print @texto_fatura
			print concat('Pre�o total: ', @pre�o_total)
			select @total = @pre�o_total
			return
		end
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_h�spede varchar(128)
	select @nome_h�spede = nome from H�spede where nif = 111
	insert into Fatura(id, id_estada, nome_h�spede, nif_h�spede)
		values(9999, 12345, @nome_h�spede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirBungalowNumParque N'45', N'Alojamento Pequeno', N'Cascais', 'Primeiro Alojamento',
		N'3', N'Marechal Carmona', N'T1', N'9999'
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel B�sico de Canoagem'

	exec inscreverH�spedeNumaAtividade N'111', N'Canoagem', N'Marechal Carmona'

	declare @pre�o_total numeric
	exec pagamentoEstadaComFatura N'12345', @pre�o_total output -- ou passar nif?

	--select * from Estada
	--select * from H�spede
	--select * from Parque
	select * from Atividade
	select * from H�spedeAtividade
	select * from Fatura
	select * from ComponenteFatura
rollback