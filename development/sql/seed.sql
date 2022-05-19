INSERT INTO [RoomTypes]
	([Id], [Name])
VALUES
	('a5cd405d-6adc-4d0d-b82b-61f293679c16', 'Standard'),
	('56c1d50e-619b-48a4-a341-14fcb7afc6c4', 'Master');
	
INSERT INTO [Rooms] 
	([Id], [Number], [RoomTypeId])
VALUES
	('0860df1f-5ebb-4ef4-af25-944fe12a5b09', 1, 'a5cd405d-6adc-4d0d-b82b-61f293679c16'),
	('dd689f7b-fd08-49dd-9ae6-36768c027ee6', 2, '56c1d50e-619b-48a4-a341-14fcb7afc6c4');
	
INSERT INTO [HouseGuests]
	([Id], [Name], [Email])
VALUES
	('61f95eaa-717b-4d37-aa6c-aebc8256dbdd', 'Wesley Costa', 'wesley@mail.com');