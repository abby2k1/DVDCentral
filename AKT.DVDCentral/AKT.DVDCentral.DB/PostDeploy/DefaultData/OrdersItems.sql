BEGIN
	INSERT INTO dbo.tblOrderItem (ID, OrderID, MovieID, Quantity, Cost)
	VALUES
		(1, 1, 1, 1, 11.77),
		(2, 2, 2, 1, 7.86),
		(3, 3, 3, 1, 186.09)
END;