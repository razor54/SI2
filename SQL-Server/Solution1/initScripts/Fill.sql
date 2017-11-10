SET XACT_ABORT ON
SET NOCOUNT ON
USE [SI2-Trabalho];
GO

/** INSERIR H�SPEDE **/
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


/** REMOVER H�SPEDE **/
create procedure removerHospede 
	@nif numeric
as
	if exists (select * from Hospede where nif = @nif)
		delete from Hospede where nif = @nif
	else
		raiserror
		(N'N�o existe nenhum h�spede com o nif pedido',
		10,
		1);
go

exec removerHospede N'123'
exec removerHospede N'5555'

select * from Hospede
go

/** ATUALIZAR H�SPEDE **/
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
		(N'N�o existe nenhum h�spede com o nif pedido',
		10,
		1);
go

exec atualizarHospede N'123', N'456', N'Jaquim', N'Praceta Sem Nome', N'jaquim@gmail.com'
exec atualizarHospede N'5555', N'456', N'Jaquim', N'Praceta Sem Nome', N'jaquim@gmail.com'

select * from Hospede
go

/** INSERIR ALOJAMENTO NUM PARQUE**/
-- falta remover alojamento e fazer update segundo as condi��es
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
	@pre�o_base numeric, @descri��o varchar(256),
	@localiza��o varchar(20), @nome_alojamento varchar(56),
	@max_pessoas numeric, @nome_parque varchar(56)
as
	if exists (select * from Parque where nome = @nome_parque)
		insert into Alojamento(nome_alojamento, nome_parque, pre�o_base, descri��o, localiza��o, max_pessoas)
			values(@nome_alojamento, @nome_parque, @pre�o_base, @descri��o, @localiza��o, @max_pessoas)
	else
		raiserror
		(N'N�o existe nenhum parque com o nome',
		10,
		1);
go

exec criarAlojamento N'125', N'Alojamento pequeno com bela vista', N'Quinta da Marinha', N'Primeiro Alojamento', N'3', N'Marechal Carmona'
exec criarAlojamento N'125', N'Alojamento pequeno com bela vista', N'Quinta da Marinha', N'Segundo Alojamento', N'3', N'Marechal Treta'

select * from Alojamento
go

create procedure criarEstada
	@id numeric, @data_inicio Date, @data_fim Date,
	@nome_alojamento varchar(56), @respons�vel varchar(128)
as
	if exists (select * from Alojamento where nome_alojamento = @nome_alojamento)
		insert into Estada(id, data_inicio, data_fim, nome_alojamento, respons�vel)
			values(@id, @data_inicio, @data_fim, @nome_alojamento, @respons�vel)
	else
		raiserror
		(N'N�o existe nenhum parque com o nome',
		10,
		1);
go

exec criarEstada N'111', N'01-01-2000', N'05-01-2000', N'Primeiro Alojamento', N'123'
exec criarEstada N'111', N'01-01-2000', N'05-01-2000', N'Segundo Alojamento', N'123'

select * from Estada
go

-- criar bungalow em transa��o com alojamento
-- o mesmo se passar� para as tendas
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
	@id numeric, @id_estada numeric, @descri�ao varchar(64),
	@pre�o_dia money, @tipo varchar(15)
as
	if exists (select * from Estada where id = @id_estada)
		insert into Extra(id, id_estada, descri�ao, pre�o_dia, tipo)
			values(@id, @id_estada, @descri�ao, @pre�o_dia, @tipo)
	else
		raiserror
		(N'N�o existe nenhuma estada com o id pedido',
		10,
		1);
go

exec criarExtraAlojamento N'1', N'111', N'Pessoa Extra', N'20', N'Alojamento'
exec criarExtraAlojamento N'1', N'22222', N'Pessoa Extra', N'20', N'Alojamento'

select * from Extra where tipo = 'Alojamento'
go

/** INSERIR EXTRA H�SPEDE **/
create procedure criarExtraHospede
	@id numeric, @id_estada numeric, @descri�ao varchar(64),
	@pre�o_dia money, @tipo varchar(15)
as
	if exists (select * from Estada where id = @id_estada)
		insert into Extra(id, id_estada, descri�ao, pre�o_dia, tipo)
			values(@id, @id_estada, @descri�ao, @pre�o_dia, @tipo)
	else
		raiserror
		(N'N�o existe nenhuma estada com o id pedido',
		10,
		1);
go

exec criarExtraAlojamento N'2', N'111', N'Pequeno Almo�o', N'5', N'H�spede'
exec criarExtraAlojamento N'2', N'22222', N'Pequeno Almo�o', N'5', N'H�spede'

select * from Extra where tipo = 'H�spede'
go

/** INSERIR ATIVIDADE **/
create procedure criarAtividade
	@data_atividade Date, @pre�o money, @lota�ao numeric,
	@nome_atividade varchar(56), @nome_parque varchar(56),
	@descri��o varchar(526)
as
	insert into Atividade(nome_atividade, nome_parque, data_atividade, pre�o, lota�ao, descri��o)
		values(@nome_atividade, @nome_parque, @data_atividade, @pre�o, @lota�ao, @descri��o)
go

exec criarAtividade N'02-01-2000', N'20', N'10', N'Canoagem', N'Marechal Carmona', N'Pequeno circuito de canoagem'

select * from Atividade
go