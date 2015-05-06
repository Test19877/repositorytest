-- USE [publisherdatabase]
USE [DatawarehouseOds]
GO

-- checking fields allow_anonymous, immediate_sync, both should be false (0)
-- for adding certain number of articles
sp_helppublication


--Run on your publisher database
EXEC sp_changepublication 
@publication = 'ODS', 
@property = 'allow_anonymous' , 
@value = 'false' 
GO 

EXEC sp_changepublication 
@publication = 'ODS', 
@property = 'immediate_sync' , 
@value = 'false' 
GO 

-- after this add articles in wizard and start Snapshot agent

