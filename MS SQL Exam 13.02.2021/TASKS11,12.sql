USE Bitbucket
GO

CREATE FUNCTION udf_AllUserCommits(@username VARCHAR (30)) 
RETURNS INT
AS
	BEGIN
		DECLARE @RESULT INT;

		SET @RESULT = (SELECT COUNT(*) FROM Users AS U
								JOIN Commits AS C ON C.ContributorId = U.Id
								WHERE U.Username = @username
								GROUP BY U.Id)
        IF @RESULT IS NULL
			SET @RESULT =0;

		RETURN @RESULT;
	END

GO

SELECT dbo.udf_AllUserCommits('BLABLA')

CREATE PROC usp_SearchForFiles(@fileExtension VARCHAR(10))
AS
	SELECT Id,Name, CAST(Size AS VARCHAR)+'KB' FROM Files 
				WHERE Name LIKE '%'+@fileExtension
				ORDER BY Id,Name,Size DESC
GO

EXEC usp_SearchForFiles 'txt'