IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_AddTournamentUsers')
BEGIN
    exec ('create procedure up_AddTournamentUsers as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_AddTournamentUsers]
	@XmlString xml
AS
INSERT INTO dbo.TournamentUsers(UserId, TournamentId, PreviousPosition)
   SELECT
	  UserId = XCol.value('(value)[1]','int'),
	  TournamentId = XCol.value('(value)[2]','int'),
	  PreviousPosition = XCol.value('(value)[3]','int')
   FROM 
      @XmlString.nodes('/dataset/data/row') AS XTbl(XCol)