CREATE TABLE [dbo].[CourseDemand]
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [ContactEmailAddress] VARCHAR(255) NOT NULL,
    [OrganisationName] VARCHAR(1000) NOT NULL,
    [NumberOfApprentices] INT NOT NULL DEFAULT 0,
    [CourseId] INT NOT NULL,
    [CourseTitle] VARCHAR(1000) NOT NULL,
    [CourseLevel] INT NOT NULL,
    [LocationName] VARCHAR(1000) NOT NULL,
    [Lat] FLOAT NOT NULL DEFAULT 0,
    [Long] FLOAT NOT NULL DEFAULT 0,
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(),
    [EmailVerified] BIT NOT NULL DEFAULT 0
)
GO