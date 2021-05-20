USE Bitbucket
GO

--TASK5

SELECT Id,	Message,	RepositoryId,	ContributorId FROM Commits
  ORDER BY Id,Message,RepositoryId,ContributorId

---- TASK6

SELECT Id,	Name,	Size
	FROM Files
  WHERE Size> 1000 AND Name LIKE '%html%'
  ORDER BY Size DESC, Id, Name

----TASK7

SELECT I.Id, U.Username + ' : '+I.Title
	FROM Issues AS I
	JOIN Users AS U ON U.Id = I.AssigneeId
  ORDER BY I.Id DESC, I.AssigneeId

----TASK8

SELECT Id,Name,CAST (Size AS VARCHAR)+'KB' AS Size
	FROM Files
  WHERE Id NOT IN (SELECT ParentId FROM Files WHERE ParentId IS NOT NULL GROUP BY ParentId)
  ORDER BY Id,Name,Size DESC

----TASK9

SELECT R.Id, R.Name, COUNT(*) AS Commits
	FROM RepositoriesContributors AS RC
	JOIN Repositories AS R ON RC.RepositoryId = R.Id
	JOIN Commits AS C ON C.RepositoryId = R.Id
 GROUP BY R.Id, R.Name
 ORDER BY Commits DESC ,R.Id, R.Name



----TASK10

SELECT U.Username, AVG(F.Size) AS Size
	FROM Users AS U
	JOIN Commits AS C ON C.ContributorId = U.Id
	JOIN Files AS F ON F.CommitId = C.Id
  GROUP BY U.Id, U.Username
  ORDER BY Size DESC, U.Username
