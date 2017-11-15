SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

IF OBJECT_ID('Bungallows') IS NOT NULL
	DROP view Bungallows
go
IF OBJECT_ID('Bungallows') IS NOT NULL
	DROP view Bungallows
go

Create view Bungallows
as
select tipologia,
	nome_alojamento,
	preço_base ,
	descrição  ,
	localização ,
	nome_parque ,
	max_pessoas,
	email,
    morada,
    estrelas 

	from Bungalow inner join Alojamento on nome = nome_alojamento
				  inner join Parque p on nome_parque = p.nome 
go	

IF EXISTS (SELECT * FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[inserts]')
               AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[inserts];
END;
go

create trigger inserts on Bungallows instead of Insert
as
	begin tran
		begin 
			try
				insert into Alojamento([preço_base]  ,[descrição]  ,[localização] ,[nome] ,[nome_parque] ,[max_pessoas])
							select [preço_base]  ,[descrição]  ,[localização] ,[nome_alojamento] ,[nome_parque] ,[max_pessoas] from inserted
				
				insert into Bungalow([tipologia],[nome_alojamento])
							select [tipologia], [nome_alojamento] from inserted
			commit
		end try

		begin
			catch 
				rollback
		end catch
go



IF EXISTS (SELECT * FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[updates]')
               AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[updates];
END;
go


create trigger updates on Bungallows instead of update, delete
as
	begin tran
	SET NOCOUNT ON
		begin 
			try
				DECLARE @nomealojamento varchar(56)
				select @nomealojamento = nome_alojamento from deleted

				delete from dbo.Bungalow where nome_alojamento = @nomealojamento
				delete Alojamento where nome = @nomealojamento

				if exists(select(1)from inserted)
				begin
					insert into Alojamento([preço_base]  ,[descrição]  ,[localização] ,[nome] ,[nome_parque] ,[max_pessoas])
								select [preço_base]  ,[descrição]  ,[localização] ,[nome_alojamento] ,[nome_parque] ,[max_pessoas] from inserted
				
					insert into Bungalow([tipologia],[nome_alojamento])
								select [tipologia], [nome_alojamento] from inserted
				end
			commit

		end try

		begin
			catch 
				rollback
		end catch
go

---TESTES INSERT

begin tran

	begin 
		try
			insert into parque (nome,email ,morada ,estrelas)
						values( 'Parque Arrabida','arr@parqueArrabida.com','Arrabida',5)
			insert into bungallows (tipologia, nome_alojamento,preço_base ,descrição  ,localização ,nome_parque ,
	                    max_pessoas )
						values('T2','Rua torya',7999,'sem ventilaçao','Arrabida','Parque Arrabida',100)
			select *from Bungallows
			select *from Bungalow
			select * from Alojamento
		rollback
	end try
	begin catch
	rollback
	end catch
go
				
				
				
begin tran

	begin 
		try
			insert into parque (nome,email ,morada ,estrelas)
						values( 'Parque Arrabida','arr@parqueArrabida.com','Arrabida',5)
			insert into bungallows (tipologia, nome_alojamento,preço_base ,descrição  ,localização ,nome_parque ,
	                    max_pessoas )
						values('T2','Rua torya',7999,'sem ventilaçao','Arrabida','Parque Arrabida',100)
			select *from Bungallows
			select *from Bungalow
			select * from Alojamento

			update Bungallows set tipologia = 'T3', descrição =' lool'

			select *from Bungallows
			select *from Bungalow
			select * from Alojamento

		rollback
	end try
	begin catch
	rollback
	end catch
go			
