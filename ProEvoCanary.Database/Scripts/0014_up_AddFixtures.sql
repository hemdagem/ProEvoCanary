IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_AddFixtures')
BEGIN
    exec ('create procedure up_AddFixtures as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_AddFixtures]
	@XmlString xml
AS
INSERT INTO dbo.Results(TournamentId, HomeScore, AwayScore, Round, HomeUserId, AwayUserId)
   SELECT
	  TournamentId = XCol.value('(value)[1]','int'),
	  HomeScore = XCol.value('(value)[2]','int'),
	  AwayScore = XCol.value('(value)[3]','int'),	
      Round = XCol.value('(value)[4]','int'),
      HomeUserId = XCol.value('(value)[5]','int'),
      AwayUserId = XCol.value('(value)[6]','int')
   FROM 
      @XmlString.nodes('/dataset/data/row') AS XTbl(XCol)