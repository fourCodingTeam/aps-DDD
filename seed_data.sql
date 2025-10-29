-- BattleHub Seed Data: 16 Participants + 4 Tournaments
USE BattleHubDb;
GO

-- Insert 16 participants
INSERT INTO Participantes (Id, Nome, Email) VALUES
(NEWID(), 'Ana Silva', 'ana.silva@example.com'),
(NEWID(), 'Bruno Costa', 'bruno.costa@example.com'),
(NEWID(), 'Carlos Mendes', 'carlos.mendes@example.com'),
(NEWID(), 'Diana Rocha', 'diana.rocha@example.com'),
(NEWID(), 'Eduardo Alves', 'eduardo.alves@example.com'),
(NEWID(), 'Fernanda Lima', 'fernanda.lima@example.com'),
(NEWID(), 'Gabriel Santos', 'gabriel.santos@example.com'),
(NEWID(), 'Helena Martins', 'helena.martins@example.com'),
(NEWID(), 'Igor Pereira', 'igor.pereira@example.com'),
(NEWID(), 'Juliana Souza', 'juliana.souza@example.com'),
(NEWID(), 'Kleber Nunes', 'kleber.nunes@example.com'),
(NEWID(), 'Larissa Ramos', 'larissa.ramos@example.com'),
(NEWID(), 'Marcos Vieira', 'marcos.vieira@example.com'),
(NEWID(), 'Natalia Castro', 'natalia.castro@example.com'),
(NEWID(), 'Otavio Reis', 'otavio.reis@example.com'),
(NEWID(), 'Patricia Lopes', 'patricia.lopes@example.com');

-- Insert 4 tournaments
INSERT INTO Torneios (Id, Nome, TamanhoChave, DataInicio, DataFim, Estado) VALUES
(NEWID(), 'Campeonato Primavera 2025', 16, '2025-11-01', '2025-11-15', 0),
(NEWID(), 'Copa Verao Elite', 8, '2025-12-01', '2025-12-10', 0),
(NEWID(), 'Torneio Relampago', 4, '2025-11-20', '2025-11-22', 0),
(NEWID(), 'Liga Nacional de Batalha', 32, '2026-01-10', '2026-02-28', 0);

PRINT '16 participantes e 4 torneios inseridos com sucesso!';
