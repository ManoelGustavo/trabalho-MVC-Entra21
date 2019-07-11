INSERT INTO contabilidades(nome) VALUES('Joao'), ('Jorde'), ('Alex'), ('Alexandre'), ('Rose');

INSERT INTO usuarios(id_contabilidade, login, senha, data_nascimento) VALUES
(5, 'Jesse', 'jesse123', '2000-07-12'),
(4, 'Isaac', 'isaac321', '2000-05-20'),
(3, 'Lucas', 'lucas222', '1980-07-19'),
(2, 'Leonardo', 'leonardo666', '1970-06-16'),
(1, 'Maria', 'maria098', '1890-02-12');

INSERT INTO clientes(id_contabilidade, nome, cpf) VALUES
(1, 'Marlon', '101.968.512-33'),
(2, 'Sabrina', '202.364.677-44'),
(3, 'Aline', '222.111.565-12'),
(4, 'Jessica', '666.626.616-16'),
(5, 'Paulo', '514.675.432-75');

INSERT INTO cartoes_credito(id_cliente, numero, data_vencimento, cvv) VALUES
(1, '1111 2222 3332 4444', '2020-05-22', '231'),
(2, '9999 8888 7777 6666', '2019-08-11', '666'),
(3, '6543 1234 6574 0897', '2021-12-09', '675'),
(4, '9384 4456 7564 8769', '2023-06-16', '775'),
(5, '3033 3829 0057 2386', '2033-03-13', '336');

INSERT INTO compras(id_cartao_credito, valor, data_compra) VALUES
(3, 1200.39, '2010-02-12'),
(2, 2000.99, '2012-12-12'),
(1, 2500.00, '2015-09-30'),
(4, 5123.99, '2020-05-14'),
(5, 9650.59, '2018-07-16');

INSERT INTO categorias(nome) VALUES('Casa'), ('Carro'), ('Bike'), ('Cachorro'), ('Gato');

INSERT INTO contas_pagar(id_cliente, id_categoria, nome, data_vencimento, data_pagamento, valor) VALUES
(5, 1, 'Jonathan', '2019-12-20', '2019-05-22', 2500.00),
(4, 2, 'Rosane', '2018-06-17', '2018-06-01', 3000.02),
(3, 3, 'Renato', '2020-03-12', '2019-11-30', 4599.99),
(2, 4, 'Gustavo', '2021-05-21', '2020-07-31', 3999.99),
(1, 5, 'Andre', '2016-09-29', '2016-09-28', 6500.89);

INSERT INTO contas_receber(id_cliente, id_categoria, nome, data_pagamento, valor) VALUES
(2, 2, 'Francisco', '2015-04-17', 9000.00),
(1, 1, 'Renan', '2017-07-18', 7599.99),
(4, 4, 'Samara', '2014-01-01', 6543.75),
(5, 5, 'Eduardo', '2013-03-02', 1000.00),
(3, 3, 'Gabriel', '2009-08-25', 2060.06);