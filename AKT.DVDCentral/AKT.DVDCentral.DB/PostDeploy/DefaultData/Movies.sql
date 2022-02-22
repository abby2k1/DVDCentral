BEGIN
	INSERT INTO dbo.tblMovie (ID, Title, Description, Cost, RatingID, FormatID, DirectorID, InStkQty)
	VALUES
		(1, 'GTFO', 'Released Sep 13 2018', 11.77, 3, 3, 2, 2),
		(2, 'Déjà vu', 'Feat. Sia, Released May 5 2015', 7.86, 3, 3, 1, 3),
		(3, 'The Night Is Still Young', 'Released May 26 2015', 186.09, 3, 3, 3, 1)
END;