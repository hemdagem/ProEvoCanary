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

SELECT R.HomeScore, R.AwayScore, HomeTeam = HU.Forename + ' ' + HU.Surname, AwayTeam = AU.Forename + ' ' + AU.Surname
FROM Results R JOIN Users HU ON HU.Id =R.HomeUserId JOIN Users AU ON AU.Id = R.AwayUserId  
WHERE R.TournamentId = @Id