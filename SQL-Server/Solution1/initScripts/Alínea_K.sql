SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO
-- acrescentar pagamento!!
IF EXISTS (
		select type_desc, type
		from sys.procedures with(nolock)
		where name = 'enviarEmail'
			and type = 'P'
		)
		DROP PROCEDURE dbo.enviarEmail

IF EXISTS (
        select type_desc, type
        from sys.procedures with(nolock)
        where name = 'enviarEmailsNumIntervaloTemporal'
            and type = 'P'
	    )
		DROP PROCEDURE dbo.enviarEmailsNumIntervaloTemporal

go

/** ENVIAR UM EMAIL **/

create procedure enviarEmail
	@nif_hóspede numeric, @mensagem varchar(512)
as
	if exists (select * from Hóspede where nif = @nif_hóspede)
		begin
			select @mensagem = CONCAT(@mensagem, char(13))
			print @mensagem
		end
	else
		raiserror
			(N'Não existe nenhuma estada com o id pedido',
			10,
			1);
go

/** ENVIAR EMAILS COM ESTADAS A INICIAR BREVEMENTE **/

create procedure enviarEmailsNumIntervaloTemporal
	@dias numeric
as
	declare @início date = '01-01-2000'--getdate()
	declare @fim date = dateadd(DAY, @dias, @início), @nif numeric

	select * from Estada
	declare cursor_fatura cursor for
	select nif_hóspede from Estada where data_início between @início and @fim
	open cursor_fatura
	fetch next from cursor_fatura into @nif
	while @@FETCH_STATUS = 0 -- ??
	begin
		exec enviarEmail @nif, N'Tem uma estada marcada para breve'
		fetch next from cursor_fatura into @nif
	end
	close cursor_fatura
	deallocate cursor_fatura
go

begin tran
	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(12345, '01-02-2000', '01-05-2000', 111)
	exec inserirHóspedeComEstadaExistente N'111', N'456', N'Jaquim', 
		N'Rua Sem Nome', N'jaquim@gmail.com', N'12345'

	insert into Estada(id, data_início, data_fim, nif_hóspede)
		values(67890, '01-04-2000', '01-08-2000', 222)
	exec inserirHóspedeComEstadaExistente N'222', N'789', N'Pedro', 
		N'Praceta Sem Nome', N'pedro@gmail.com', N'67890'

	exec enviarEmailsNumIntervaloTemporal N'5'

rollback