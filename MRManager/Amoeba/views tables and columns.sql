SELECT        views.[View], views.[Table], views.[Column], source.TABLE_TYPE
FROM            (SELECT        TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
                          FROM            INFORMATION_SCHEMA.TABLES) AS source INNER JOIN
                             (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                               FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                               ORDER BY [View]) AS views ON source.TABLE_SCHEMA = views.TABLE_SCHEMA AND source.TABLE_NAME = views.[Table]
ORDER BY views.[View]

SELECT        [View]
FROM            (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                          FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                          ORDER BY [View]) AS views
GROUP BY [View]
ORDER BY [View]

-------------------Get Views----------------------------
select Es.EntitySetId, Es.[View], es.entity
from (SELECT        [View]
FROM            (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                          FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                          ORDER BY [View]) AS views
GROUP BY [View]) as E
cross apply
(SELECT DISTINCT 
                         TOP (1) views.[View], AmoebaDB.dbo.Entities.Id AS EntitySetId, AmoebaDB.dbo.EntityRank.EntityId, MAX(AmoebaDB.dbo.EntityRank.Rank) AS Expr1, AmoebaDB.dbo.EntityRank.Name, source.TABLE_TYPE, 
                         AmoebaDB.dbo.Entities.Name AS Entity
FROM            (SELECT        TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
                          FROM            INFORMATION_SCHEMA.TABLES) AS source INNER JOIN
                             (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table]
                               FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                               GROUP BY VIEW_NAME, TABLE_SCHEMA, TABLE_NAME
                               ORDER BY [View]) AS views ON source.TABLE_SCHEMA = views.TABLE_SCHEMA AND source.TABLE_NAME = views.[Table] INNER JOIN
                         AmoebaDB.dbo.Entities ON views.TABLE_SCHEMA = AmoebaDB.dbo.Entities.SchemaName AND views.[Table] = AmoebaDB.dbo.Entities.EntitySetName AND 
                         source.TABLE_SCHEMA = AmoebaDB.dbo.Entities.SchemaName AND source.TABLE_NAME = AmoebaDB.dbo.Entities.EntitySetName INNER JOIN
                         AmoebaDB.dbo.EntityRank ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.EntityRank.EntityId
WHERE        (views.[View] = E.[view])
GROUP BY views.[View], AmoebaDB.dbo.Entities.Id, AmoebaDB.dbo.EntityRank.EntityId, AmoebaDB.dbo.EntityRank.Name, source.TABLE_TYPE, AmoebaDB.dbo.Entities.Name
ORDER BY views.[View], source.TABLE_TYPE, Expr1 DESC) as Es

-------------------------------Get View Properties--------------------------------
SELECT DISTINCT views.[View], views.[Table], views.[Column], AmoebaDB.dbo.Entities.EntitySetName, AmoebaDB.dbo.EntityProperties.PropertyName, AmoebaDB.dbo.EntityProperties.Id AS EntityPropertyId
FROM            (SELECT        TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
                          FROM            INFORMATION_SCHEMA.TABLES) AS source INNER JOIN
                             (SELECT        TOP (100) PERCENT VIEW_NAME AS [View], TABLE_SCHEMA, TABLE_NAME AS [Table], COLUMN_NAME AS [Column]
                               FROM            INFORMATION_SCHEMA.VIEW_COLUMN_USAGE
                               ORDER BY [View]) AS views ON source.TABLE_SCHEMA = views.TABLE_SCHEMA AND source.TABLE_NAME = views.[Table] INNER JOIN
                         AmoebaDB.dbo.Entities ON source.TABLE_SCHEMA = AmoebaDB.dbo.Entities.SchemaName AND source.TABLE_NAME = AmoebaDB.dbo.Entities.EntitySetName AND 
                         views.TABLE_SCHEMA = AmoebaDB.dbo.Entities.SchemaName AND views.[Table] = AmoebaDB.dbo.Entities.EntitySetName INNER JOIN
                         AmoebaDB.dbo.EntityProperties ON AmoebaDB.dbo.Entities.Id = AmoebaDB.dbo.EntityProperties.EntityId AND views.[Column] = AmoebaDB.dbo.EntityProperties.PropertyName INNER JOIN
                         AmoebaDB.dbo.DataProperties ON AmoebaDB.dbo.EntityProperties.Id = AmoebaDB.dbo.DataProperties.EntityPropertyId INNER JOIN
                         AmoebaDB.dbo.ModelTypes ON AmoebaDB.dbo.DataProperties.ModelTypeId = AmoebaDB.dbo.ModelTypes.Id
WHERE        (AmoebaDB.dbo.ModelTypes.Name <> N'EntityId') AND (AmoebaDB.dbo.ModelTypes.Name <> N'ForeignKey') AND (source.TABLE_TYPE = 'Base Table')
ORDER BY views.[View]


