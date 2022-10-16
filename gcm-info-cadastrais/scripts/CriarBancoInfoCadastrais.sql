CREATE DATABASE db_info_cadastrais
GO
USE db_info_cadastrais
GO

CREATE TABLE CategoriaProduto (id UNIQUEIDENTIFIER NOT NULL,
						codigo INT,						
						nome NVARCHAR(200),
						descricao NVARCHAR(max),						
						datacriacao DATETIME,
						dataatualizacao DATETIME, 
						PRIMARY KEY (id))
						
CREATE TABLE Produto (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo INT, 
						 nome NVARCHAR(200), 
						 quantidade INT, 
						 valor DECIMAL(18,2), 
						 categoriaprodutoid UNIQUEIDENTIFIER,
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id),
						 FOREIGN KEY (categoriaprodutoid) REFERENCES CategoriaProduto(id))					
					
CREATE TABLE Cliente (id UNIQUEIDENTIFIER NOT NULL,
					  cpf NVARCHAR(20),
					  nome NVARCHAR(200),
					  codigocampanha int,
					  aniversario DATETIME,
					  cep NVARCHAR(10),
					  logradouro NVARCHAR(250),
					  numero INT,
					  complemento NVARCHAR(200),
					  cidade NVARCHAR(200),
					  estado NVARCHAR(50),
					  datacriacao DATETIME,
					  dataatualizacao DATETIME, 
					  PRIMARY KEY (id))