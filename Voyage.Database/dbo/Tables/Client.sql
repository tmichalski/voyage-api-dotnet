﻿CREATE TABLE [dbo].[Client]
(
	[Id] nvarchar(128) PRIMARY KEY NOT NULL,
	[Name] nvarchar(500) NOT NULL, 
	[ClientIdentifier] nvarchar(500) NOT NULL, 
    [ClientSecret] nvarchar(1000) NOT NULL, 
	[RedirectUri] nvarchar(500) NOT NULL, 
	[IsSecretRequired] bit NOT NULL, 
	[IsScoped] bit NOT NULL, 
	[IsAutoApprove] bit NOT NULL, 
	[AccessTokenValiditySeconds] int NOT NULL,
	[RefreshTokenValiditySeconds] int NOT NULL,
	[FailedLoginAttempts] int NULL,
	[ForceTokenExpiredate] DateTime NULL,
	[IsEnabled] bit NOT NULL, 
	[IsAccountLocked] bit NOT NULL, 
	[CreatedBy] nvarchar(255) NOT NULL,
	[CreatedDate] datetime NOT NULL,
	[LastModifiedBy] nvarchar(255) NOT NULL,
	[LastModifiedDate] datetime NOT NULL, 
    [IsDeleted] BIT NOT NULL 
)
