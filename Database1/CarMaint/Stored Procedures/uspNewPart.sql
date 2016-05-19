CREATE PROCEDURE [CarMaint].[uspNewPart]
@PartName NVARCHAR (50),
@UnitPrice FLOAT,
@PartQuantity INT,
@SubTotal FLOAT,
@AddDate DATE,
@PartID INT OUTPUT
AS
BEGIN
INSERT INTO [CarMaint].[Parts] (PartName, UnitPrice, PartQuantity, SubTotal, AddDate) VALUES (@PartName, @UnitPrice, @PartQuantity, @SubTotal, @AddDate);
SET @PartID = SCOPE_IDENTITY();
RETURN @@ERROR
END