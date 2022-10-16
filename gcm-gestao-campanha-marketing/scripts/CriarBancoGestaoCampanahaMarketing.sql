CREATE DATABASE db_gestao_campanha_marketing
GO
USE db_gestao_campanha_marketing
GO
						 
CREATE TABLE Audiencia (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo INT, 
						 audiencia NVARCHAR(max),	
						 clientes NVARCHAR(max),
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id))						 
						 
CREATE TABLE Campanha (id UNIQUEIDENTIFIER NOT NULL, 
                         codigo INT, 						 
						 descricao NVARCHAR(max),
						 ativa BOOLEAN, 
						 audienciaid UNIQUEIDENTIFIER,
						 datacriacao DATETIME, 
						 dataatualizacao DATETIME, 
						 PRIMARY KEY (id),
						 FOREIGN KEY (audienciaid) REFERENCES Audiencia(id)))