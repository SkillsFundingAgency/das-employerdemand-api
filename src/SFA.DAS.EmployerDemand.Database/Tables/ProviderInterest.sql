﻿CREATE TABLE [dbo].[ProviderInterest]
(
    [Id] UNIQUEIDENTIFIER PRIMARY KEY,
    [EmployerDemandId] UNIQUEIDENTIFIER NOT NULL,
    [Ukprn] INT NOT NULL,
    [Email] VARCHAR(256) NULL,
    [Phone] VARCHAR(50) NULL,
    [Website] VARCHAR(500) NULL,
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE()
)
GO