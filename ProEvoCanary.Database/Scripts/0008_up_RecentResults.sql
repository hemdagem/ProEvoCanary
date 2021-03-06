IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_RecentResults')
BEGIN
    exec ('create procedure up_RecentResults as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_RecentResults]


AS
SELECT DISTINCT TOP 7      R.ResultId, UHome.UserId AS HomeTeamID, UAway.UserId AS AwayTeamID,  
UHome.Forename + ' ' + UHome.Surname   AS HomeTeam,ISNULL(HomeScore,-1) AS HomeScore,ISNULL(AwayScore,-1) 
AS AwayScore,UAway.Forename + ' ' + UAway.Surname AS AwayTeam
FROM         
                      Results R INNER JOIN
                      Users UAway ON
                      UAway.UserId = R.AwayUserID INNER JOIN
                      Users UHome ON R.HomeUserID = UHome.UserId 
WHERE R.HomeScore >-1 and R.AwayScore >-1
Order by R.ResultId DESC          
	
RETURN
