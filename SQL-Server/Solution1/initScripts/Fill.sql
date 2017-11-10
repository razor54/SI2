SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

/** INSERIR HÓSPEDE **/
create procedure criarHospede 
	@nif numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64)
as 
	insert into Hospede(nif, bi, nome, morada, email)
		values(@nif, @bi, @nome, @morada, @email)
go

exec criarHospede N'123', N'456', N'Jaquim', 
	N'Rua Sem Nome', N'jaquim@gmail.com'

select * from Hospede
go


/** REMOVER HÓSPEDE **/
create procedure removerHospede 
	@nif numeric
as
	if exists (select * from Hospede where nif = @nif)
		delete from Hospede where nif = @nif
	else
		raiserror
		(N'Não existe nenhum hóspede com o nif pedido',
		10,
		1);
go

exec removerHospede N'123'
exec removerHospede N'5555'

select * from Hospede
go

/** ATUALIZAR HÓSPEDE **/
create procedure atualizarHospede 
	@nif numeric, @bi numeric, @nome varchar(128),
	@morada varchar(128), @email varchar(64)
as 
	if exists (select * from Hospede where nif = @nif)
		update Hospede
		set bi = @bi, nome = @nome, morada = @morada, 
			email = @email
		where nif = @nif
	else
		raiserror
		(N'Não existe nenhum hóspede com o nif pedido',
		10,
		1);
go

exec atualizarHospede N'123', N'456', N'Jaquim', N'Praceta Sem Nome', N'jaquim@gmail.com'
exec atualizarHospede N'5555', N'456', N'Jaquim', N'Praceta Sem Nome', N'jaquim@gmail.com'

select * from Hospede
go

/** INSERIR ALOJAMENTO NUM PARQUE**/
-- falta remover alojamento e fazer update segundo as condições
create procedure criarParque
	@email varchar(50),
	@nome varchar(56),
	@morada varchar(256),
	@estrelas numeric
as
	insert into Parque(nome, email, morada, estrelas)
		values(@nome, @email, @morada, @estrelas)
go

exec criarParque N'mcarmona@gmail.com', N'Marechal Carmona', N'Rua de Cascais', N'4'

select * from Parque
go


create procedure criarAlojamento
	@preço_base numeric, @descrição varchar(256),
	@localização varchar(20), @nome_alojamento varchar(56),
	@max_pessoas numeric, @nome_parque varchar(56)
as
	if exists (select * from Parque where nome = @nome_parque)
		insert into Alojamento(nome_alojamento, nome_parque, preço_base, descrição, localização, max_pessoas)
			values(@nome_alojamento, @nome_parque, @preço_base, @descrição, @localização, @max_pessoas)
	else
		raiserror
		(N'Não existe nenhum parque com o nome',
		10,
		1);
go

exec criarAlojamento N'125', N'Alojamento pequeno com bela vista', N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona'
exec criarAlojamento N'125', N'Alojamento pequeno com bela vista', N'Quinta da Marinha', N'Segundo Alojamento', N'3', N'Marechal Treta'

select * from Alojamento
go

create procedure criarEstada
	@id numeric, @data_inicio Date, @data_fim Date,
	@nome_alojamento varchar(56), @responsável varchar(128)
as
	if exists (select * from Alojamento where nome_alojamento = @nome_alojamento)
		insert into Estada(id, data_inicio, data_fim, nome_alojamento, responsável)
			values(@id, @data_inicio, @data_fim, @nome_alojamento, @responsável)
	else
		raiserror
		(N'Não existe nenhum parque com o nome',
		10,
		1);
go

exec criarEstada N'111', N'01-01-2000', N'05-01-2000', N'Primeiro Alojamento', N'123'
exec criarEstada N'111', N'01-01-2000', N'05-01-2000', N'Segundo Alojamento', N'123'

select * from Estada
go

-- criar bungalow em transação com alojamento
-- o mesmo se passará para as tendas
create procedure criarBungalow
	@tipologia varchar(256),
	@nome varchar(56)
as
	insert into Bungalow(tipologia, nome)
		values(@tipologia, @nome)
go

exec criarBungalow N'T0', N'Primeiro Alojamento'

select * from Bungalow
go

--falta criar tenda

/** INSERIR EXTRA DE ALOJAMENTO **/
create procedure criarExtraAlojamento
	@id numeric, @id_estada numeric, @descriçao varchar(64),
	@preço_dia money, @tipo varchar(15)
as
	if exists (select * from Estada where id = @id_estada)
		insert into Extra(id, id_estada, descriçao, preço_dia, tipo)
			values(@id, @id_estada, @descriçao, @preço_dia, @tipo)
	else
		raiserror
		(N'Não existe nenhuma estada com o id pedido',
		10,
		1);
go

exec criarExtraAlojamento N'1', N'111', N'Pessoa Extra', N'20', N'Alojamento'
exec criarExtraAlojamento N'1', N'22222', N'Pessoa Extra', N'20', N'Alojamento'

select * from Extra where tipo = 'Alojamento'
go

/** INSERIR EXTRA HÓSPEDE **/
create procedure criarExtraHospede
	@id numeric, @id_estada numeric, @descriçao varchar(64),
	@preço_dia money, @tipo varchar(15)
as
	if exists (select * from Estada where id = @id_estada)
		insert into Extra(id, id_estada, descriçao, preço_dia, tipo)
			values(@id, @id_estada, @descriçao, @preço_dia, @tipo)
	else
		raiserror
		(N'Não existe nenhuma estada com o id pedido',
		10,
		1);
go

exec criarExtraAlojamento N'2', N'111', N'Pequeno Almoço', N'5', N'Hóspede'
exec criarExtraAlojamento N'2', N'22222', N'Pequeno Almoço', N'5', N'Hóspede'

select * from Extra where tipo = 'Hóspede'
go

/** INSERIR ATIVIDADE **/
create procedure criarAtividade
	@data_atividade Date, @preço money, @lotaçao numeric,
	@nome_atividade varchar(56), @nome_parque varchar(56),
	@descrição varchar(526)
as
	insert into Atividade(nome_atividade, nome_parque, data_atividade, preço, lotaçao, descrição)
		values(@nome_atividade, @nome_parque, @data_atividade, @preço, @lotaçao, @descrição)
go

exec criarAtividade N'02-01-2000', N'20', N'10', N'Canoagem', N'Marechal Carmona', N'Pequeno circuito de canoagem'

select * from Atividade
go