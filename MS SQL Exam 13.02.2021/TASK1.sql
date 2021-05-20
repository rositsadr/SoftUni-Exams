CREATE DATABASE Bitbucket
GO

USE Bitbucket
GO

CREATE TABLE	Users 
(
	Id	INT NOT NULL IDENTITY PRIMARY KEY ,
	Username VARCHAR(30) NOT NULL,
	Password VARCHAR(30) NOT NULL,
	Email VARCHAR(50) NOT NULL,
)
CREATE TABLE	Repositories
(
	Id INT NOT NULL IDENTITY PRIMARY KEY ,
	Name VARCHAR(50) NOT NULL
)
CREATE TABLE	RepositoriesContributors 
(
	RepositoryId INT NOT NULL REFERENCES Repositories (Id),
	ContributorId INT NOT NULL REFERENCES Users (Id),
	CONSTRAINT PK_REPOSITORY_USER PRIMARY KEY (RepositoryId, ContributorId)
)
CREATE TABLE	Issues 
(
	Id	INT NOT NULL IDENTITY PRIMARY KEY ,
	Title	VARCHAR(255) NOT NULL,
	IssueStatus	VARCHAR(6) NOT NULL ,
	RepositoryId	INT NOT NULL REFERENCES Repositories (Id),
	AssigneeId	INT NOT NULL REFERENCES Users(Id)
)
--			Each issue has a repository.
--			Each issue has an assignee (user).


CREATE TABLE	Commits
(
	Id	INT NOT NULL IDENTITY PRIMARY KEY ,
	Message	VARCHAR(255) NOT NULL,
	IssueId	INT REFERENCES Issues (Id),
	RepositoryId	INT NOT NULL REFERENCES Repositories (Id),
	ContributorId	INT NOT NULL REFERENCES Users (Id)
)
--	Each commit MAY have an issue.
--	Each commit has a repository.
--	Each commit has a contributor (user).


CREATE TABLE	Files 
(
	Id INT NOT NULL IDENTITY PRIMARY KEY,
	Name VARCHAR(100) NOT NULL,
	Size DECIMAL(10,2) NOT NULL,
	ParentId INT REFERENCES Files(Id),
	CommitId INT NOT NULL REFERENCES Commits(Id)
)
--	Each file MAY have a parent (file).
--	Each file has a commit. 
