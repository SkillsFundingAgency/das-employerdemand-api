﻿CREATE TABLE [dbo].[ProviderInterest]
(
    [Id]  UNIQUEIDENTIFIER NOT NULL,
    [EmployerDemandId] UNIQUEIDENTIFIER NOT NULL,
    [Ukprn] INT NOT NULL,
    [Email] VARCHAR(256) NOT NULL,
    [Phone] VARCHAR(50) NOT NULL,
    [Website] VARCHAR(500) NULL,
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT PK_ProviderInterest PRIMARY KEY CLUSTERED([Id], [EmployerDemandId], [Ukprn])
)
GO