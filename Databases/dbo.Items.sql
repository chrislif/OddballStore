CREATE TABLE [dbo].[Items] (
    [ItemID]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [Price]       INT            NOT NULL,
    [Thumbnail]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED ([ItemID] ASC)
);

