SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

/** PAGAMENTO DE UMA ESTADA COM EMISSÃO DE FATURA **/

create procedure pagamentoEstadaComFatura
	@id_estada numeric
as
	if exists(select * from Estada where id = @id_estada)
		begin
			declare @id_fatura numeric, @preço_total money,
			@descrição varchar(256), @preço numeric,
			@tipo varchar(30), @numHóspedes numeric,
			@texto_fatura varchar
			-- declare numHóspedes as count from select by nif
			select @numHóspedes = count(nif_hóspede) from EstadaHóspede
				where id_estada = @id_estada
			-- vamos buscar a fatura respetiva à estada
			select @id_fatura = id from Fatura where id_estada = @id_estada
			-- iniciamos um cursor para iterar sobre os componentes da fatura
			declare cursor_fatura cursor for
			select descrição, preço, tipo from ComponenteFatura where id_fatura = @id_fatura
			open cursor_fatura
			fetch next from cursor_fatura into @descrição, @preço, @tipo
			while @@FETCH_STATUS = 0 -- ??
			begin
				set @texto_fatura = @texto_fatura + '\n' + @descrição + ' - ' + @preço 
				if(@tipo = 'Hóspede')
					set @preço = @preço * @numHóspedes
				set @preço_total = @preço_total + @preço
			end
			-- imprimimos a fatura
			print @texto_fatura
		end
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'
	exec inserirHóspedeComEstadaExistente N'222', N'567', N'Pedro', 
		N'Praceta Sem Nome', N'pedro@gmail.com', N'12345'

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'

	exec inscreverHóspedeNumaAtividade N'111', N'Canoagem', N'Marechal Carmona'

	select * from Estada
	select * from Hóspede
	select * from Parque
	select * from Atividade
	select * from HóspedeAtividade
rollback