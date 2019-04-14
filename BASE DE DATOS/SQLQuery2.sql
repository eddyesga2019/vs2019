EXEC ups_GetAll 'AERO%'

GO



create procedure ups_GetAll
(
   @FilterByName NVARCHAR(100)

)
AS 
BEGIN

SELECT * FROM Artist
wHERE Name Like @FilterByName

END