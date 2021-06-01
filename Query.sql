USE OKEntrega;

SELECT * FROM Users;
SELECT * FROM Shippers;
SELECT * FROM ShipperCompanies;
SELECT * FROM Companies;
SELECT * FROM Orders;
SELECT * FROM FinishedOrders;
SELECT * FROM Occurrences;
SELECT * FROM Deliverers;

INSERT INTO Users VALUES
(NEWID(), 'Entregador', '$2a$11$OMFkZfVgWJprLzVIDbqypebeIM38FPScpecjSw1Vd2HM/NVSNy53a', GETDATE(), 1);

INSERT INTO Deliverers VALUES
(NEWID(), '11982039269', '67C82B41-5712-46B4-BF6F-E099C9069FF9', GETDATE(), NULL, NULL)

INSERT INTO Shippers VALUES
(NEWID(), 'danielamaralw13@gmail.com', '95D8D8D0-3BA0-4D44-B8AA-F25DFE8F3029', GETDATE(), NULL);

INSERT INTO ShipperCompanies VALUES
(NEWID(), 'E9A23721-9C67-4A03-9C03-94319A36692F', '7E08AAFD-CA2B-49F4-91C3-B88A70828A93', 0, GETDATE());

UPDATE Users
SET Active = 1
WHERE Id = '757889BA-A5B3-40AC-BC23-EAE46C7CB042';

UPDATE Shippers
SET CodeEmail = NULL
WHERE Id = 'E9A23721-9C67-4A03-9C03-94319A36692F';

UPDATE Shippers
SET Email = 'daniel.amaral720@gmail.com'
WHERE Id = 'E9A23721-9C67-4A03-9C03-94319A36692F'

UPDATE ShipperCompanies
SET ShipperRole = 1
WHERE Id = 'B8C8CF3F-4731-4048-A94E-7426B4DB2075';

DELETE FROM Users;
DELETE FROM Shippers;
DELETE FROM Deliverers;