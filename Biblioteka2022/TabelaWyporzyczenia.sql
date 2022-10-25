USE [Biblioteka2022]
GO

/****** Object:  Table [dbo].[Wyporzyczenia]    Script Date: 25.10.2022 15:19:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Wyporzyczenia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Klient] [int] NOT NULL,
	[Id_Ksiazka] [int] NOT NULL,
	[DataWyporzyczenia] [date] NOT NULL,
	[DataZwrotu] [date] NOT NULL
) ON [PRIMARY]
GO

