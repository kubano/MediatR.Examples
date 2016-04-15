﻿
CREATE TABLE #TempLocale (

	[Id] UNIQUEIDENTIFIER  NOT NULL DEFAULT (newsequentialid()),
	[IsoCode] NVARCHAR (2) NULL ,
);

ALTER TABLE Locale NOCHECK CONSTRAINT ALL

BULK INSERT #TempLocale
    FROM '$(OutputPath)\Scripts\Data\Configuration\Locale.csv'
        WITH
    (
		FIRSTROW = 2,
		FIELDTERMINATOR = ';',
		ROWTERMINATOR = '\n',
		TABLOCK
    )
    	
MERGE dbo.Locale AS T
	USING #TempLocale AS S	
	ON 
	(
        (T.Id = S.Id)
	)
	WHEN MATCHED THEN
		UPDATE SET 
		T.[IsoCode] = S.[IsoCode]

	WHEN NOT MATCHED BY TARGET THEN 
		INSERT ([Id], [IsoCode])
		VALUES ([Id], [IsoCode])

    WHEN NOT MATCHED BY SOURCE THEN DELETE;
    
ALTER TABLE Locale WITH CHECK CHECK CONSTRAINT ALL
DROP TABLE #TempLocale
