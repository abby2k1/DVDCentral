BEGIN
	INSERT INTO dbo.tblOrder (ID, CustomerID, OrderDate, UserID, ShipDate)
	VALUES
		(1, 1, 1-9-2022, 1, 2-1-2022),
		(2, 2, 1-18-2022, 2, 2-3-2022),
		(3, 3, 1-29-2022, 3, 2-3-2022)
END;