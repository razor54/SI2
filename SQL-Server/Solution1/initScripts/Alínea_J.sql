SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

/** PAGAMENTO DE UMA ESTADA COM EMISS�O DE FATURA **/

create procedure pagamentoEstadaComFatura
	@id_estada numeric
as
	if exists(select * from Estada where id = @id_estada)
		begin
			declare @id_fatura numeric, @pre�o_total money,
			@descri��o varchar(256), @pre�o numeric,
			@tipo varchar(30), @numH�spedes numeric,
			@texto_fatura varchar
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
				set @texto_fatura = @texto_fatura + '\n' + @descri��o + ' - ' + @pre�o 
				if(@tipo = 'H�spede')
					set @pre�o = @pre�o * @numH�spedes
				set @pre�o_total = @pre�o_total + @pre�o
			end
			-- imprimimos a fatura
			print @texto_fatura
		end
go

begin tran
	insert into Estada(id, data_in�cio, data_fim, nif_h�spede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirH�spedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'
	exec inserirH�spedeComEstadaExistente N'222', N'567', N'Pedro', 
		N'Praceta Sem Nome', N'pedro@gmail.com', N'12345'

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel B�sico de Canoagem'

	exec inscreverH�spedeNumaAtividade N'111', N'Canoagem', N'Marechal Carmona'

	select * from Estada
	select * from H�spede
	select * from Parque
	select * from Atividade
	select * from H�spedeAtividade
rollback