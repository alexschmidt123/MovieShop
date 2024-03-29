﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [Cast] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(128) NULL,
        [Gender] nvarchar(max) NULL,
        [ProfilePath] nvarchar(2084) NULL,
        [TmdbUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_Cast] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [Genre] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(64) NOT NULL,
        CONSTRAINT [PK_Genre] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [Movie] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(256) NULL,
        [Overview] nvarchar(max) NULL,
        [Tagline] nvarchar(512) NULL,
        [Budget] decimal(18,4) NULL DEFAULT 9.9,
        [Revenue] decimal(18,4) NULL DEFAULT 9.9,
        [ImdbUrl] nvarchar(2084) NULL,
        [TmdbUrl] nvarchar(2084) NULL,
        [PosterUrl] nvarchar(2084) NULL,
        [BackdropUrl] nvarchar(2084) NULL,
        [OriginalLanguage] nvarchar(64) NULL,
        [ReleaseDate] datetime2 NULL,
        [RunTime] int NULL,
        [Price] decimal(5,2) NULL DEFAULT 9.9,
        [CreatedDate] datetime2 NULL DEFAULT (getdate()),
        [UpdatedDate] datetime2 NULL DEFAULT (getdate()),
        [UpdatedBy] nvarchar(max) NULL,
        [CreatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [Role] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(20) NULL,
        CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [User] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(128) NULL,
        [LastName] nvarchar(128) NULL,
        [DateOfBirth] datetime2 NULL DEFAULT (getdate()),
        [Email] nvarchar(256) NULL,
        [HashedPassword] nvarchar(1024) NULL,
        [Salt] nvarchar(1024) NULL,
        [PhoneNumber] nvarchar(16) NULL,
        [TwoFactorEnabled] bit NULL,
        [LockoutEndDate] datetime2 NULL DEFAULT (getdate()),
        [LastLoginDateTime] datetime2 NULL DEFAULT (getdate()),
        [IsLocked] bit NULL,
        [AccessFailedCount] int NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [MovieCast] (
        [MovieId] int NOT NULL,
        [CastId] int NOT NULL,
        [Character] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_MovieCast] PRIMARY KEY ([MovieId], [CastId]),
        CONSTRAINT [FK_MovieCast_Cast_CastId] FOREIGN KEY ([CastId]) REFERENCES [Cast] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieCast_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [MovieGenre] (
        [MovieId] int NOT NULL,
        [GenreId] int NOT NULL,
        CONSTRAINT [PK_MovieGenre] PRIMARY KEY ([MovieId], [GenreId]),
        CONSTRAINT [FK_MovieGenre_Genre_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieGenre_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [Trailer] (
        [Id] int NOT NULL IDENTITY,
        [MovieId] int NOT NULL,
        [TrailerUrl] nvarchar(2084) NULL,
        [Name] nvarchar(2084) NULL,
        CONSTRAINT [PK_Trailer] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Trailer_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [Favorite] (
        [Id] int NOT NULL IDENTITY,
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_Favorite] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Favorite_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Favorite_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [Purchase] (
        [Id] int NOT NULL IDENTITY,
        [PurchaseNumber] uniqueidentifier NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL DEFAULT 9.9,
        [PurchaseDateTime] datetime2 NOT NULL DEFAULT (getdate()),
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_Purchase] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Purchase_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Purchase_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [Review] (
        [MovieId] int NOT NULL,
        [UserId] int NOT NULL,
        [Rating] decimal(3,2) NOT NULL DEFAULT 9.9,
        [ReviewText] nvarchar(max) NULL,
        CONSTRAINT [PK_Review] PRIMARY KEY ([MovieId], [UserId]),
        CONSTRAINT [FK_Review_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Review_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE TABLE [UserRole] (
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        CONSTRAINT [PK_UserRole] PRIMARY KEY ([RoleId], [UserId]),
        CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_Favorite_MovieId] ON [Favorite] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_Favorite_UserId] ON [Favorite] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_MovieCast_CastId] ON [MovieCast] ([CastId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_MovieGenre_GenreId] ON [MovieGenre] ([GenreId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_Purchase_MovieId] ON [Purchase] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_Purchase_UserId] ON [Purchase] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_Review_UserId] ON [Review] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_Trailer_MovieId] ON [Trailer] ([MovieId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    CREATE INDEX [IX_UserRole_UserId] ON [UserRole] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220619184201_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220619184201_InitialMigration', N'6.0.6');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621041928_UpdatingMovieTable')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'Revenue');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Movie] ALTER COLUMN [Revenue] decimal(18,2) NULL;
    ALTER TABLE [Movie] ADD DEFAULT 9.9 FOR [Revenue];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621041928_UpdatingMovieTable')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'Budget');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Movie] ALTER COLUMN [Budget] decimal(18,2) NULL;
    ALTER TABLE [Movie] ADD DEFAULT 9.9 FOR [Budget];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220621041928_UpdatingMovieTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220621041928_UpdatingMovieTable', N'6.0.6');
END;
GO

COMMIT;
GO

