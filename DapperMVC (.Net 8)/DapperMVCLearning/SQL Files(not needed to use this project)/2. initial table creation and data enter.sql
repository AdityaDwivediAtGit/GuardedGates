use DapperMvcLearn_DB

create table dbo.Person(
	Id int primary key identity,
	Name nvarchar(100) not null,
	Email nvarchar(100) not null,
	[Address] nvarchar(200) not null
	)

insert into Person (Name, Email, [Address])
values ('Aditya Dwivedi', 'Aditya@aditya.com', 'heart'),
	   ('John Abraham', 'John.Abraham@hero.bollywood', 'Bombay')

select * from dbo.Person