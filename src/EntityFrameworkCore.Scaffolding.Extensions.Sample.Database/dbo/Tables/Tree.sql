CREATE TABLE [dbo].[Tree]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(64) NOT NULL,
	[Description] NVARCHAR(256) NULL,
	[Type] INT NOT NULL,

    [CreatedAt] DATETIMEOFFSET NOT NULL,
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL,
    [ModifiedAt] DATETIMEOFFSET NOT NULL,
    [ModifiedBy] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [pkTree] PRIMARY KEY CLUSTERED ([Id] ASC),
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Tree entity',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Primary key.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = N'COLUMN',
    @level2name = N'Id'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Name of the tree.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = N'COLUMN',
    @level2name = N'Name'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Description of the tree.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Type of the tree.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = N'COLUMN',
    @level2name = N'Type'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Creation time.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = N'COLUMN',
    @level2name = N'CreatedAt'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Created by.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = N'COLUMN',
    @level2name = N'CreatedBy'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Last modification time.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedAt'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Last modified by.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tree',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedBy'