USE [MultiTracks]
GO
/****** Object:  StoredProcedure [dbo].[GetSongs]    Script Date: 28/3/2023 13:45:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ilich Daniel Morales>
-- Create date: <Create Date,, 27/03/2023 21:40>
-- Description:	<Description,, Stored procedure with all the songs from filtered artist
--								API would be in charge of setting paging variables>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetSongs]
	@artistID	INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * 
	FROM Song
	WHERE artistID = @artistID
	ORDER BY dateCreation DESC
END
