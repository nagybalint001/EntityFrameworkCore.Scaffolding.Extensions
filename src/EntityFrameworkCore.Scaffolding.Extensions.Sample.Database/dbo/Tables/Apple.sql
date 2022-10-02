CREATE TABLE [dbo].[Apple]
(
	[Id] INT NOT NULL IDENTITY,
	[Status] INT NULL,
	[TreeId] INT NOT NULL,

    [CreatedAt] DATETIMEOFFSET NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
    [ModifiedAt] DATETIMEOFFSET NOT NULL,
    [ModifiedBy] UNIQUEIDENTIFIER NOT NULL,

    [IsDeleted] BIT NOT NULL,

    CONSTRAINT [pkApple] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [fkAppleToTree] FOREIGN KEY ([TreeId]) REFERENCES [Tree] ([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary key.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Status of the apple.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = N'COLUMN',
    @level2name = N'Status'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The tree it is belonging to.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = N'COLUMN',
    @level2name = N'TreeId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Apple entity',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Creation time.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = N'COLUMN',
    @level2name = N'CreatedAt'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Created by.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = N'COLUMN',
    @level2name = N'CreatedBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Last modification time.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedAt'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Last modified by.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Is deleted.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Apple',
    @level2type = N'COLUMN',
    @level2name = N'IsDeleted'