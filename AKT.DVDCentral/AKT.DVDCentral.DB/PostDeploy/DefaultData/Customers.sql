BEGIN
	INSERT INTO dbo.tblCustomer (ID, FirstName, LastName, Address, City, State, Zip, Phone, UserID)
	VALUES
		(1, 'Franklin', 'Clinton', '7 Grove St', 'Los Santos', 'San Andreas', '90210', '9892108989', '1'),
		(2, 'Trevor', 'Philips', '1 Somewhere Ln', 'Sandy Shores', 'San Andreas', '90909', '9890002956', '2'),
		(3, 'Brad', 'Snider', '6 Under Rd', 'Ludendorff', 'North Yankton', '19450', '9181987103', '3')
END;