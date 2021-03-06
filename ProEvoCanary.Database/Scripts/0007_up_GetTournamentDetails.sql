IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_GetTournamentDetails')
BEGIN
    exec ('create procedure up_GetTournamentDetails as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_GetTournamentDetails]

AS
	SELECT	T.TournamentId,T.TournamentName, T.Date, ISNULL(U.Forename + ' ' + U.Surname,'No Owner') As [Name], T.Completed 
	FROM	TOURNAMENT T LEFT OUTER JOIN Users U ON U.UserId = T.OwnerID	/* SET NOCOUNT ON */
	WHERE	T.TournamentType = 1
	RETURN
