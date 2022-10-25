USE [Biblioteka2022]
GO

/****** Object:  Table [dbo].[Klienci]    Script Date: 25.10.2022 15:19:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Klienci](
	[Id_Klient] [int] IDENTITY(1,1) NOT NULL,
	[Imie] [varchar](100) NOT NULL,
	[Nazwisko] [varchar](100) NOT NULL,
	[Pesel] [varchar](11) NOT NULL,
	[Telefon] [varchar](20) NOT NULL
) ON [PRIMARY]
GO

