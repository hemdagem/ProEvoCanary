IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_AddResult')
BEGIN
    exec ('create procedure up_AddResult as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_AddResult]
@ResultId int,
@HomeScore int,
@AwayScore int

AS


UPDATE Results SET HomeScore = @HomeScore, AwayScore= @AwayScore
WHERE ResultId=@ResultId
