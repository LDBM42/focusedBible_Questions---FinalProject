USE [master]
GO
/****** Object:  Database [focusedBible]    Script Date: 6/7/2019 5:38:04 p. m. ******/
CREATE DATABASE [focusedBible]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'focusedBible', FILENAME = N'E:\Instalados\SQL Server 2016\MSSQL13.MSSQLSERVER\MSSQL\DATA\focusedBible.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'focusedBible_log', FILENAME = N'E:\Instalados\SQL Server 2016\MSSQL13.MSSQLSERVER\MSSQL\DATA\focusedBible_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [focusedBible] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [focusedBible].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [focusedBible] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [focusedBible] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [focusedBible] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [focusedBible] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [focusedBible] SET ARITHABORT OFF 
GO
ALTER DATABASE [focusedBible] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [focusedBible] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [focusedBible] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [focusedBible] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [focusedBible] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [focusedBible] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [focusedBible] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [focusedBible] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [focusedBible] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [focusedBible] SET  ENABLE_BROKER 
GO
ALTER DATABASE [focusedBible] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [focusedBible] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [focusedBible] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [focusedBible] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [focusedBible] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [focusedBible] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [focusedBible] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [focusedBible] SET RECOVERY FULL 
GO
ALTER DATABASE [focusedBible] SET  MULTI_USER 
GO
ALTER DATABASE [focusedBible] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [focusedBible] SET DB_CHAINING OFF 
GO
ALTER DATABASE [focusedBible] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [focusedBible] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [focusedBible] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'focusedBible', N'ON'
GO
ALTER DATABASE [focusedBible] SET QUERY_STORE = OFF
GO
USE [focusedBible]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [focusedBible]
GO
/****** Object:  Table [dbo].[preguntas]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[preguntas](
	[codPreg] [int] IDENTITY(1,1) NOT NULL,
	[preg] [varchar](300) NOT NULL,
	[a] [varchar](200) NOT NULL,
	[b] [varchar](200) NOT NULL,
	[c] [varchar](200) NOT NULL,
	[d] [varchar](200) NOT NULL,
	[resp] [char](1) NOT NULL,
	[pasage] [varchar](50) NULL,
	[dificultad] [varchar](50) NULL,
	[IdLibro] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[codPreg] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[idCat] [int] IDENTITY(1,1) NOT NULL,
	[nombreCat] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Libros]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libros](
	[idLibro] [int] IDENTITY(1,1) NOT NULL,
	[nombreLibro] [varchar](20) NOT NULL,
	[idTestamento] [int] NOT NULL,
	[idCat] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idLibro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[PregCategoriaDificultad]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PregCategoriaDificultad]
AS
SELECT	P.codPreg,
		P.preg,
		P.a,
		P.b,
		P.c,
		P.d,
		P.resp,
		P.pasage,
		P.dificultad,
		L.nombreLibro,
		L.idTestamento,
		C.nombreCat
FROM preguntas AS P
INNER JOIN Libros AS L
ON L.idLibro = P.IdLibro
INNER JOIN Categorias C
ON C.idCat = L.idCat
GO
/****** Object:  Table [dbo].[Listener]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Listener](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Comando] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NULL,
	[Password] [varbinary](8000) NULL,
	[Tipo] [varchar](100) NULL,
	[Genero] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (1, N'Pentateuco')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (2, N'Históricos')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (3, N'Poéticos')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (4, N'Profetas Mayores')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (5, N'Profetas Menores')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (6, N'Evangelios')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (7, N'Historia')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (8, N'Cartas Paulinas')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (9, N'Cartas Generales')
INSERT [dbo].[Categorias] ([idCat], [nombreCat]) VALUES (10, N'Profecías')
SET IDENTITY_INSERT [dbo].[Categorias] OFF
SET IDENTITY_INSERT [dbo].[Libros] ON 

INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (1, N'Génesis', 1, 1)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (2, N'Éxodo', 1, 1)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (3, N'Levítico', 1, 1)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (4, N'Números', 1, 1)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (5, N'Deuteronomio', 1, 1)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (6, N'Josué', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (7, N'Jueces', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (8, N'Rut', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (9, N'1 Samuel', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (10, N'2 Samuel', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (11, N'1 Reyes', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (12, N'2 Reyes', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (13, N'1 Crónicas', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (14, N'2 Crónicas', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (15, N'Esdras', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (16, N'Nehemías', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (17, N'Ester', 1, 2)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (18, N'Job', 1, 3)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (19, N'Salmos', 1, 3)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (20, N'Proverbios', 1, 3)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (21, N'Eclesiastés', 1, 3)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (22, N'Cantares', 1, 3)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (23, N'Isaías', 1, 4)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (24, N'Jeremías', 1, 4)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (25, N'Lamentaciones', 1, 4)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (26, N'Ezequiel', 1, 4)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (27, N'Daniel', 1, 4)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (28, N'Oseas', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (29, N'Joel', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (30, N'Amós', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (31, N'Abdías', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (32, N'Jonás', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (33, N'Miqueas', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (34, N'Nahúm', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (35, N'Habacuc', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (36, N'Sofonías', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (37, N'Hageo', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (38, N'Zacarías', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (39, N'Malaquías', 1, 5)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (40, N'Mateo', 2, 6)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (41, N'Marcos', 2, 6)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (42, N'Lucas', 2, 6)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (43, N'Juan', 2, 6)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (44, N'Hechos', 2, 7)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (45, N'Romanos', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (46, N'1 Corintios', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (47, N'2 Corintios', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (48, N'Gálatas', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (49, N'Efesios', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (50, N'Filipenses', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (51, N'Colosenses', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (52, N'1 Tesalonicenses', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (53, N'2 Tesalonicenses', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (54, N'1 Timoteo', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (55, N'2 Timoteo', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (56, N'Tito', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (57, N'Filemón', 2, 8)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (58, N'Hebreos', 2, 9)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (59, N'Santiago', 2, 9)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (60, N'1 Pedro', 2, 9)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (61, N'2 Pedro', 2, 9)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (62, N'1 Juan', 2, 9)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (63, N'2 Juan', 2, 9)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (64, N'3 Juan', 2, 9)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (65, N'Judas', 2, 9)
INSERT [dbo].[Libros] ([idLibro], [nombreLibro], [idTestamento], [idCat]) VALUES (66, N'Apocalipsis', 2, 10)
SET IDENTITY_INSERT [dbo].[Libros] OFF
SET IDENTITY_INSERT [dbo].[Listener] ON 

INSERT [dbo].[Listener] ([ID], [Comando]) VALUES (1, N'')
SET IDENTITY_INSERT [dbo].[Listener] OFF
SET IDENTITY_INSERT [dbo].[preguntas] ON 

INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (1, N'¿Quién fue el primer rey de Israel?', N'David', N'Aarón', N'Saúl', N'Moisés', N'c', N'1 Sam 13', N'Normal', 9)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (2, N'¿Con que rey se casó Ester para convertirse en reina?', N'Darío', N'Asuero', N'Belsasar', N'Herodes', N'b', N'Est 2:16', N'Dificil', 17)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (3, N'¿Quién sirvió como profeta en el reinado del rey Darío el persa?', N'Zacarías', N'Hageo', N'Daniel', N'Jeremías', N'c', N'N/A', N'Normal', 27)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (4, N'¿Quién deseaba la viña de Nabot tanto como para poder dormir?', N'Acab', N'Jeroboam', N'Jezabel', N'Manases', N'a', N'1 Re 21', N'Dificil', 11)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (5, N'¿En qué día de la creación Dios hizo los animales?', N'Segundo', N'Tercero', N'Cuarto', N'Quinto', N'd', N'Gn 1:23-25', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (6, N'¿Qué general del ejército sirio fue leproso?', N'Naamán', N'Urías', N'Acan', N'Jefte', N'a', N'2 Re 5', N'Dificil', 12)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (7, N'¿Cuantos discípulos estuvieron presentes en la transfiguración?', N'Tres', N'Dos', N'Cuatro', N'Cinco', N'a', N'Mt 17:1-2', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (8, N'¿Cómo se llamaba el Padre de Juan el bautista?', N'Jacobo', N'Zacarías', N'Ezequías', N'Eli', N'b', N'Lc 1:5-14', N'Normal', 42)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (9, N'¿En que se originó el pecado?', N'Adán', N'Caín', N'Eva', N'Lucifer', N'd', N'Ez 28:14-15', N'Normal', 26)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (10, N'¿Cuál fue el nombre que Nabuconosor le puso a Azarías?', N'Sadrac', N'Mesac', N'Abed-Nego', N'Beltsasar', N'c', N'Dn 1:7', N'Dificil', 27)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (11, N'¿En la transfiguración de Jesús, este apareció junto a Moisés y', N'Eliseo', N'Enoc', N'Elías', N'Juan el Bautista', N'c', N'Mt 17:3', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (12, N'¿Quién fue el primogénito de Adán y Eva?', N'Cain', N'Abel', N'Set', N'Noe', N'a', N'Gn 4:1', N'Facil', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (13, N'¿Quién fue el padre de Samuel?', N'Eliu', N'Esaú', N'Elcana', N'Eli', N'c', N'1 Sam 1:19-20', N'Dificil', 9)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (14, N'¿Cuantas monedas le dieron a Judas por entregar a Jesús?', N'40 ', N'30', N'20', N'50', N'b', N'Mt 26:14-15', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (15, N'¿En qué día de la creación hizo Dios los árboles y las plantas?', N'Primero', N'Segundo', N'Tercero', N'Cuarto', N'c', N'Gn 1:11-13', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (16, N'¿Bienaventurados los pobres en espíritu porque', N'Ellos verán a Dios', N'De ellos es el reino', N'Ellos serán saciados', N'Ellos serán consolados', N'b', N'Mt 5:3', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (17, N'¿Qué pasaje del antiguo testamento leía el etíope cuando se encontró con Felipe?', N'Daniel 2', N'Éxodo 20', N'Isaías 53', N'Salmo 23', N'c', N'Hech 8:32-33', N'Normal', 44)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (18, N'¿En qué día de la creación hizo Dios al hombre?', N'Cuarto', N'Quinto', N'Sexto', N'Tercero', N'c', N'Gn 1:27-31', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (19, N'¿Quiénes fueron los discípulos que le dijeron a Jesús “Concédenos que en tu gloria nos sentemos uno a la derecha y el otro a tu izquierda”?', N'Andrés y Juan', N'Juan y Pedro', N'Andrés y Jacobo', N'Jacobo y juan', N'd', N'Mc 10:35-37', N'Normal', 41)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (20, N'¿En la parábola de las diez vírgenes. ¿Cuántas se durmieron? ', N'Cinco', N'Una', N'Diez', N'Dos', N'c', N'Mt 25:3-5', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (21, N'¿En qué día de la creación separo Dios la luz de la oscuridad?', N'Primero', N'Segundo', N'Tercero', N'Cuarto', N'a', N'Gn 1:1-4', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (22, N'¿Cómo eran los cabellos del personaje que mira el apóstol Juan en Apocalipsis cap. 1?', N'Dorados como el oro', N'Como bronce Bruñido', N'Blancos como la lana', N'Gris como la Espinela', N'c', N'Ap 1:14', N'Dificil', 66)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (23, N'¿En apocalipsis que simbolizan las siete estrellas en la diestra de Jesús?', N'Las 7 Iglesias', N'Los 7 Ángeles', N'Los 7 Juicios', N'Los 7 Espíritus', N'b', N'Ap 1:20', N'Dificil', 66)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (24, N'¿En las bodas de Cana cuantas tinajas de piedra llenaron de agua?', N'4', N'5', N'6', N'7', N'c', N'Jn 2:6', N'Dificil', 43)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (25, N'¿Con que dio de comer Jesús a 5000 personas?', N'5 panes y 2 peces', N'7 panes y 3 peces', N'5 peces y 2 panes', N'4 panes y 3 peces', N'a', N'Jn 6:9', N'Normal', 43)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (26, N'¿Cuál es el sexto mandamiento?', N'No tendrás dioses ajenos', N'No Hurtaras', N'No Mataras', N'No Codiciaras', N'c', N'Ex 20:14', N'Normal', 2)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (27, N'¿Qué libro relata la edificación de los muros de Jerusalén?', N'Jeremías', N'Isaías', N'Lamentaciones', N'Nehemías', N'd', N'N/A', N'Normal', 16)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (28, N'¿En qué libro se encuentra la profecía del gran sufrimiento de Jesús?', N'Abdías', N'Génesis', N'Jeremías', N'Isaías', N'd', N'N/A', N'Normal', 23)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (29, N'¿Quién dijo, apártate de mi Señor porque soy hombre pecador?', N'Pablo', N'Pedro', N'Andrés', N'Mateo', N'b', N'Lc 5:8', N'Normal', 42)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (30, N'¿Cómo fueron llamados los jefes de Israel después de la muerte de Josué?', N'Reyes', N'Sacerdotes', N'Jueces', N'Levitas', N'c', N'N/A', N'Normal', 7)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (31, N'Sobre los fariseos caían todos los asesinatos, desde Abel hasta el hijo de Berequías asesinado entre el templo y el altar. ¿Quién es el hijo de Berequías?', N'Jetró', N'Eleazar', N'Eliézer', N'Zacarías', N'd', N'Mt 23:35', N'Dificil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (32, N'¿Quién fue bendecido por tener en su casa el arca del Señor?', N'David', N'Saul', N'Odeb-Edom', N'Obeb-Tubal', N'c', N'2 Sam 6:11', N'Dificil', 10)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (33, N'¿De quién era esposa Herodías?', N'Herodes', N'Felipe', N'Agripa', N'Pilato', N'b', N'Mc 6:17', N'Dificil', 41)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (34, N'¿Durante el reinado de quien se halló el libro de la ley que por mucho tiempo estaba perdido?', N'Salomón', N'Ezequías', N'Isaías', N'Josías', N'd', N'2 Re 22:3-8', N'Normal', 12)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (35, N'¿A cuántos profetas de Baal degolló Elías?', N'400', N'420', N'430', N'450', N'd', N'1 Re 18:22-40', N'Dificil', 11)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (36, N'¿A cuántos hombres alimento Eliseo con 20 panes de cebada?', N'100', N'200', N'300', N'5000', N'a', N'2 Re 4:42-44', N'Dificil', 12)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (37, N'¿Cómo se llamó el rey que vio una mano escribiendo en la pared?', N'Nabucodonosor', N'Daniel', N'Belsasar', N'Ciro', N'c', N'Dn 5:1-5', N'Dificil', 27)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (38, N'¿Quién estuvo 3 días y tres noches en el vientre de un pez?', N'Sofonías', N'Arquelao', N'Jonás', N'Mateo', N'c', N'Jon 1:16', N'Facil', 32)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (39, N'¿Qué hecho convirtió a David en héroe para Israel?', N'Su victoria sobre Goliat', N'Sus poesías', N'Su victoria sobre los babilonios', N'Su victoria sobre Jericó', N'a', N'1 Sam 17', N'Facil', 9)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (40, N'¿Quién fue arrojado al foso de los leones y salió intacto de ahí?', N'Juan Bautista', N'Daniel', N'Moisés', N'Pablo', N'b', N'Dn 6:17', N'Facil', 27)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (41, N'¿A quién reclama Jesús en Getsemaní: "¿Ni siquiera una hora pudiste mantener despierto?"', N'Juan', N'Pedro', N'Andrés', N'Judas Tadeo', N'b', N'Mc 14:37', N'Normal', 41)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (42, N'¿Hijo de Abraham que estuvo a punto de ser sacrificado?', N'Esaú', N'Jonás', N'José', N'Isaac', N'd', N'Gn  22:1', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (43, N'Apóstol que había sido cobrador de impuestos para Roma.', N'Marcos', N'Mateo', N'Lucas', N'Juan', N'b', N'Mt 9:9', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (44, N'Junto con José estaban en la cárcel el _____ y el _____ del faraón.', N'Amigo y el enemigo', N'Hermano y el primo', N'Copero y el Panadero', N'General y el consejero', N'c', N'Gn 40:1', N'Dificil', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (45, N'Rey al que los magos visitaron en Judea para preguntar por el Mesías.', N'David', N'Herodes', N'Cesar Augusto', N'Agripa', N'b', N'Mt 2:1', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (46, N'Quién sucede a David en el trono de Israel.', N'Miqueas', N'Salomón', N'Roboam', N'Jeroboam', N'b', N'1 Re 1:33', N'Normal', 11)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (47, N'¿Qué profeta sintió que la burra le hablaba?', N'Elias', N'Isaias', N'Zacarías', N'Balaam', N'd', N'Num 22:21-35', N'Normal', 4)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (48, N'Rey de Babilonia que fabricó una estatua de sí y exigió adorarla.', N'Nabucodonosor', N'Ciro', N'Ramsés', N'Sihón', N'a', N'Dn 3', N'Normal', 27)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (49, N'Apóstol elegido para reemplazar a Judas.', N'Matías', N'Bernabé', N'Apolos', N'Silas', N'a', N'Hech 1:26', N'Normal', 44)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (50, N'Nuera de Noemí que al morir el esposo la acompañó hasta Belén. Un libro del A. T. lleva su nombre.', N'Ester', N'Judit', N'Rut', N'Rebeca', N'c', N'Rut 1:18', N'Dificil', 8)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (51, N'¿Quiénes son los apóstoles a los que Jesús llamó: hijos del trueno?', N'Tomás y Mateo', N'Santiago y Juan', N'Simón el cananeo y Judas Iscariote', N'Felipe y Bartolomé', N'b', N'Mc 3:13', N'Dificil', 41)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (52, N'¿Cuántos hijos tuvo Jacob?', N'12', N'6', N'7', N'11', N'a', N'Gn 35:23', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (53, N'¿Cuáles fueron los apóstoles que estuvieron con Jesús en la transfiguración?', N'Santiago, hijo de Alfeo, y Tadeo', N'Bartolomé y Juan', N'Felipe, Pedro y Andrés', N'Pedro, Santiago y Juan', N'd', N'Mt 17:1', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (54, N'¿Cuál era el nombre de Abraham antes de su Alianza con Dios?', N'Aarón', N'Israel', N'Jacob', N'Abram', N'd', N'Gn 17:5', N'Facil', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (55, N'¿Quién perfumó los pies de Jesús en casa de Simón el leproso?', N'Martha', N'María la hermana de Lázaro', N'Herodías', N'María, su madre', N'b', N'Jn 12:3', N'Normal', 43)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (56, N'Patriarca emigrante de Ur de Caldea.', N'Aarón', N'Jacob', N'Isaac', N'Abraham', N'd', N'Gn 12', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (57, N'Sacerdote que ofreció por Abraham un sacrificio de pan y vino.', N'Melquisedec', N'Potifera', N'Jetró', N'Elcana', N'd', N'Gn 14', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (58, N'¿Para librar a Pablo de los judíos de Jerusalén, que le tenían una emboscada fue enviado de noche a Cesarea, a qué gobernador se lo enviaron?', N'A Jairo', N'A Felipe', N'A Félix', N'A Herodias', N'c', N'Hech 23:24', N'Dificil', 44)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (59, N'Primer rey de Israel.', N'Saúl', N'Abimélec', N'David', N'Nahas', N'a', N'1 Sam 9', N'Normal', 9)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (60, N'Primer mártir del cristianismo.', N'Pedro', N'Esteban', N'Apolos', N'Santiago', N'b', N'Hech 7', N'Normal', 44)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (61, N'Hijo de Abraham y de Agar.', N'Isaac', N'Jacob', N'José', N'Ismael', N'd', N'Gn 16', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (62, N'¿A qué personas, en forma individual, les escribió Pablo cartas?', N'Santiago, Juan, Timoteo', N'Filemón, Timoteo, Tito', N'Tito, Pedro, Timoteo', N'Filemón, Judas, Santiago', N'b', N'N/A', N'Normal', NULL)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (63, N'Rey de Israel muerto por una flecha tirada al azar.', N'Agag', N'Aquís', N'Talmai', N'Acab', N'd', N'1 Re 22', N'Dificil', 11)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (64, N'¿Quién es el padre de José el esposo de María?', N'Absalón', N'Jacob', N'Josías', N'Jeconías', N'b', N'Mt 1:16', N'Dificil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (65, N'Primer sumo sacerdote de Israel.', N'Melquisedec', N'Potifera', N'Aarón', N'Reuel', N'c', N'Ex 29', N'Normal', 2)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (66, N'Jueces de Israel que combatieron contra Sísara.', N'Débora y Barac', N'Ladán y Simí', N'Térah y Nahor', N'Jeús y Beriá', N'a', N'Jue 4', N'Dificil', 7)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (67, N'En su discurso sobre el fin del mundo, ¿qué profeta dice Jesús que predijo la abominación desoladora.', N'Daniel', N'Natán', N'Gad', N'Ahías', N'a', N'Mt 24:15', N'Dificil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (68, N'Primer Ministro de Asuero que buscaba la muerte del tío de Esther.', N'Nebuzaradán', N'Ahuzat', N'Ficol', N'Amán', N'd', N'Est 3', N'Dificil', 17)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (69, N'¿Cuáles son los discípulos enviados a preparar la pascua?', N'Andrés y Santiago', N'Felipe y Bartolomé', N'Tomás y Mateo', N'Pedro y Juan', N'd', N'Lc 22:8', N'Dificil', 42)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (70, N'Tercer hijo de Adán.', N'Set', N'Enós', N'Cainán', N'Mahalalel', N'a', N'Gn 4:25', N'Normal', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (71, N'Ciego de Jericó que a gritos decia a Jesús: !Hijo de David, ten misericordia de mí!.', N'Timeo', N'Bartimeo', N'Malco', N'Elimas', N'b', N'Mc 10:46', N'Dificil', 41)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (72, N'El Faraón soñó siete vacas gordas y...', N'un río de aguas cristalinas.', N'siete vacas flacas.', N'un lobo asechando.', N'siete establos.', N'b', N'Gn 41:1', N'Facil', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (73, N'Cuando Pedro se opone al anuncio de Jesús de que iría a Jerusalén a sufrir, ¿qué duras palabras dirigió Jesús a Pedro?', N'No escucharé más tus consejos.', N'Apártate de mí Satanás.', N'No tentarás al Señor tu Dios.', N'¿Es que aún no me conoces?', N'b', N'Mt 16:23', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (74, N'Completa la frase del profeta Isaías: La virgen dará a luz un hijo y le...', N'dará gran dolor a su corazón.', N'enseñará el camino al Padre.', N'pondrá por nombre Emmanuel.', N'conocerá todo el mundo.', N'c', N'Is 7:14', N'Normal', 23)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (75, N'¿Qué calificativo suele dar Jesús a los escribas y fariseos?', N'Codiciosos', N'Hipócritas', N'Vanidosos', N'Blanqueados', N'b', N'Mt 23:13', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (76, N'¿Qué pide Jesús para los enemigos?', N'Amor', N'Comprensión', N'Castigo', N'Indiferencia', N'a', N'Mt 5:44', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (77, N'Completa esta sentencia de Cristo: “El que come mi cuerpo y bebe mi sangre...', N'Será fortalecido.', N'Me conoce a mí y yo a él.', N'Entrará al reino de los cielos.', N'tiene vida eterna; y yo le resucitaré en el día postrero.', N'd', N'Jn. 6:54', N'Normal', 43)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (78, N'¿Qué ejemplo usa Jesús para decir que no hay que juzgar?', N'La viga y la paja en el ojo.', N'El rollo de la ley abierto o cerrado.', N'Haz el bien, sin mirar a quien.', N'El que es buen juez, por su casa empieza.', N'a', N'Mt 7', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (79, N'¿El más importante y el primero de los mandamientos de la ley de Moisés?', N'Ama a Dios con todo tu corazón, con toda tu alma y con toda tu mente.', N'Haz el bien, sin mirar a quien.', N'Ama a tu prójimo como a ti mismo.', N'Vende todo y regálalo a los pobres.', N'a', N'Mt 22:37', N'Facil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (80, N'¿Cómo son la puerta y el camino que llevan a la vida?', N'Amplios y hermosos.', N'Soberbia y fácil respectivamente.', N'Ancha y escarpado respectivamente.', N'Angostos y difíciles', N'd', N'Mt 7:13', N'Facil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (81, N'¿Qué se hace con todo árbol que no da buenos frutos?', N'Se abona y se cuida.', N'Se riega con aguas de manantial.', N'Se corta y se quema.', N'Se deja abandonado.', N'c', N'Mt 7:19', N'Facil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (82, N'"Señor, no soy digno de que entres en mi casa". ¿Quién dijo eso?', N'Un centurión (capitán) romano.', N'José de Arimatea.', N'Jairo.', N'Zaqueo.', N'a', N'Mt 8:8', N'Facil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (83, N'"No te metas con ese hombre porque anoche tuve un sueño horrible por su causa". ¿De quién eran esas palabras?', N'De la esposa de Pilatos', N'De la esposa de Herodes', N'De Herodías', N'De Pilatos', N'a', N'Mt 27:19', N'Facil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (84, N'¿Qué significa "Elí, Elí, lama sabactani"?', N'Padre, perdónalos pues no saben lo que hacen.', N'Apártate de mí Satanás.', N'Dios mío, Dios mío, ¿por qué me has abandonado?', N'Si es posible, aparta de mí este cáliz.', N'c', N'Mt 27:46', N'Facil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (85, N'¿Cuál fue la encomienda final de Jesús a sus discípulos, antes de subir al cielo?', N'Vayan a todo hombre y perdónenlo en mi nombre.', N'Vayan a cada ciudad y permanezcan ahí hasta que me conozcan.', N'Sean fieles y busquen su salvación.', N'id, y haced discípulos a todas las naciones, bautizándolos en el nombre del Padre, y del Hijo, y del Espíritu Santo.', N'd', N'Mt 28:19', N'Facil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (86, N'¿A la hija de qué jefe de la sinagoga resucita Cristo?', N'Anás', N'Jairo', N'Zacarías', N'Zaqueo', N'b', N'Mc 5:22', N'Normal', 41)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (87, N'Completa el fragmento del Salmo: “Jehová es mi pastor; nada me faltará.', N'Junto a aguas de reposo me pastoreará.', N'Me guiará por sendas de justicia..', N'Confortará mi alma;', N'En lugares de delicados pastos me hará descansar', N'd', N'Sal 23:2', N'Normal', 19)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (88, N'¿Qué responde Jesús cuando el diablo le dice que convierta unas piedras en pan? No sólo de pan viveel Hombre...', N'así que apártate de mí Satanás.', N'y no tentarás al Señor tu Dios', N'el agua también le es necesaria.', N'si no de toda palabra que sale de la boca de Dios.', N'd', N'Mt 4:3', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (89, N'¿Para qué dio Dios al mundo a su Hijo único?', N'Para que le conociera.', N'Para viviera en el mundo.', N'Para que el que crea en él, tenga vida eterna.
', N'Para demostrar que somos hechos a su semejanza.', N'c', N'Jn 3:16', N'Facil', 43)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (90, N'¿Cómo dice Juan Bautista que bautizará al que ha de venir después de él?', N'Con el agua que da vida.', N'Con gran poder y autoridad.', N'Sin distinciones.', N'Con el Espíritu Santo.', N'd', N'Mt 3:11', N'Facil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (91, N'Cualquiera que mire con deseo a una mujer en palabras de Jesús ¿qué es lo que comete?', N'Felonía', N'Adulterio', N'Traición', N'Engaño', N'b', N'Mt 5:28', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (92, N'¿Qué significa Moisés?', N'El hijo de Faraón', N'Salvado de las aguas', N'Salvador del pueblo de Dios', N'Hacedor de prodigios', N'b', N'Ex 2:10', N'Normal', 2)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (93, N'Tras la triple profesión de amor de Pedro. ¿Qué le dijo Jesús?', N'Apacienta mis ovejas.', N'Te devuelvo las llaves del reino.', N'Sí serás la piedra sobre la que edificaré mi Iglesia.', N'Padecerás por mí.
', N'a', N'Jn 21:17', N'Dificil', 43)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (94, N'¿Con qué palabras acepta María el plan de Dios?', N'Que sea como ha querido', N'Le pondré a mi hijo Emmanuel', N'Lo acepto mi Señor', N'He aquí la sierva del Señor', N'd', N'Lc 1:38', N'Dificil', 42)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (95, N'¿Qué cántico entona María cuando se encuentra con su prima Isabel?', N'Bendita la que viene en el nombre del Señor...', N'Mi hijo ha saltado en mi seno...', N'Bendita la casa que te recibe...', N'Engrandece mi alma al Señor...', N'd', N'Lc 1:46-55', N'Dificil', 42)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (96, N'¿Qué significa la palabra Abraham?', N'Padre de muchas naciones.', N'El que sale del desierto.', N'El que ha luchado contra Dios', N'Padre de la Fe', N'a', N'Gn 17:3-5', N'Dificil', 1)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (97, N'¿Con qué palabras presenta Juan Bautista a Jesús?', N'He ahí el elegido', N'¿Quién soy yo para bautizarte?', N'He aquí el Cordero de Dios, que quita el pecado del mundo.', N'Este es el que he anunciado.', N'c', N'Jn 1:29', N'Dificil', 43)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (98, N'Completa la frase: "Ni se enciende una luz y se pone debajo de un almud, sino...', N'que debe dejarse a la vista.', N'sobre el candelero, y alumbra a todos los que están en casa.', N'que debe ponerse en un lugar seguro de cada hogar', N'se pone en un lugar adecuado para que dé luz y calor.', N'b', N'Mt 5:15', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (99, N'"sed vosotros perfectos como vuestro Padre celestial es perfecto" ¿en qué capítulo de Mateo está?', N'Mateo 3', N'Mateo 6', N'Mateo 5', N'Mateo 8', N'c', N'N/A', N'Dificil', 40)
GO
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (100, N'¿Por qué el reino de Dios es como el dueño de una casa?', N'Porque siempre esta pendiente de sus pertenencias.', N'Porque de lo guardado saca lo nuevo y lo viejo.', N'Porque siempre esta al cuidado de ella.', N'Porque se cuida del ladrón.', N'b', N'Mt 13:52', N'Dificil', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (101, N'¿De cuántos capítulos consta el Libro de Isaías?', N'55', N'66', N'44', N'33', N'b', N'N/A', N'Dificil', NULL)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (102, N'¿A qué profeta alude Jesús cuando al echar a los vendedores les dice: "Escrito está: Mi casa, casa de oración será llamada; mas vosotros la habéis hecho cueva de ladrones."?', N'Isaías', N'Jeremías', N'Zacarías', N'Daniel', N'a', N'N/A', N'Dificil', 23)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (103, N'¿Con qué palabras concluye Jesús la parábola de la fiesta de bodas?', N'El que persevere hasta el final entrará al reino de los cielos.', N'Los últimos serán los primeros.', N'Porque muchos son llamados, y pocos escogidos.', N'Sean santos, como mi Padre y yo somos santos.', N'c', N'Mt 22:14', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (104, N'¿Qué personaje bíblico dijo: "Jehová dio, y Jehová quitó; sea el nombre de Jehová bendito."?', N'David', N'Roboam', N'Job', N'Sansón', N'c', N'N/A', N'Normal', 18)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (105, N'Para acusar a los fariseos, Cristo usa esta sentencia, complétala: "Ustedes, guías ciegos, cuelan elmosquito pero...', N'Se tragan todo lo demás.', N'Se tragan el camello.', N'Pero no cuidan de las cosas de Dios.', N'No cuidan lo que se les ha encomendado.', N'b', N'Mt 23:24', N'Normal', 40)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (106, N'¿En qué capítulo habla Ezequiel de los huesos secos?', N'37', N'15', N'28', N'4', N'a', N'N/A', N'Dificil', 26)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (107, N'¿De dónde huye Moisés por haber matado a un hombre?', N'Medián', N'Jericó', N'Egipto', N'Jerusalén', N'c', N'Ex 12:11-15', N'Normal', 2)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (109, N'¿Quién y donde anunció a María que sería madre del Mesías?', N'Miguel en Belén de Judea', N'Miguel en Nazareth de Galilea', N'Gabriel en Belén de Judea', N'Gabriel en Nazareth de Galilea', N'd', N'Lc 1:26', N'Dificil', 42)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (110, N'¿Contra que pueblo fueron enviadas las plagas?', N'Babilonia', N'Roma', N'Saba', N'Egipto
', N'd', N'Ex 7:11', N'Facil', 2)
INSERT [dbo].[preguntas] ([codPreg], [preg], [a], [b], [c], [d], [resp], [pasage], [dificultad], [IdLibro]) VALUES (111, N'¿En dónde se apareció el Señor por primera vez a Salomón? ', N'Jericó', N'Gabaón', N'Ararat', N'Canaán', N'b', N'1 Re 9.2', N'Dificil', 11)
SET IDENTITY_INSERT [dbo].[preguntas] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id], [Usuario], [Password], [Tipo], [Genero]) VALUES (1, N'LDBM', 0x01000000BA27B523D930F726AFB31DE2A883325E40A13136F5D23436, N'Admin', N'M')
INSERT [dbo].[Usuario] ([id], [Usuario], [Password], [Tipo], [Genero]) VALUES (4, N'Admin', 0x01000000E7FE78FA7C114D123AF8906BD05ED79BE82C28AB00824ED8, N'Admin', N'Admin')
INSERT [dbo].[Usuario] ([id], [Usuario], [Password], [Tipo], [Genero]) VALUES (1010, N'wliz', 0x0100000027906A50D6EE2687BB3C110D4579514EB998DEA1427BA7F9, N'Admin', N'M')
INSERT [dbo].[Usuario] ([id], [Usuario], [Password], [Tipo], [Genero]) VALUES (1011, N'Estefany', 0x01000000B9A1244B570BB45622558F1E50382510FF0B8983C2E19BC1, N'Admin', N'F')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[Libros]  WITH CHECK ADD  CONSTRAINT [fk_IDCAT_LIBROS] FOREIGN KEY([idCat])
REFERENCES [dbo].[Categorias] ([idCat])
GO
ALTER TABLE [dbo].[Libros] CHECK CONSTRAINT [fk_IDCAT_LIBROS]
GO
ALTER TABLE [dbo].[preguntas]  WITH CHECK ADD  CONSTRAINT [fk_IDLIBRO_PREG] FOREIGN KEY([IdLibro])
REFERENCES [dbo].[Libros] ([idLibro])
GO
ALTER TABLE [dbo].[preguntas] CHECK CONSTRAINT [fk_IDLIBRO_PREG]
GO
/****** Object:  StoredProcedure [dbo].[sp_Data_FLogin_ValidarLogin]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Data_FLogin_ValidarLogin]
	@Usuario varchar(100),
	@Password varchar(100)
AS
BEGIN
	select	Id, Usuario,
			CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('password', Password)) as Password,Tipo
		 from	[dbo].[Usuario]
		 where	Usuario=@Usuario and 
				CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('password', Password))=@Password
END


GO
/****** Object:  StoredProcedure [dbo].[sp_Data_FUsuario_Actualizar]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Data_FUsuario_Actualizar] 
(
@Usuario varchar(50),
@Password varchar(MAX),
@Tipo varchar(100),
@Genero varchar(10)
)
AS

Update [dbo].[Usuario] set Usuario=@Usuario,
		Password=ENCRYPTBYPASSPHRASE('password', @Password),Tipo=@Tipo, Genero=@Genero
	where Usuario=@Usuario

Select @@ROWCOUNT as cantidad

GO
/****** Object:  StoredProcedure [dbo].[sp_Data_FUsuario_Eliminar]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Data_FUsuario_Eliminar]

(
@Id int 
) 
as
delete from [dbo].[Usuario] where Id = @Id



GO
/****** Object:  StoredProcedure [dbo].[sp_Data_FUsuario_ExistUser]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Data_FUsuario_ExistUser]
(
@Usuario varchar(50)
)
AS
	select COUNT(Usuario) from [dbo].[Usuario] where Usuario = @Usuario
	


GO
/****** Object:  StoredProcedure [dbo].[sp_Data_FUsuario_Insertar]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Data_FUsuario_Insertar] 
(
@Usuario varchar(50),
@Password varchar(MAX),
@Tipo varchar(100),
@Genero varchar(10)
)
AS

insert into [dbo].[Usuario] (Usuario,Password,Tipo,Genero)
values (@Usuario,ENCRYPTBYPASSPHRASE('password', @Password),@Tipo,@Genero)

Select @@IDENTITY

GO
/****** Object:  StoredProcedure [dbo].[sp_Data_FUsuario_Login]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Data_FUsuario_Login]
(
@Usuario varchar(50)
)	
AS
BEGIN
	select Usuario,CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('password', Password)) as Contraseña,Tipo 
	from [dbo].[Usuario]
	where @Usuario = Usuario
END


GO
/****** Object:  StoredProcedure [dbo].[sp_editar]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_editar](
	@codPreg int,
	@preg varchar(300),
	@a varchar(200),
	@b varchar(200),
	@c varchar(200),
	@d varchar(200),
	@resp char(1))
as
	update empleados set preg=@preg,a=@a,b=@b,
	c=@c, d=@d, resp=@resp where codPreg=@codPreg


GO
/****** Object:  StoredProcedure [dbo].[sp_listar]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_listar](
@codPreg int)
as
select * from preguntas where(@codPreg = codPreg)


GO
/****** Object:  StoredProcedure [dbo].[sp_ListarCategoriasxTestamento]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListarCategoriasxTestamento](
@testamento1 int,
@testamento2 int
)
AS
SELECT nombreCat
FROM Categorias C
INNER JOIN Libros L
ON L.idCat = C.idCat 
WHERE L.idTestamento = @testamento1 OR L.idTestamento = @testamento2
GROUP BY C.nombreCat
GO
/****** Object:  StoredProcedure [dbo].[sp_ListarLibrosxCategorias]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ListarLibrosxCategorias](
@nombreCat1 VARCHAR(20),
@nombreCat2 VARCHAR(20),
@nombreCat3 VARCHAR(20),
@nombreCat4 VARCHAR(20),
@nombreCat5 VARCHAR(20),
@nombreCat6 VARCHAR(20),
@nombreCat7 VARCHAR(20),
@nombreCat8 VARCHAR(20),
@nombreCat9 VARCHAR(20),
@nombreCat10 VARCHAR(20)
)
AS
SELECT nombreLibro
FROM Libros L
INNER JOIN Categorias C
ON C.idCat = L.idCat
WHERE nombreCat IN(	@nombreCat1, @nombreCat2,
					@nombreCat3, @nombreCat4,
					@nombreCat5, @nombreCat6,
					@nombreCat7, @nombreCat8,
					@nombreCat9, @nombreCat10)
GO
/****** Object:  StoredProcedure [dbo].[sp_listarTodo]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_listarTodo]
as
select * from preguntas


GO
/****** Object:  StoredProcedure [dbo].[sp_update]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_update](
	@codPreg int,
	@preg varchar(300),
	@a varchar(200),
	@b varchar(200),
	@c varchar(200),
	@d varchar(200),
	@resp char(1))
as
	update preguntas set preg=@preg,a=@a,b=@b,
	c=@c, d=@d, resp=@resp where codPreg=@codPreg


GO
/****** Object:  StoredProcedure [dbo].[sp_Usuario_Genero_yTipo]    Script Date: 6/7/2019 5:38:04 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_Usuario_Genero_yTipo](
@usuario varchar(50))
as
select Genero, Tipo from [dbo].[Usuario] where(@usuario = Usuario)
GO
USE [master]
GO
ALTER DATABASE [focusedBible] SET  READ_WRITE 
GO
