USE [Biblioteka2022]
GO

/****** Object:  Table [dbo].[Ksiazki]    Script Date: 25.10.2022 15:19:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ksiazki](
	[Id_Ksiazka] [int] IDENTITY(1,1) NOT NULL,
	[Tytul] [varchar](100) NOT NULL,
	[Autor] [varchar](100) NOT NULL,
	[Opis] [varchar](200) NULL,
	[RokWydania] [varchar](4) NOT NULL
) ON [PRIMARY]
GO

