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

/** PAGAMENTO DE UMA ESTADA COM EMISSÃO DE FATURA **/

create procedure pagamentoEstadaComFatura
	@id_estada numeric, @total numeric output
as
	if exists(select * from Estada where id = @id_estada)
		begin
			declare @id_fatura numeric, @preço_total numeric,
			@descrição varchar(256), @preço numeric,
			@tipo varchar(30), @numHóspedes numeric,
			@texto_fatura varchar(1024)
			--
			set @preço_total = 0
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
				-- concat não funciona
				select @texto_fatura = concat(@texto_fatura, 
					concat(char(13), concat(@descrição, concat(' ', @preço))))
				if(@tipo = 'Extra Hóspede')
					set @preço = @preço * @numHóspedes
				set @preço_total = @preço_total + @preço
				fetch next from cursor_fatura into @descrição, @preço, @tipo
			end
			close cursor_fatura
			deallocate cursor_fatura
			-- imprimimos a fatura
			print @texto_fatura
			print concat('Preço total: ', @preço_total)
			select @total = @preço_total
			return
		end
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-01-2000', '05-02-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	declare @nome_hóspede varchar(128)
	select @nome_hóspede = nome from Hóspede where nif = 111
	insert into Fatura(id, id_estada, nome_hóspede, nif_hóspede)
		values(9999, 12345, @nome_hóspede, 111)

	insert into Parque(nome, email, morada, estrelas)
		values('Marechal Carmona', 'mcarmona@gmail.com', 'Rua de Cascais', 4)
	exec inserirBungalowNumParque N'45', N'Alojamento Pequeno', N'Cascais', 'Primeiro Alojamento',
		N'3', N'Marechal Carmona', N'T1', N'9999'
	exec inserirAtividade N'01-01-2000', N'25', N'15', N'Canoagem', 
		N'Marechal Carmona', N'Nivel Básico de Canoagem'

	exec inscreverHóspedeNumaAtividade N'111', N'Canoagem', N'Marechal Carmona'

	declare @preço_total numeric
	exec pagamentoEstadaComFatura N'12345', @preço_total output -- ou passar nif?

	--select * from Estada
	--select * from Hóspede
	--select * from Parque
	select * from Atividade
	select * from HóspedeAtividade
	select * from Fatura
	select * from ComponenteFatura
rollback