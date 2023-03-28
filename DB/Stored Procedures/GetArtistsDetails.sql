USE [MultiTracks]
GO
/****** Object:  StoredProcedure [dbo].[GetArtistDetails]    Script Date: 28/3/2023 13:44:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Ilich Daniel Morales>
-- Create date: <Create Date,,27/03/2023 20:07>
-- Description:	<Description,,Get Artist Details is a stored procedure which takes all the data
--							  from the artist including Disks and Songs entities. Returns 3 data sets filtered by @artistID>
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[GetArtistDetails]
	@artistID	INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM Artist
	WHERE artistID = @artistID

	SELECT [albumID], A.artistID, A.[title], AR.title artistName,
	A.[year], A.[imageURL]
	FROM [MultiTracks].[dbo].[Album] A
	INNER JOIN Artist AR
	ON A.artistID = AR.artistID
	WHERE A.artistID = @artistID
	ORDER BY A.dateCreation DESC

	select songID, S.albumID, S.artistID, S.title, CONVERT(INT, bpm) bpm, A.title albumName, A.imageURL albumUrl
	from Song S
	INNER JOIN [Album] A
	ON S.artistID = A.artistID AND
	S.albumID = A.albumID
	WHERE S.artistID = @artistID
	ORDER BY S.dateCreation DESC
END
