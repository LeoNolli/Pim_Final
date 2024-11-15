DROP DATABASE IF EXISTS `fazenda`;
CREATE DATABASE IF NOT EXISTS `fazenda`;
USE `fazenda`;

-- Tabela Usuario
CREATE TABLE IF NOT EXISTS `Usuario` (
  `UsuarioId` INT NOT NULL AUTO_INCREMENT,
  `Email` VARCHAR(50),
  `Senha` VARCHAR(255),
  `DataCriacao` TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  `Funcao` VARCHAR(15),
  PRIMARY KEY (`UsuarioId`),
  CONSTRAINT UQ_Usuario_EmailLogin UNIQUE (`Email`)
);

-- Tabela Cliente
CREATE TABLE IF NOT EXISTS `Cliente` (
  `ClienteId` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(50),
  `CPF` VARCHAR(14),
  `RG` VARCHAR(20),
  `Endereco` VARCHAR(100),
  `Telefone` VARCHAR(20),
  `UsuarioId` INT,
  CONSTRAINT PK_Cliente PRIMARY KEY (`ClienteId`),
  CONSTRAINT UQ_Cliente_CPF UNIQUE (`CPF`),
  FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario`(`UsuarioId`) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Tabela Funcionario
CREATE TABLE IF NOT EXISTS `Funcionario` (
  `FuncionarioId` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(50),
  `CPF` VARCHAR(14),
  `RG` VARCHAR(20),
  `Endereco` VARCHAR(100),
  `Telefone` VARCHAR(20),
  `Cargo` VARCHAR(30),
  `UsuarioId` INT,
  CONSTRAINT PK_Funcionario PRIMARY KEY (`FuncionarioId`),
  CONSTRAINT UQ_Funcionario_CPF UNIQUE (`CPF`),
  FOREIGN KEY (`UsuarioId`) REFERENCES `Usuario`(`UsuarioId`) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Tabela Insumos
CREATE TABLE IF NOT EXISTS `Insumo` (
  `InsumoId` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(50),
  `Descricao` VARCHAR(100),
  `Quantidade` INT,
  `Preco` FLOAT,
  CONSTRAINT PK_Insumos PRIMARY KEY (`InsumoId`)
);
-- Tabela Produto
CREATE TABLE IF NOT EXISTS `Produto` (
  `ProdutoId` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(50),
  `Descricao` VARCHAR(300),
  `QuantidadeEstoque` INT,
  `Preco` FLOAT,
  CONSTRAINT PK_Produto PRIMARY KEY (`ProdutoId`)
);

-- Tabela ControleProducao
CREATE TABLE IF NOT EXISTS `ControleProducao` (
  `ProducaoId` INT NOT NULL AUTO_INCREMENT,
  `Quantidade` INT,
  `DataInicio` DATE,
  `DataConclusao` DATE,
  `Status` VARCHAR(50),
  `FuncionarioId` INT,
  `ProdutoId` INT,
  CONSTRAINT PK_ControleProducao PRIMARY KEY (`ProducaoId`),
  FOREIGN KEY (`FuncionarioId`) REFERENCES `Funcionario`(`FuncionarioId`) ON DELETE SET NULL ON UPDATE CASCADE,
  FOREIGN KEY (`ProdutoId`) REFERENCES `Produto`(`ProdutoId`) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Tabela InsumoProducao
CREATE TABLE IF NOT EXISTS `InsumoProducao` (
  `Quantidade` FLOAT,
  `ProducaoId` INT,
  `InsumoId` INT,
  CONSTRAINT PK_InsumoProducao PRIMARY KEY (`ProducaoId`, `InsumoId`),
  FOREIGN KEY (`ProducaoId`) REFERENCES `ControleProducao`(`ProducaoId`) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (`InsumoId`) REFERENCES `Insumo`(`InsumoId`) ON DELETE CASCADE ON UPDATE CASCADE
);



-- Tabela Venda
CREATE TABLE IF NOT EXISTS `Venda` (
  `VendaId` INT NOT NULL AUTO_INCREMENT,
  `DataHora` DATETIME,
  `ValorTotal` FLOAT,
  `ClienteId` INT,
  `FuncionarioId` INT,
  CONSTRAINT PK_Venda PRIMARY KEY (`VendaId`),
  FOREIGN KEY (`ClienteId`) REFERENCES `Cliente`(`ClienteId`) ON DELETE SET NULL ON UPDATE CASCADE,
  FOREIGN KEY (`FuncionarioId`) REFERENCES `Funcionario`(`FuncionarioId`) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Tabela ItemVenda
CREATE TABLE IF NOT EXISTS `ItemVenda` (
  `ProdutoId` INT,
  `VendaId` INT,
  `Quantidade` INT,
  `PrecoVenda` FLOAT,
  CONSTRAINT PK_ItemVenda PRIMARY KEY (`ProdutoId`, `VendaId`),
  FOREIGN KEY (`ProdutoId`) REFERENCES `Produto`(`ProdutoId`) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (`VendaId`) REFERENCES `Venda`(`VendaId`) ON DELETE CASCADE ON UPDATE CASCADE
);


INSERT INTO Usuario
(`Email`,
`Senha`,
`DataCriacao`,
`Funcao`)
VALUES
('Admin@Admin',
'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',
'2024-10-17 20:37:26',
'Admin');

INSERT INTO Usuario
(`Email`,
`Senha`,
`DataCriacao`,
`Funcao`)
VALUES
('Cliente01@email.com',
'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',
'2024-10-17 20:37:26',
'Cliente');

INSERT INTO Funcionario VALUES(
  1,
  'Adm',
  '1',
  '1',
  'Nenhum',
  '1111',
  'Administrador',
  1
);

INSERT INTO `Cliente` (Nome, CPF, RG, Endereco, Telefone, UsuarioId)
VALUES (
    'Cliente01',
    '111.111.111-11',
    '99-99.999.999',
    'Rua das Flores, 123, Bairro Central, Cidade Exemplo, MG',
    '(00) 0000-0000',
    2
);



INSERT INTO `Insumo` (`Nome`, `Descricao`, `Quantidade`, `Preco`) VALUES
('Sementes de Trigo', 'Sementes de trigo para plantio e cultivo de grãos', 100, 5.50),
('Fertilizante Orgânico', 'Fertilizante orgânico rico em nutrientes para cultivo', 200, 3.00),
('Sal Mineral para Plantas', 'Suplemento mineral para o solo, aumentando a fertilidade', 150, 1.20),
('Óleo de Neem', 'Óleo de neem usado para controle de pragas em plantações', 80, 4.50),
('Composto Microbiano', 'Microrganismos para melhoria e fertilização do solo', 50, 2.00),
('Sementes de Tomate', 'Sementes de tomate orgânico para plantio', 120, 8.00),
('Adubo de Esterco', 'Adubo de esterco natural para aumentar a produtividade do solo', 200, 6.00),
('Turfa', 'Turfa para reter umidade e nutrir o solo em plantações de hortaliças', 90, 10.00),
('Sementes de Milho', 'Sementes de milho selecionadas para plantio', 300, 12.00),
('Controle Biológico', 'Agentes de controle biológico para reduzir pragas naturalmente', 70, 15.00),
('Sementes de Alface', 'Sementes de alface orgânica para cultivo', 150, 4.00),
('Fertilizante Nitrogenado', 'Fertilizante rico em nitrogênio para vegetais', 100, 6.50),
('Fosfato de Rocha', 'Fonte natural de fósforo para solo', 200, 5.00),
('Sementes de Cenoura', 'Sementes de cenoura para cultivo', 180, 3.50),
('Farinha de Ossos', 'Adubo de farinha de ossos para melhorar o solo', 100, 7.50),
('Cinzas Vegetais', 'Cinzas usadas como suplemento de potássio para o solo', 90, 2.50),
('Areia Silicosa', 'Areia usada para melhorar drenagem do solo', 150, 1.50),
('Sementes de Rabanete', 'Sementes de rabanete para plantio rápido e cultivo', 160, 4.20),
('Enxofre em Pó', 'Suplemento de enxofre para fertilização de plantas', 120, 3.80),
('Sementes de Beterraba', 'Sementes de beterraba para cultivo orgânico', 130, 6.80),
('Alga Marinha', 'Extrato de alga para aumentar a vitalidade do solo', 75, 5.50),
('Sementes de Pepino', 'Sementes de pepino para plantio em hortas', 140, 4.70),
('Sulfato de Magnésio', 'Suplemento de magnésio para o solo', 200, 1.80),
('Sementes de Abóbora', 'Sementes de abóbora para plantio', 110, 5.30),
('Carvão Vegetal', 'Carvão em pó para melhorar a retenção de nutrientes', 60, 8.20),
('Sementes de Pimentão', 'Sementes de pimentão para cultivo', 130, 9.00),
('Estiércol', 'Adubo orgânico natural feito de esterco animal', 180, 4.00),
('Sementes de Berinjela', 'Sementes de berinjela para plantio', 100, 7.00),
('Argila Expandida', 'Substrato de argila para uso em jardinagem', 90, 5.50),
('Composto Orgânico', 'Composto orgânico para aumento de fertilidade do solo', 150, 3.30),
('Sementes de Couve', 'Sementes de couve orgânica para plantio', 140, 2.80),
('Inoculantes Biológicos', 'Bactérias benéficas para melhorar a saúde do solo', 60, 12.00),
('Sementes de Alho', 'Sementes de alho para plantio e cultivo', 80, 6.40),
('Turfa Vegetal', 'Turfa vegetal usada para substratos de mudas', 75, 10.50),
('Sementes de Espinafre', 'Sementes de espinafre orgânico para cultivo', 110, 3.80),
('Vermiculita', 'Vermiculita para melhorar a aeração do solo', 90, 4.40),
('Sementes de Rúcula', 'Sementes de rúcula para cultivo de hortas', 130, 2.90),
('Fertilizante Potássico', 'Fertilizante com alta concentração de potássio', 100, 5.80),
('Sementes de Quiabo', 'Sementes de quiabo para plantio', 120, 5.00),
('Borra de Café', 'Borra de café para enriquecimento orgânico do solo', 200, 1.00);


INSERT INTO `Produto` (`Nome`, `Descricao`, `QuantidadeEstoque`, `Preco`) VALUES
('Trigo', 'Trigo colhido e pronto para produção de grãos e farinha', 50, 7.00),
('Cacau', 'Sementes de cacau colhidas para processamento e fabricação de chocolate', 30, 15.00),
('Batata Doce', 'Batatas-doces colhidas, prontas para consumo ou replantio', 100, 2.50),
('Milho Verde', 'Milho verde colhido e pronto para consumo fresco', 20, 18.00),
('Tomate Orgânico', 'Tomates cultivados sem agrotóxicos, prontos para venda', 25, 22.00),
('Alface Crespa', 'Alface orgânica cultivada para consumo em saladas', 60, 4.00),
('Cenoura', 'Cenouras frescas e orgânicas colhidas para venda', 80, 3.50),
('Feijão', 'Feijões colhidos para consumo e plantio', 40, 5.00),
('Mandioca', 'Raízes de mandioca frescas para processamento e venda', 35, 6.50),
('Couve', 'Folhas de couve frescas e orgânicas, prontas para consumo', 50, 4.20),
('Pimenta', 'Pimenta fresca, ideal para temperos e conservas', 25, 8.00),
('Abobrinha', 'Abobrinhas frescas e orgânicas para consumo direto', 45, 5.50),
('Berinjela', 'Berinjelas frescas prontas para preparo em diversas receitas', 30, 6.00),
('Beterraba', 'Beterrabas orgânicas frescas para saladas e sucos', 70, 4.00),
('Chuchu', 'Chuchus frescos e prontos para consumo', 50, 3.80),
('Morango', 'Morangos orgânicos frescos e doces para consumo direto', 20, 12.00),
('Banana Prata', 'Bananas pratas frescas e maduras para consumo', 100, 3.00),
('Melão', 'Melões frescos e doces, prontos para venda', 15, 20.00),
('Abacaxi', 'Abacaxis maduros e prontos para venda e consumo', 25, 18.50),
('Uva', 'Uvas frescas e doces, ideais para consumo e sucos', 40, 13.00),
('Alho', 'Cabeças de alho frescas para tempero e consumo', 35, 10.00),
('Cebola Roxa', 'Cebolas roxas frescas e orgânicas para consumo', 80, 6.50),
('Cebola Branca', 'Cebolas brancas frescas e prontas para consumo', 90, 4.50),
('Espinafre', 'Folhas de espinafre frescas e orgânicas', 55, 5.00),
('Repolho', 'Repolho orgânico, ideal para saladas e cozidos', 40, 3.20),
('Açafrão', 'Raízes de açafrão fresco, ideal para tempero', 30, 15.00),
('Gengibre', 'Gengibre fresco para consumo e chás', 25, 9.00),
('Limão Siciliano', 'Limões sicilianos frescos para consumo e tempero', 50, 6.50),
('Laranja', 'Laranjas frescas e doces, prontas para consumo', 100, 4.00),
('Maçã Gala', 'Maçãs Gala frescas e suculentas para venda', 60, 8.00),
('Mamão', 'Mamão fresco e maduro, pronto para consumo', 30, 12.00),
('Maracujá', 'Maracujás frescos para consumo e produção de suco', 45, 7.50),
('Pepino', 'Pepinos frescos e crocantes, ideais para saladas', 80, 4.00),
('Pimentão Amarelo', 'Pimentões amarelos frescos para consumo', 35, 9.00),
('Pimentão Vermelho', 'Pimentões vermelhos frescos, ideais para diversas receitas', 25, 10.00),
('Rúcula', 'Folhas de rúcula frescas e orgânicas, ideais para saladas', 40, 3.00),
('Vagem', 'Vagens frescas, ideais para consumo direto', 60, 5.20),
('Quiabo', 'Quiabos frescos e prontos para consumo', 30, 4.80),
('Café em Grão', 'Grãos de café frescos, prontos para torra', 20, 22.00),
('Melancia', 'Melancias frescas e suculentas, prontas para consumo', 10, 25.00);



