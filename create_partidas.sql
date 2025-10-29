USE BattleHubDb;
GO

DECLARE @torneioId UNIQUEIDENTIFIER = '95F8FA59-F51B-42FD-B26E-870034F24855'; -- Copa Verao Elite

-- Create 4 matches (quarterfinals) for the 8 participants
INSERT INTO Partidas (Id, TorneioId, Rodada, ParticipanteAId, ParticipanteBId, Estado) VALUES
(NEWID(), @torneioId, 1, 'BBBBD79E-2F71-4952-B683-0094FA3DED7E', '21FAEB3F-7F6F-4BD3-9B31-0F990B3D566F', 0), -- Ana vs Carlos
(NEWID(), @torneioId, 1, '3464FA2A-6882-4B10-AFE4-31E65C47C5CF', '045C84A9-EA2E-4B8C-AA19-2CCA5054F74F', 0), -- Helena vs Igor
(NEWID(), @torneioId, 1, '970450FD-EBEA-4DC7-B82B-F1CDC19DDFE9', '40FCA558-4D21-4CB0-B6A3-6295C8BB7011', 0), -- Juliana vs Larissa
(NEWID(), @torneioId, 1, 'CBD327C7-DB2A-4E60-8D0D-9A000B23C43C', 'D9514B92-9DAE-4C4B-B2E8-122EB98BC743', 0); -- Natalia vs Otavio

-- Create matches for Torneio Relampago (4 participants)
DECLARE @torneio2 UNIQUEIDENTIFIER = 'FE01806F-1E3E-46ED-9DF5-949F3F19BF1B';

-- Get first 4 participants for this tournament
DECLARE @p1 UNIQUEIDENTIFIER, @p2 UNIQUEIDENTIFIER, @p3 UNIQUEIDENTIFIER, @p4 UNIQUEIDENTIFIER;

SELECT TOP 1 @p1 = ParticipanteId FROM Inscricoes WHERE TorneioId = @torneio2 ORDER BY CriadoEm;
SELECT TOP 1 @p2 = ParticipanteId FROM Inscricoes WHERE TorneioId = @torneio2 AND ParticipanteId != @p1 ORDER BY CriadoEm;
SELECT TOP 1 @p3 = ParticipanteId FROM Inscricoes WHERE TorneioId = @torneio2 AND ParticipanteId NOT IN (@p1, @p2) ORDER BY CriadoEm;
SELECT TOP 1 @p4 = ParticipanteId FROM Inscricoes WHERE TorneioId = @torneio2 AND ParticipanteId NOT IN (@p1, @p2, @p3) ORDER BY CriadoEm;

INSERT INTO Partidas (Id, TorneioId, Rodada, ParticipanteAId, ParticipanteBId, Estado) VALUES
(NEWID(), @torneio2, 1, @p1, @p2, 0),
(NEWID(), @torneio2, 1, @p3, @p4, 0);

PRINT 'Partidas criadas com sucesso!';
