IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_GetTournamentForEdit')
BEGIN
    exec ('create procedure up_GetTournamentForEdit as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_GetTournamentForEdit]
@Id int
AS

SELECT Id, OwnerId, TournamentName, Date, Completed,TournamentType
FROM Tournament
WHERE Id = @Id

SELECT * FROM Results WHERE TournamentId = @Id