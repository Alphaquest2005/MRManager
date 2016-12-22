
declare @AppName varchar(50)
set @AppName = 'Amoeba'

declare @appId int
set @appId = (select Id from AmoebaDB.dbo.Applications where Name = @AppName)
select @appId 

DELETE FROM AmoebaDB.dbo.EntityRelationships
FROM            AmoebaDB.dbo.Entities INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON Entities.Id = EntityProperties.EntityId INNER JOIN
                         AmoebaDB.dbo.EntityRelationships ON EntityProperties.Id = EntityRelationships.ChildEntityPropertyId
WHERE        (Entities.Id in (select distinct Id from AmoebaDB.dbo.EntitiesInfo where ApplicationId = @appId))


delete from AmoebaDB.dbo.Entities where id in (select distinct Id from AmoebaDB.dbo.EntitiesInfo where ApplicationId = @appId)

delete from AmoebaDB.dbo.ApplicationEntities where ApplicationId = @appId

INSERT INTO AmoebaDB.dbo.Entities
                         (Name, EntitySetName, schemaname)
SELECT    TABLE_NAME, TABLE_NAME AS Expr1, TABLE_SCHEMA 
FROM            INFORMATION_SCHEMA.TABLES
WHERE        (TABLE_Type = N'BASE TABLE' and TABLE_NAME ='ApplicationSettings')

INSERT INTO AmoebaDB.dbo.Entities
                         (Name, EntitySetName, schemaname)
SELECT    TABLE_NAME, TABLE_NAME AS Expr1, TABLE_SCHEMA --top 60
FROM            INFORMATION_SCHEMA.TABLES
WHERE        (TABLE_Type = N'BASE TABLE' and TABLE_NAME not in ('ApplicationSettings', 'sysdiagrams', '__EFMigrationsHistory'))
order by TABLE_NAME



insert into AmoebaDB.dbo.ApplicationEntities (ApplicationId, EntityId)
select @appId, Id from AmoebaDB.dbo.Entities where id not in (select distinct EntityId from AmoebaDB.dbo.ApplicationEntities)

UPDATE       AmoebaDB.dbo.Entities
SET                Name = 'ApplicationSetting'
FROM            AmoebaDB.dbo.ApplicationEntities INNER JOIN
                         AmoebaDB.dbo.Entities ON AmoebaDB.dbo.ApplicationEntities.EntityId = AmoebaDB.dbo.Entities.Id
WHERE        (AmoebaDB.dbo.ApplicationEntities.ApplicationId = @appId) AND (AmoebaDB.dbo.Entities.EntitySetName = 'ApplicationSettings')

select * from AmoebaDB.dbo.EntitiesInfo where applicationid = @appId 

INSERT INTO AmoebaDB.dbo.EntityProperties
                         (EntityId, PropertyName)
SELECT        AmoebaDB.dbo.EntitiesInfo.Id, INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME
FROM            INFORMATION_SCHEMA.COLUMNS INNER JOIN
                         AmoebaDB.dbo.EntitiesInfo ON INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = AmoebaDB.dbo.EntitiesInfo.EntitySetName
WHERE        (AmoebaDB.dbo.EntitiesInfo.ApplicationId = @appId)

INSERT INTO AmoebaDB.dbo.DataProperties
                         (entitypropertyId, DataTypeId, MaxLength, ModelTypeId)
SELECT        AmoebaDB.dbo.EntityProperties.Id, AmoebaDB.dbo.DataTypes.Id AS DataTypeId, ISNULL(INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, 0) AS MaxLength, 
                         ModelType.Id AS modeltypeid
FROM            AmoebaDB.dbo.Entities INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.EntityProperties.EntityId INNER JOIN
                         INFORMATION_SCHEMA.COLUMNS INNER JOIN
                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.COLUMN_NAME INNER JOIN
                         AmoebaDB.dbo.DataTypes ON INFORMATION_SCHEMA.COLUMNS.DATA_TYPE = AmoebaDB.dbo.DataTypes.Name ON 
                         AmoebaDB.dbo.Entities.EntitySetName = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AND AmoebaDB.dbo.EntityProperties.PropertyName = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME AND 
                         AmoebaDB.dbo.Entities.EntitySetName = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.TABLE_NAME INNER JOIN
                         AmoebaDB.dbo.ApplicationEntities ON AmoebaDB.dbo.EntityProperties.EntityId = AmoebaDB.dbo.ApplicationEntities.EntityId CROSS JOIN
                             (SELECT        Id
                               FROM            AmoebaDB.dbo.ModelTypes
                               WHERE        (Name = 'EntityId')) AS ModelType
WHERE        (INFORMATION_SCHEMA.KEY_COLUMN_USAGE.CONSTRAINT_NAME LIKE N'PK%') AND (AmoebaDB.dbo.ApplicationEntities.ApplicationId = @appId)

---------------------------------------------insert primary key options----------------------------------------------------

INSERT INTO AmoebaDB.dbo.PrimaryKeyOptions
                         (Id, IsCalculated)
SELECT        AmoebaDB.dbo.DataProperties.Id, KeyData.is_identity
FROM            AmoebaDB.dbo.ApplicationEntities INNER JOIN
                         AmoebaDB.dbo.Entities INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.EntityProperties.EntityId ON AmoebaDB.dbo.ApplicationEntities.EntityId = AmoebaDB.dbo.EntityProperties.EntityId INNER JOIN
                         AmoebaDB.dbo.DataProperties ON AmoebaDB.dbo.EntityProperties.Id = AmoebaDB.dbo.DataProperties.EntityPropertyId INNER JOIN
                             (SELECT        OBJECT_NAME(i.object_id) AS tableName, i.name AS indexName, c.name AS columnName, c.is_identity, idc.seed_value, idc.increment_value, idc.last_value
                               FROM            sys.indexes AS i INNER JOIN
                                                         sys.index_columns AS ic ON ic.object_id = i.object_id AND ic.index_id = i.index_id INNER JOIN
                                                         sys.columns AS c ON c.object_id = ic.object_id AND c.column_id = ic.column_id LEFT OUTER JOIN
                                                         sys.identity_columns AS idc ON idc.object_id = c.object_id AND idc.column_id = c.column_id
                               WHERE        (i.is_primary_key = 1)) AS KeyData ON AmoebaDB.dbo.Entities.EntitySetName = KeyData.tableName AND AmoebaDB.dbo.EntityProperties.PropertyName = KeyData.columnName 
WHERE        (AmoebaDB.dbo.ApplicationEntities.ApplicationId = @appId)



-------------------------- Do Foreign Keys--------------------------------------------

INSERT INTO AmoebaDB.dbo.DataProperties
                         (entityPropertyId, DataTypeId, MaxLength, ModelTypeId)
SELECT        AmoebaDB.dbo.EntityProperties.Id, AmoebaDB.dbo.DataTypes.Id AS DataTypeId, ISNULL(INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, 0) AS MaxLength, ModelType.Id AS modeltypeid
FROM            INFORMATION_SCHEMA.KEY_COLUMN_USAGE INNER JOIN
                         INFORMATION_SCHEMA.COLUMNS INNER JOIN
                         AmoebaDB.dbo.DataTypes ON INFORMATION_SCHEMA.COLUMNS.DATA_TYPE = AmoebaDB.dbo.DataTypes.Name INNER JOIN
                         AmoebaDB.dbo.Entities INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.EntityProperties.EntityId ON INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = AmoebaDB.dbo.Entities.EntitySetName AND 
                         INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = AmoebaDB.dbo.EntityProperties.PropertyName ON 
                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE.TABLE_NAME = AmoebaDB.dbo.Entities.EntitySetName AND 
                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE.COLUMN_NAME = INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME AND 
                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE.COLUMN_NAME = AmoebaDB.dbo.EntityProperties.PropertyName AND 
                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE.TABLE_NAME = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME INNER JOIN
                         AmoebaDB.dbo.ApplicationEntities ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.ApplicationEntities.EntityId CROSS JOIN
                             (SELECT        Id
                               FROM            AmoebaDB.dbo.ModelTypes
                               WHERE        (Name = 'ForeignKey')) AS ModelType
WHERE       (INFORMATION_SCHEMA.KEY_COLUMN_USAGE.CONSTRAINT_NAME LIKE N'FK%') AND  (AmoebaDB.dbo.ApplicationEntities.ApplicationId = @appId)
			/**and AmoebaDB.dbo.EntityProperties.Id not in (select EntityPropertyId from DataProperties)**/
---------------------------------------------- enter Entity Names -----------------------------------------


INSERT INTO AmoebaDB.dbo.DataProperties
                         (entityPropertyId, DataTypeId, MaxLength, ModelTypeId)
SELECT        AmoebaDB.dbo.EntityProperties.Id, AmoebaDB.dbo.DataTypes.Id AS DataTypeId, ISNULL(INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, 0) AS MaxLength, 
                         ModelType.Id AS modeltypeid
FROM            AmoebaDB.dbo.DataTypes INNER JOIN
                         INFORMATION_SCHEMA.COLUMNS INNER JOIN
                         AmoebaDB.dbo.Entities INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.EntityProperties.EntityId ON INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = AmoebaDB.dbo.Entities.EntitySetName AND 
                         INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = AmoebaDB.dbo.EntityProperties.PropertyName INNER JOIN
                         AmoebaDB.dbo.ApplicationEntities ON AmoebaDB.dbo.EntityProperties.EntityId = AmoebaDB.dbo.ApplicationEntities.EntityId ON 
                         AmoebaDB.dbo.DataTypes.DBType = INFORMATION_SCHEMA.COLUMNS.DATA_TYPE LEFT OUTER JOIN
                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE ON AmoebaDB.dbo.Entities.EntitySetName = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.TABLE_NAME AND 
                         INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.COLUMN_NAME CROSS JOIN
                             (SELECT        Id
                               FROM            AmoebaDB.dbo.ModelTypes
                               WHERE        (Name = 'EntityName')) AS ModelType
WHERE        (INFORMATION_SCHEMA.KEY_COLUMN_USAGE.CONSTRAINT_NAME IS NULL) AND (AmoebaDB.dbo.ApplicationEntities.ApplicationId = @appId) AND 
                         (INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = N'Name')

--------------------------------- Do Attributes -------------------------------------------------------------

INSERT INTO AmoebaDB.dbo.DataProperties
                         (entityPropertyId, DataTypeId, MaxLength, ModelTypeId)
SELECT        AmoebaDB.dbo.EntityProperties.Id, AmoebaDB.dbo.DataTypes.Id AS DataTypeId, ISNULL(INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, 0) AS MaxLength, 
                         ModelType.Id AS modeltypeid
FROM            AmoebaDB.dbo.DataTypes INNER JOIN
                         INFORMATION_SCHEMA.COLUMNS INNER JOIN
                         AmoebaDB.dbo.Entities INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.EntityProperties.EntityId ON INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = AmoebaDB.dbo.Entities.EntitySetName AND 
                         INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = AmoebaDB.dbo.EntityProperties.PropertyName INNER JOIN
                         AmoebaDB.dbo.ApplicationEntities ON AmoebaDB.dbo.EntityProperties.EntityId = AmoebaDB.dbo.ApplicationEntities.EntityId ON 
                         AmoebaDB.dbo.DataTypes.DBType = INFORMATION_SCHEMA.COLUMNS.DATA_TYPE LEFT OUTER JOIN
                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE ON AmoebaDB.dbo.Entities.EntitySetName = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.TABLE_NAME AND 
                         INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.COLUMN_NAME CROSS JOIN
                             (SELECT        Id
                               FROM            AmoebaDB.dbo.ModelTypes
                               WHERE        (Name = 'Attribute')) AS ModelType
WHERE        (INFORMATION_SCHEMA.KEY_COLUMN_USAGE.CONSTRAINT_NAME IS NULL) AND (AmoebaDB.dbo.ApplicationEntities.ApplicationId = @appId) AND 
                         (INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME <> N'Name')

------------------------------------- use Properties ending in Name as entityname --------------------------------------

INSERT INTO AmoebaDB.dbo.DataProperties
                         (entityPropertyId, DataTypeId, MaxLength, ModelTypeId)
SELECT        AmoebaDB.dbo.EntityProperties.Id, AmoebaDB.dbo.DataTypes.Id AS DataTypeId, ISNULL(INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, 0) AS MaxLength, 
                         ModelType.Id AS modeltypeid
FROM            AmoebaDB.dbo.DataTypes INNER JOIN
                         INFORMATION_SCHEMA.COLUMNS INNER JOIN
                         AmoebaDB.dbo.Entities INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.EntityProperties.EntityId ON INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = AmoebaDB.dbo.Entities.EntitySetName AND 
                         INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = AmoebaDB.dbo.EntityProperties.PropertyName INNER JOIN
                         AmoebaDB.dbo.ApplicationEntities ON AmoebaDB.dbo.EntityProperties.EntityId = AmoebaDB.dbo.ApplicationEntities.EntityId ON 
                         AmoebaDB.dbo.DataTypes.DBType = INFORMATION_SCHEMA.COLUMNS.DATA_TYPE LEFT OUTER JOIN
                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE ON AmoebaDB.dbo.Entities.EntitySetName = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.TABLE_NAME AND 
                         INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = INFORMATION_SCHEMA.KEY_COLUMN_USAGE.COLUMN_NAME CROSS JOIN
                             (SELECT        Id
                               FROM            AmoebaDB.dbo.ModelTypes
                               WHERE        (Name = 'EntityName')) AS ModelType
WHERE        (INFORMATION_SCHEMA.KEY_COLUMN_USAGE.CONSTRAINT_NAME IS NULL) AND (AmoebaDB.dbo.ApplicationEntities.ApplicationId = @appId) AND 
                         (INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME <> N'Name' and INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME like N'%Name')
						 and AmoebaDB.dbo.EntityProperties.entityId not in (SELECT        Entities.Id
																				FROM            AmoebaDB.dbo.Entities INNER JOIN
																										 AmoebaDB.dbo.EntityProperties ON Entities.Id = EntityProperties.EntityId INNER JOIN
																										 AmoebaDB.dbo.DataProperties ON EntityProperties.Id = DataProperties.EntityPropertyId
																				WHERE        (DataProperties.ModelTypeId = 2)) 

--------------------------------------- do relationships --------------------------------------

INSERT INTO AmoebaDB.dbo.EntityRelationships
                         (ParentEntityPropertyId,ChildEntityPropertyId , RelationshipTypeId)
SELECT        ParentProperties.Id AS ParentPropertyId, ChildProperties.Id AS ChildPropertyId, 2 AS RelationshipTypeId
FROM            AmoebaDB.dbo.Entities AS ParentEntities INNER JOIN
                         AmoebaDB.dbo.EntityProperties AS ParentProperties ON ParentEntities.Id = ParentProperties.EntityId INNER JOIN
                         AmoebaDB.dbo.ApplicationEntities ON ParentEntities.Id = AmoebaDB.dbo.ApplicationEntities.EntityId INNER JOIN
                         AmoebaDB.dbo.Entities AS ChildEntities INNER JOIN
                             (SELECT        KEY_COLUMN_USAGE_1.TABLE_NAME AS ParentTable, KEY_COLUMN_USAGE_1.COLUMN_NAME AS ParentColumn, 
                                                         INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.TABLE_NAME AS ChildTable, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME AS ChildColumn
                               FROM            INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE INNER JOIN
                                                         INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS ON 
                                                         INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.CONSTRAINT_NAME INNER JOIN
                                                         INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KEY_COLUMN_USAGE_1 ON 
                                                         INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS.UNIQUE_CONSTRAINT_NAME = KEY_COLUMN_USAGE_1.CONSTRAINT_NAME INNER JOIN
                                                         INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ON 
                                                         INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME) AS RelView ON 
                         ChildEntities.EntitySetName = RelView.ChildTable INNER JOIN
                         AmoebaDB.dbo.EntityProperties AS ChildProperties ON RelView.ChildColumn = ChildProperties.PropertyName AND ChildEntities.Id = ChildProperties.EntityId INNER JOIN
                         AmoebaDB.dbo.ApplicationEntities AS ApplicationEntities_1 ON ChildEntities.Id = ApplicationEntities_1.EntityId ON ParentProperties.PropertyName = RelView.ParentColumn AND 
                         ParentEntities.EntitySetName = RelView.ParentTable
WHERE        (AmoebaDB.dbo.ApplicationEntities.ApplicationId = @appId) AND (ApplicationEntities_1.ApplicationId = @appId)

----------------------------------Load Views--------------------------------------------------------

insert into AmoebaDB.dbo.EntityView(EntityId, Name)
select Es.EntitySetId, Es.[View]
from (SELECT        [View]
FROM            (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                          FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                          ORDER BY [View]) AS views
GROUP BY [View]) as E
cross apply
(SELECT DISTINCT 
                         TOP (1) views.[View], AmoebaDB.dbo.EntityRank.EntityId, MAX(AmoebaDB.dbo.EntityRank.Rank) AS Expr1, AmoebaDB.dbo.EntityRank.Name, source.TABLE_TYPE, AmoebaDB.dbo.EntitiesInfo.Name AS Entity, 
                         AmoebaDB.dbo.EntitiesInfo.Id AS EntitySetId
FROM            (SELECT        TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
                          FROM            INFORMATION_SCHEMA.TABLES) AS source INNER JOIN
                             (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table]
                               FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                               GROUP BY VIEW_NAME, TABLE_SCHEMA, TABLE_NAME
                               ORDER BY [View]) AS views ON source.TABLE_SCHEMA = views.TABLE_SCHEMA AND source.TABLE_NAME = views.[Table] INNER JOIN
                         AmoebaDB.dbo.EntitiesInfo ON views.TABLE_SCHEMA = AmoebaDB.dbo.EntitiesInfo.SchemaName AND views.[Table] = AmoebaDB.dbo.EntitiesInfo.EntitySetName AND 
                         source.TABLE_SCHEMA = AmoebaDB.dbo.EntitiesInfo.SchemaName AND source.TABLE_NAME = AmoebaDB.dbo.EntitiesInfo.EntitySetName INNER JOIN
                         AmoebaDB.dbo.EntityRank ON AmoebaDB.dbo.EntitiesInfo.Id = AmoebaDB.dbo.EntityRank.EntityId
WHERE        (views.[View] = E.[view]) AND (AmoebaDB.dbo.EntitiesInfo.ApplicationId = @appId)
GROUP BY views.[View], AmoebaDB.dbo.EntitiesInfo.Id, AmoebaDB.dbo.EntityRank.EntityId, AmoebaDB.dbo.EntityRank.Name, source.TABLE_TYPE, AmoebaDB.dbo.EntitiesInfo.Name
ORDER BY views.[View], source.TABLE_TYPE, Expr1 DESC) as Es

------------------------------------------------Load Entity View Properties-----------------------------------

insert into AmoebaDB.dbo.EntityViewProperties
	(EntityViewId,Name)
select V.EntityViewId , V.EntityViewPropertyName
from 
(SELECT DISTINCT 
                         views.[View], views.[Table], views.[Column], AmoebaDB.dbo.EntitiesInfo.EntitySetName, AmoebaDB.dbo.EntityProperties.PropertyName, AmoebaDB.dbo.EntityProperties.Id AS EntityPropertyId, 
                         AmoebaDB.dbo.EntitiesInfo.Id AS EntitySetId, AmoebaDB.dbo.EntityView.Id AS EntityViewId, AmoebaDB.dbo.EntityView.Name, CASE WHEN PropertyName LIKE '%Name%' OR
                         PropertyName = 'Value' THEN AmoebaDB.dbo.Pluralize(views.[Table]) ELSE PropertyName END AS EntityViewPropertyName
FROM            (SELECT        TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
                          FROM            INFORMATION_SCHEMA.TABLES) AS source INNER JOIN
                             (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                               FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                               ORDER BY [View]) AS views ON source.TABLE_SCHEMA = views.TABLE_SCHEMA AND source.TABLE_NAME = views.[Table] INNER JOIN
                         AmoebaDB.dbo.EntitiesInfo ON source.TABLE_SCHEMA = AmoebaDB.dbo.EntitiesInfo.SchemaName AND source.TABLE_NAME = AmoebaDB.dbo.EntitiesInfo.EntitySetName AND 
                         views.TABLE_SCHEMA = AmoebaDB.dbo.EntitiesInfo.SchemaName AND views.[Table] = AmoebaDB.dbo.EntitiesInfo.EntitySetName INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.EntitiesInfo.Id = AmoebaDB.dbo.EntityProperties.EntityId AND views.[Column] = AmoebaDB.dbo.EntityProperties.PropertyName INNER JOIN
                         AmoebaDB.dbo.DataProperties ON AmoebaDB.dbo.EntityProperties.Id = AmoebaDB.dbo.DataProperties.EntityPropertyId INNER JOIN
                         AmoebaDB.dbo.ModelTypes ON AmoebaDB.dbo.DataProperties.ModelTypeId = AmoebaDB.dbo.ModelTypes.Id INNER JOIN
                         AmoebaDB.dbo.EntityView ON views.[View] = AmoebaDB.dbo.EntityView.Name
WHERE        (AmoebaDB.dbo.ModelTypes.Name <> N'EntityId') AND (AmoebaDB.dbo.ModelTypes.Name <> N'ForeignKey') AND (source.TABLE_TYPE = 'Base Table') AND (AmoebaDB.dbo.EntitiesInfo.ApplicationId = @appId)) as V
----------------------------------------------insert Entity View Entity Property-----------------------------
insert into AmoebaDB.dbo.EntityViewEntityProperties
	(Id,EntityPropertyId)
select V.RealEntityViewPropertyId , V.EntityPropertyId
from 
(SELECT DISTINCT 
                         views.[View], views.[Table], views.[Column], AmoebaDB.dbo.EntitiesInfo.EntitySetName, AmoebaDB.dbo.EntityProperties.PropertyName, AmoebaDB.dbo.EntityProperties.Id AS EntityPropertyId, 
                         AmoebaDB.dbo.EntitiesInfo.Id AS EntitySetId, AmoebaDB.dbo.EntityView.Id AS EntityViewId, AmoebaDB.dbo.EntityView.Name, CASE WHEN PropertyName LIKE '%Name%' OR
                         PropertyName = 'Value' THEN AmoebaDB.dbo.Pluralize(views.[Table]) ELSE PropertyName END AS EntityViewPropertyName, AmoebaDB.dbo.EntityViewProperties.Name AS RealEntityViewPropertyName, 
                         AmoebaDB.dbo.EntityViewProperties.Id AS RealEntityViewPropertyId, AmoebaDB.dbo.EntitiesInfo.ApplicationId
FROM            (SELECT        TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
                          FROM            INFORMATION_SCHEMA.TABLES) AS source INNER JOIN
                             (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                               FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                               ORDER BY [View]) AS views ON source.TABLE_SCHEMA = views.TABLE_SCHEMA AND source.TABLE_NAME = views.[Table] INNER JOIN
                         AmoebaDB.dbo.EntitiesInfo ON source.TABLE_SCHEMA = AmoebaDB.dbo.EntitiesInfo.SchemaName AND source.TABLE_NAME = AmoebaDB.dbo.EntitiesInfo.EntitySetName AND 
                         views.TABLE_SCHEMA = AmoebaDB.dbo.EntitiesInfo.SchemaName AND views.[Table] = AmoebaDB.dbo.EntitiesInfo.EntitySetName INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.EntitiesInfo.Id = AmoebaDB.dbo.EntityProperties.EntityId AND views.[Column] = AmoebaDB.dbo.EntityProperties.PropertyName INNER JOIN
                         AmoebaDB.dbo.DataProperties ON AmoebaDB.dbo.EntityProperties.Id = AmoebaDB.dbo.DataProperties.EntityPropertyId INNER JOIN
                         AmoebaDB.dbo.ModelTypes ON AmoebaDB.dbo.DataProperties.ModelTypeId = AmoebaDB.dbo.ModelTypes.Id INNER JOIN
                         AmoebaDB.dbo.EntityView ON views.[View] = AmoebaDB.dbo.EntityView.Name INNER JOIN
                         AmoebaDB.dbo.EntityViewProperties ON AmoebaDB.dbo.EntityView.Id = AmoebaDB.dbo.EntityViewProperties.EntityViewId AND CASE WHEN PropertyName LIKE '%Name%' OR 
                         PropertyName = 'Value' THEN AmoebaDB.dbo.Pluralize(views.[Table]) ELSE AmoebaDB.dbo.EntityProperties.PropertyName END = AmoebaDB.dbo.EntityViewProperties.Name
WHERE        (AmoebaDB.dbo.ModelTypes.Name <> N'EntityId') AND (AmoebaDB.dbo.ModelTypes.Name <> N'ForeignKey') AND (source.TABLE_TYPE = 'Base Table') AND (AmoebaDB.dbo.EntitiesInfo.ApplicationId = @appId)) as V



----------------------------------------------insert Entity View Entity Property-----------------------------

insert into AmoebaDB.dbo.EntityViewProperties
	(EntityViewId, Name)
select V.RequestingEntityViewId , V.[Column]
from 
(SELECT DISTINCT views.[View], views.[Table], views.[Column], AmoebaDB.dbo.EntityViewsInfo.EntityViewId AS RequestingEntityViewId, ExistingEVProperties.EntityViewPropertyId AS providingEntityViewPropertyId
FROM            (SELECT        TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
                          FROM            INFORMATION_SCHEMA.TABLES) AS source INNER JOIN
                             (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                               FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                               ORDER BY [View]) AS views ON source.TABLE_SCHEMA = views.TABLE_SCHEMA AND source.TABLE_NAME = views.[Table] INNER JOIN
                         AmoebaDB.dbo.EntityViewsInfo ON views.[View] = AmoebaDB.dbo.EntityViewsInfo.EntityViewName INNER JOIN
                             (SELECT        [View], ViewProperty, EntityViewPropertyId
                               FROM            AmoebaDB.dbo.EntityViewPropertiesInfo) AS ExistingEVProperties ON views.[Table] = ExistingEVProperties.[View] AND views.[Column] = ExistingEVProperties.ViewProperty
WHERE        (source.TABLE_TYPE = 'View') AND (AmoebaDB.dbo.EntityViewsInfo.ApplicationId = 2)) as V

-----------------------------------------------insert into entity view view property
insert into AmoebaDB.dbo.EntityViewViewProperties
	(Id, EntityViewPropertyId)
SELECT        AmoebaDB.dbo.EntityViewProperties.Id, V.providingEntityViewPropertyId
FROM            (SELECT DISTINCT 
                                                    views.[View], views.[Table], views.[Column], AmoebaDB.dbo.EntityViewsInfo.EntityViewId AS RequestingEntityViewId, 
                                                    ExistingEVProperties.EntityViewPropertyId AS providingEntityViewPropertyId
                          FROM            (SELECT        TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
                                                    FROM            INFORMATION_SCHEMA.TABLES) AS source INNER JOIN
                                                        (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                                                          FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                                                          ORDER BY [View]) AS views ON source.TABLE_SCHEMA = views.TABLE_SCHEMA AND source.TABLE_NAME = views.[Table] INNER JOIN
                                                    AmoebaDB.dbo.EntityViewsInfo ON views.[View] = AmoebaDB.dbo.EntityViewsInfo.EntityViewName INNER JOIN
                                                        (SELECT        [View], ViewProperty, EntityViewPropertyId
                                                          FROM            AmoebaDB.dbo.EntityViewPropertiesInfo) AS ExistingEVProperties ON views.[Table] = ExistingEVProperties.[View] AND views.[Column] = ExistingEVProperties.ViewProperty
                          WHERE        (source.TABLE_TYPE = 'View') AND (AmoebaDB.dbo.EntityViewsInfo.ApplicationId = @appId)) AS V INNER JOIN
                         AmoebaDB.dbo.EntityViewProperties ON V.RequestingEntityViewId = AmoebaDB.dbo.EntityViewProperties.EntityViewId AND V.[Column] = AmoebaDB.dbo.EntityViewProperties.Name
