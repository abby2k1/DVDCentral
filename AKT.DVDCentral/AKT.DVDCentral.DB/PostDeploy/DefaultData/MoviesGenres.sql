BEGIN
	INSERT INTO dbo.tblMovieGenre (ID, MovieID, GenreID)
	VALUES
		(1, 1, 1),
		(2, 2, 2),
		(3, 3, 1)
END;