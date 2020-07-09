CREATE TABLE [dbo].[Post] (
    [Id]         INT            NOT NULL,
    [Title]      NVARCHAR (20)  NOT NULL,
    [Body]       NVARCHAR (MAX) NULL,
    [CreatedOn]  DATETIME       NULL,
    [ModifiedOn] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);