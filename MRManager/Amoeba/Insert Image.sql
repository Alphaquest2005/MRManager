set identity_insert Media on

INSERT INTO Media(value, MediaTypeId, Id)
 VALUES(
	(SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Prism\Clients\SAMS-MRManager\images\People-Patient-Female-icon.png', SINGLE_BLOB) AS x), 1, 0)

set identity_insert Media off