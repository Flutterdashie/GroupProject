use GroupProject
go


if exists(select * from sys.tables where name='BlogPost')
	drop table BlogPost
go


create table BlogPost (
BlogPostId int primary key identity(1,1),
BlogPostTitle varchar(40) not null,
BlogPostMessage nvarchar(max) not null,
DateAdded datetime not null,
DateEdited datetime not null
)

