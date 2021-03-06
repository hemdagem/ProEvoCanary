IF NOT EXISTS(SELECT 1 FROM sys.views  WHERE Name = 'up_HeadToHeadRecord')
BEGIN
    exec ('create procedure up_HeadToHeadRecord as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_HeadToHeadRecord]
@UserOneID int,
@UserTwoID int

AS


SELECT  ISNULL(COUNT(U.ResultID),0) AS TotalMatches, ISNULL(SUM(CASE WHEN PlayerOneScore = PlayerTwoScore THEN 1 ELSE 0 END),0) AS TotalDraws,
ISNULL(SUM(CASE WHEN U.PlayerOneID = @UserOneID THEN CASE WHEN U.PlayerOneScore > U.PlayerTwoScore THEN 1 ELSE 0 END ELSE CASE WHEN U.PlayerTwoID=@UserOneID THEN CASE WHEN U.PlayerTwoScore > U.PlayerOneScore THEN 1 ELSE 0 END END END),0) AS PlayerOneWins,
ISNULL(SUM(CASE WHEN U.PlayerOneID = @UserTwoID THEN CASE WHEN U.PlayerOneScore > U.PlayerTwoScore THEN 1 ELSE 0 END ELSE CASE WHEN U.PlayerTwoID=@UserTwoID THEN CASE WHEN U.PlayerTwoScore > U.PlayerOneScore THEN 1 ELSE 0 END END END),0) AS PlayerTwoWins



FROM

(
SELECT DISTINCT			R.ResultId AS ResultId,UPlayerOne.UserId AS PlayerOneID, UPlayerOne.Forename + ' ' + UPlayerOne.Surname AS PlayerOne, ISNULL(R.HomeScore, - 1) AS PlayerOneScore, 
						ISNULL(R.AwayScore, - 1) AS PlayerTwoScore,UPlayerTwo.UserId AS PlayerTwoID, UPlayerTwo.Forename + ' ' + UPlayerTwo.Surname  AS PlayerTwo, 
						R.HomeUserId AS PlayerOneTeamID,R.AwayUserId AS PlayerTwoTeamID
FROM					dbo.Results R INNER JOIN					
						dbo.Users UPlayerTwo ON R.AwayUserId = UPlayerTwo.UserId INNER JOIN
						
						dbo.Users UPlayerOne ON R.HomeUserId = UPlayerOne.UserId 
WHERE					(UPlayerOne.UserId=@UserOneID AND UPlayerTwo.UserId=@UserTwoID AND AwayScore !=-1 AND HomeScore !=-1 )
UNION ALL

SELECT DISTINCT 
						R.ResultId AS ResultId,UPlayerOne.UserId AS PlayerOneID, UPlayerOne.Forename + ' ' + UPlayerOne.Surname AS PlayerOne, ISNULL(R.HomeScore, - 1) AS PlayerOneScore, 
						ISNULL(R.AwayScore, - 1) AS PlayerTwoScore,UPlayerTwo.UserId AS PlayerTwoID, UPlayerTwo.Forename + ' ' + UPlayerTwo.Surname AS PlayerTwo, 
						R.HomeUserId AS PlayerOneTeamID, R.AwayUserId AS PlayerTwoTeamID
FROM					dbo.Results R INNER JOIN					
						dbo.Users UPlayerTwo ON R.AwayUserId = UPlayerTwo.UserId INNER JOIN
						
						dbo.Users UPlayerOne ON R.HomeUserId = UPlayerOne.UserId  
WHERE					(UPlayerOne.UserId=@UserTwoID AND UPlayerTwo.UserId=@UserOneID  AND AwayScore !=-1 AND HomeScore !=-1  )
                      
                      ) AS U
                      
                      
SELECT DISTINCT			UHome.Forename + ' ' + UHome.Surname  AS HomeUser, 
						ISNULL(R.HomeScore,-1) AS HomeScore, ISNULL(R.AwayScore,-1) AS AwayScore, 
						UAway.Forename + ' ' + UAway.Surname  AS AwayUser, R.ResultId
FROM					dbo.Results AS R INNER JOIN
						dbo.Users AS UAway ON R.AwayUserId = UAway.UserId
						INNER JOIN
						dbo.Users AS UHome ON R.HomeUserId = UHome.UserId

  
                    
WHERE ((UHome.UserId=@UserOneID AND UAway.UserId=@UserTwoID) OR (UHome.UserId=@UserTwoID AND UAway.UserId=@UserOneID)) AND 
	(ISNULL(R.HomeScore,-1) <> -1 OR ISNULL(R.AwayScore,-1) <> -1)
	/* SET NOCOUNT ON */
	RETURN

