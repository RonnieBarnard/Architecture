declare @username varchar(200) = {0};
declare @password varchar(200) = {1};

SELECT  
	Users.LastName, 
	Users.FirstName, 
	Users.UsersKey,
	Users.defaultpage, 
	Users.passworddate, 
	autosave, 
	weekendaccess, 
	Users.active, 
	Users.suffix, 
	convert(char(5), AccessStart, 108) as AccessStart, 
	convert(char(5), AccessEnd, 108) as AccessEnd
From Users with (nolock)
WHERE Users.username = @username
	and Users.passwordhash = @password
	and Users.active = 1;