USE [MultiTracks]
GO
/****** Object:  StoredProcedure [dbo].[SearchArtistByName]    Script Date: 28/3/2023 13:44:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ilich Daniel Morales>
-- Create date: <Create Date,,27/03/2023 21:36>
-- Description:	<Description,,Stored procedure for API of search by 
--						      arstist name 'like' filtering by title or biography>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[SearchArtistByName]
	 @Name	VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [artistID], [dateCreation],
	[title], [biography], [imageURL],
	[heroURL]
	FROM [dbo].[Artist]
	WHERE title like '%' + @Name + '%' OR
	biography LIKE '%' + @Name + '%'
END