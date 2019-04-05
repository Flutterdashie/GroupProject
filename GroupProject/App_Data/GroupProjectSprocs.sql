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
@DateEdited datetime
)
as

begin
	UPDATE BlogPost set
	BlogPostTitle = @BlogPostTitle, 
	BlogPostMessage = @BlogPostMessage, 
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
		drop procedure BlogPostSelectById
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

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'SearchByTitle')
		drop procedure SearchByTitle
go

create procedure SearchByTitle(
@BlogPostTitle  varchar(40)
)as

begin
	select BlogPostId, BlogPostTitle, BlogPostMessage, DateAdded, DateEdited
	from BlogPost
	where BlogPostTitle LIKE CONCAT('%', @BlogPostTitle, '%')
end
go

if exists(select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'SearchById')
		drop procedure SearchById
go

create procedure SearchById(
@BlogPostId int
)as

begin
	select BlogPostId, BlogPostTitle, BlogPostMessage, DateAdded, DateEdited
	from BlogPost
	where BlogPostId = @BlogPostId
end
go

drop procedure if exists GetByMonth
go
create procedure GetByMonth(
@Month int
)
as 
begin
	select BlogPostId, BlogPostTitle, BlogPostMessage, DateAdded, DateEdited
	from BlogPost
	where @Month = MONTH(DateAdded)
end
go

drop procedure if exists GetPaginated
go
create procedure GetPaginated(
@Page int
)
as 
begin
	select BlogPostId, BlogPostTitle, BlogPostMessage, DateAdded, DateEdited
	from BlogPost
	order by DateAdded
	offset (@Page - 1) * 10 rows
	fetch next 10 rows only;
end
go
