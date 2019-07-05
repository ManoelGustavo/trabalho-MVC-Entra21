DROP TABLE IF EXISTS contabilidades;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS clientes;
DROP TABLE IF EXISTS cartoes_credito;
DROP TABLE IF EXISTS compras;
DROP TABLE IF EXISTS categorias;
DROP TABLE IF EXISTS contas_pagar;
DROP TABLE IF EXISTS contas_receber;


CREATE TABLE contabilidades(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45) NOT NULL
);

CREATE TABLE usuarios(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_contabilidade INT NOT NULL,
	FOREIGN KEY(id_contabilidade) REFERENCES contabilidades(id),
	login VARCHAR(45) NOT NULL,
	senha VARCHAR(45) NOT NULL,
	data_nascimento DATETIME2 NOT NULL
);

CREATE TABLE clientes(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_contabilidade INT NOT NULL,
	FOREIGN KEY(id_contabilidade) REFERENCES contabilidades(id),
	nome VARCHAR(45) NOT NULL,
	cpf VARCHAR(14) NOT NULL
);

CREATE TABLE cartoes_credito(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT NOT NULL,
	FOREIGN KEY(id_cliente) REFERENCES clientes(id),
	numero VARCHAR(45) NOT NULL,
	data_vencimento DATETIME2 NOT NULL,
	cvv VARCHAR(45) NOT NULL
);

CREATE TABLE compras(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cartao_credito INT NOT NULL,
	FOREIGN KEY(id_cartao_credito) REFERENCES cartoes_credito(id),
	valor DECIMAL(8,2) NOT NULL,
	data_compra DATETIME2 NOT NULL
);

CREATE TABLE categorias(
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(45) NOT NULL
);

CREATE TABLE contas_pagar(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT NOT NULL,
	FOREIGN KEY(id_cliente) REFERENCES clientes(id),
	id_categoria INT NOT NULL,
	FOREIGN KEY(id_categoria) REFERENCES categorias(id),
	nome VARCHAR(45) NOT NULL,
	data_vencimento DATETIME2 NOT NULL,
	data_pagamento DATETIME2 NOT NULL,
	valor DECIMAL(8,2) NOT NULL
);

CREATE TABLE contas_receber(
	id INT PRIMARY KEY IDENTITY(1,1),
	id_cliente INT NOT NULL,
	FOREIGN KEY(id_cliente) REFERENCES clientes(id),
	id_categoria INT NOT NULL,
	FOREIGN KEY(id_categoria) REFERENCES categorias(id),
	nome VARCHAR(45) NOT NULL,
	data_pagamento DATETIME NOT NULL,
	valor DECIMAL(8,2) NOT NULL
);
SELECT * FROM contabilidades