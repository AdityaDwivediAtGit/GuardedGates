USE [DapperMvcLearn_DB]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 04-03-2024 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04-03-2024 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[passwd] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_create_person]    Script Date: 04-03-2024 12:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_create_person]( @name nvarchar(100), @email nvarchar(100), @address nvarchar(200)) as
begin
	insert into dbo.Person (Name, Email, [Address])
	values (@name, @email, @address)
end	
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_person]    Script Date: 04-03-2024 12:20:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_delete_person](@id int) as
begin
	delete from dbo.Person
	where id = @id
end	
GO
/****** Object:  StoredProcedure [dbo].[sp_get_people]    Script Date: 04-03-2024 12:20:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_get_people] as
begin
	select * from dbo.Person
end	
GO
/****** Object:  StoredProcedure [dbo].[sp_get_person]    Script Date: 04-03-2024 12:20:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_get_person](@id int) as
begin
	select * from dbo.Person
	where id = @id
end	
GO
/****** Object:  StoredProcedure [dbo].[sp_update_person]    Script Date: 04-03-2024 12:20:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_update_person](@id int, @name nvarchar(100), @email nvarchar(100), @address nvarchar(200)) as
begin
	update dbo.Person 
	set name=@name, email=@email, [address]=@address
	where id = @id
end	
GO
