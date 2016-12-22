--------------------------------------Insert Test Data --------------------------------------------------
declare @AppName varchar(50)
set @AppName = 'Amoeba'

declare @appId int
set @appId = (select Id from AmoebaDB.dbo.Applications where Name = @AppName)
PRINT @appId 



DECLARE @entityId int

delete from AmoebaDB.dbo.TestValues
------------------------For Each Entity
DECLARE Entity_CURSOR CURSOR 
  LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR 
SELECT DISTINCT Id 
FROM AmoebaDB.dbo.Entities where id in (SELECT DISTINCT  ApplicationEntities.EntityId
											FROM            AmoebaDB.dbo.ApplicationEntities INNER JOIN
																	 AmoebaDB.dbo.EntityProperties ON ApplicationEntities.EntityId = EntityProperties.EntityId
											WHERE        (ApplicationEntities.ApplicationId = @appId)/** and (ApplicationEntities.EntityId not in (SELECT DISTINCT Entities.Id
											FROM            AmoebaDB.dbo.ApplicationEntities INNER JOIN
																	 AmoebaDB.dbo.EntityProperties ON ApplicationEntities.EntityId = EntityProperties.EntityId INNER JOIN
																	 AmoebaDB.dbo.DataProperties ON EntityProperties.Id = DataProperties.EntityPropertyId INNER JOIN
																	 AmoebaDB.dbo.DataTypes ON DataProperties.DataTypeId = DataTypes.Id INNER JOIN
																	 AmoebaDB.dbo.Entities ON ApplicationEntities.EntityId = Entities.Id
											WHERE        (DataTypes.DBType = N'varbinary')
											GROUP BY Entities.Id))**/)

OPEN Entity_CURSOR
FETCH NEXT FROM Entity_CURSOR INTO @entityId
WHILE @@FETCH_STATUS = 0
BEGIN 
	PRINT @entityId
    --Do something with Id here
	declare @entityName varchar(50)
	set @entityName = (select schemaName + '.' + EntitySetName from AmoebaDB.dbo.Entities where id = @entityId)
	PRINT @entityName

--------------------------For Each Data Row	
	DECLARE @rowId int
	Exec('DECLARE Data_CURSOR CURSOR 
	Global STATIC READ_ONLY FORWARD_ONLY
	FOR 
	SELECT Id  
	FROM ' + @entityName)

		OPEN Data_CURSOR
		FETCH NEXT FROM Data_CURSOR INTO @rowId
		WHILE @@FETCH_STATUS = 0
		BEGIN 

-------------------------For Each Entity Property	
				Declare @entityPropertyId int
		
				DECLARE EntityProperty_CURSOR CURSOR 
				  LOCAL STATIC READ_ONLY FORWARD_ONLY
				FOR 
				SELECT DISTINCT Id
				FROM AmoebaDB.dbo.EntityProperties where entityid = @entityId 

				OPEN EntityProperty_CURSOR
				FETCH NEXT FROM EntityProperty_CURSOR INTO @entityPropertyId
				WHILE @@FETCH_STATUS = 0
				BEGIN 
					PRINT @entityPropertyId

					Declare @DataType varchar(50)
					Set @DataType = (SELECT distinct  DataTypes.Name
										FROM            AmoebaDB.dbo.EntityProperties INNER JOIN
																 AmoebaDB.dbo.DataProperties ON EntityProperties.Id = DataProperties.EntityPropertyId INNER JOIN
																 AmoebaDB.dbo.DataTypes ON DataProperties.DataTypeId = DataTypes.Id
										WHERE        (EntityProperties.Id = @entityPropertyId))
					Declare @propertyName varchar(50)
					set @propertyName = (select PropertyName from AmoebaDB.dbo.EntityProperties where id = @entityPropertyId)
					--Do something with Id here
					Declare @Sql nvarchar(1000)
					declare @param1 nvarchar(1000) 

					if @DataType = 'Byte[]'
						Begin
							select @Sql = N'select @val = ' + @propertyName + N' from ' + @entityName + N' Where Id = ' + cast(@rowId as varchar(50))
							PRINT  'Sql = ' +@Sql
							set @param1 = '@val varbinary(Max) OUTPUT'
							Declare @BinaryValue varbinary(Max)
					
							execute sp_executesql @sql, @param1, @BinaryValue Output
							--PRINT 'Value = ' + @BinaryValue

							insert into AmoebaDB.dbo.TestValues(RowId, EntityPropertyId, [Value]) Values (@rowId, @entityPropertyId,  @BinaryValue)

						end
					else
						begin
							select @Sql = N'select @val = cast(' + @propertyName + N' as VarChar(Max)) from ' + @entityName + N' Where Id = ' + cast(@rowId as varchar(50))
							PRINT  'Sql = ' +@Sql
							set @param1 = '@val varchar(Max) OUTPUT'
							Declare @value nvarchar(Max)
					
							execute sp_executesql @sql, @param1, @value Output
							PRINT 'Value = ' + @value

							insert into AmoebaDB.dbo.TestValues(RowId, EntityPropertyId, [Value]) Values (@rowId, @entityPropertyId,  @value)
						End										
					
					FETCH NEXT FROM EntityProperty_CURSOR INTO @entityPropertyId
				END
				CLOSE EntityProperty_CURSOR
				DEALLOCATE EntityProperty_CURSOR


			FETCH NEXT FROM Data_CURSOR INTO @rowId
		END
		CLOSE Data_CURSOR
		DEALLOCATE Data_CURSOR
	
    FETCH NEXT FROM Entity_CURSOR INTO @entityId
END
CLOSE Entity_CURSOR
DEALLOCATE Entity_CURSOR


select * from AmoebaDB.dbo.testvalues