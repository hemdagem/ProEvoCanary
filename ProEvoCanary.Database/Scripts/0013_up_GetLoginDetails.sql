IF NOT EXISTS(SELECT 1 FROM sys.procedures  WHERE Name = 'up_GetLoginDetails')
BEGIN
    exec ('create procedure up_GetLoginDetails as select 1')
END

GO
ALTER PROCEDURE [dbo].[up_GetLoginDetails]
	@Username varchar(50)
AS
SELECT U.Forename,U.Surname, U.Email, U.UserId, U.UserType, U.Username, U.Hash
FROM Users U
WHERE U.Username=@Username