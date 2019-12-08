
 # TestAsync
 
 Script de criação do Banco de dados

    create database TestAsync
    use [TestAsync] 
    
    CREATE TABLE Logs (
        Id int  NOT NULL IDENTITY(1,1) PRIMARY KEY,
        Message varchar(100) NOT NULL,
        DataExec varchar(30),
        Async bit,
        Sequence varchar(100) NOT NULL 
    );
    
