use GroupProject
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'GetAllPosts')
		drop procedure GetAllPosts
go

create procedure GetAllPosts as
begin
	select BlogPostId, BlogPostTitle, BlogPostMessage, DateAdded, DateEdited
	from BlogPost
end
go


if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'BlogPostInsert')
		drop procedure BlogPostInsert
go

create procedure BlogPostInsert (
@BlogPostId int output,
@BlogPostTitle varchar(40),
@BlogPostMessage nvarchar(max),
@DateAdded datetime,
@DateEdited datetime
)
as

begin
	INSERT INTO BlogPost(BlogPostTitle, BlogPostMessage, DateAdded, DateEdited)
	values(@BlogPostTitle, @BlogPostMessage, @DateAdded, @DateEdited);

	set @BlogPostId = SCOPE_IDENTITY();
end
go


if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'BlogPostUpdate')
		drop procedure BlogPostUpdate
go

create procedure BlogPostUpdate (
@BlogPostId int,
@BlogPostTitle varchar(40),
@BlogPostMessage nvarchar(max),
@DateAdded datetime,
@DateEdited datetime
)
as

begin
	UPDATE BlogPost set
	BlogPostTitle = @BlogPostTitle, 
	BlogPostMessage = @BlogPostMessage, 
	DateAdded = @DateAdded, 
	DateEdited = @DateEdited 
	where BlogPostId = @BlogPostId
end
go


if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'BlogPostDelete')
		drop procedure BlogPostDelete
go

create procedure BlogPostDelete (
@BlogPostId int
)as

begin
	begin transaction

	delete from BlogPost where BlogPostId = @BlogPostId;

	commit transaction
end
go


if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'BlogPostSelectById')
		drop procedure CarSelect
go

create procedure BlogPostSelectById (
@BlogPostId int
)as

begin
	select BlogPostId, BlogPostTitle, BlogPostMessage, DateAdded, DateEdited
	from BlogPost
	where BlogPostId = @BlogPostId
end
go

