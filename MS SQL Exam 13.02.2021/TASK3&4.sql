USE Bitbucket
GO

UPDATE Issues
	SET IssueStatus = 'closed'
 WHERE AssigneeId = 6;

GO

DELETE FROM RepositoriesContributors
	WHERE RepositoryId = (SELECT Id FROM Repositories WHERE NAME = 'Softuni-Teamwork')

DELETE FROM Issues
	WHERE RepositoryId = (SELECT Id FROM Repositories WHERE NAME = 'Softuni-Teamwork')