CREATE PROCEDURE [CarMaint].[uspNewMaint]
@MaintPerformed NVARCHAR (50),
@MaintCost FLOAT, 
@CreatedDate DATE,
@MaintID INT OUTPUT
AS
BEGIN
INSERT INTO [CarMaint].[Maintenance] (MaintPerformed, MaintCost, CreatedDate) VALUES (@MaintPerformed, @MaintCost, @CreatedDate);
SET @MaintID = SCOPE_IDENTITY();
RETURN @@ERROR
END