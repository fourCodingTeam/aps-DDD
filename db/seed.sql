SET NOCOUNT ON;

IF DB_ID('BattleHubDb') IS NULL
BEGIN
    PRINT 'Criando banco BattleHubDb...';
    EXEC('CREATE DATABASE BattleHubDb');
END
GO
USE BattleHubDb;
GO

DECLARE @maxTries INT = 60;
WHILE @maxTries > 0
BEGIN
    IF EXISTS (SELECT 1 FROM sys.objects WHERE name = 'Participantes' AND type = 'U')
       AND EXISTS (SELECT 1 FROM sys.objects WHERE name = 'Torneios' AND type = 'U')
       AND EXISTS (SELECT 1 FROM sys.objects WHERE name = 'Inscricoes' AND type = 'U')
    BEGIN
        BREAK;
    END

    PRINT 'Tabelas ainda não existem. Aguardando migrations...';
    WAITFOR DELAY '00:00:05';
    SET @maxTries -= 1;
END

IF @maxTries = 0
BEGIN
    PRINT 'Timeout esperando tabelas. Encerrando seed sem inserir.';
    RETURN;
END

PRINT 'Tabelas prontas. Iniciando seed...';

IF NOT EXISTS (SELECT 1 FROM Participantes)
BEGIN
    INSERT INTO Participantes (Id, Nome, Email) VALUES
    (NEWID(), 'Alice Santos',    'alice@example.com'),
    (NEWID(), 'Bruno Lima',      'bruno@example.com'),
    (NEWID(), 'Carla Souza',     'carla@example.com'),
    (NEWID(), 'Diego Rocha',     'diego@example.com'),
    (NEWID(), 'Eduarda Melo',    'eduarda@example.com'),
    (NEWID(), 'Felipe Andrade',  'felipe@example.com'),
    (NEWID(), 'Gabriela Castro', 'gabriela@example.com'),
    (NEWID(), 'Henrique Nunes',  'henrique@example.com'),
    (NEWID(), 'Isabela Ramos',   'isabela@example.com'),
    (NEWID(), 'João Pedro',      'joaopedro@example.com'),
    (NEWID(), 'Karen Almeida',   'karen@example.com'),
    (NEWID(), 'Lucas Vieira',    'lucas@example.com'),
    (NEWID(), 'Mariana Lopes',   'mariana@example.com'),
    (NEWID(), 'Nicolas Reis',    'nicolas@example.com'),
    (NEWID(), 'Olívia Teixeira', 'olivia@example.com'),
    (NEWID(), 'Paulo Silva',     'paulo@example.com');
END
ELSE
BEGIN
    PRINT 'Participantes já existem. Pulando inserção.';
END

DECLARE @tid1 UNIQUEIDENTIFIER = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa';
DECLARE @tid2 UNIQUEIDENTIFIER = 'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb';

IF NOT EXISTS (SELECT 1 FROM Torneios WHERE Id = @tid1)
BEGIN
    INSERT INTO Torneios (Id, Nome, TamanhoChave, DataInicio, DataFim, Estado)
    VALUES
    (@tid1, 'Torneio Alpha', 8, GETUTCDATE(), DATEADD(DAY, 7, GETUTCDATE()), 0);
END

IF NOT EXISTS (SELECT 1 FROM Torneios WHERE Id = @tid2)
BEGIN
    INSERT INTO Torneios (Id, Nome, TamanhoChave, DataInicio, DataFim, Estado)
    VALUES
    (@tid2, 'Torneio Beta', 8, DATEADD(DAY, 10, GETUTCDATE()), DATEADD(DAY, 17, GETUTCDATE()), 0);
END

;WITH P AS (
    SELECT Id,
           ROW_NUMBER() OVER (ORDER BY Nome, Id) AS rn
    FROM Participantes
)
INSERT INTO Inscricoes (Id, TorneioId, ParticipanteId, CriadoEm)
SELECT NEWID(),
       CASE WHEN rn <= 8 THEN @tid1 ELSE @tid2 END,
       Id,
       GETUTCDATE()
FROM P
WHERE rn <= 16
  AND NOT EXISTS (
        SELECT 1
        FROM Inscricoes i
        WHERE i.ParticipanteId = P.Id
    );

PRINT 'Seed concluído.';
