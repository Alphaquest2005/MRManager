SET IDENTITY_INSERT applications ON; 

insert into Applications(id, ApplicationName, ApplicationFolder) values (0, 'Unspecified Application', 'Unspecified Folder')

SET IDENTITY_INSERT applications off; 

SET IDENTITY_INSERT ModelTYpes ON; 

insert into ModelTypes(id, Name) values (0, 'Unspecified ModelType')

SET IDENTITY_INSERT ModelTYpes off; 

SET IDENTITY_INSERT DataTYpes ON; 
	insert into DataTypes(id, Name) values (0, 'Unspecified DataType')
SET IDENTITY_INSERT DataTYpes off; 

SET IDENTITY_INSERT RelationshipTYpes ON; 
	insert into RelationshipTypes(id, Name) values (0, 'Unspecified RelationshipType')
SET IDENTITY_INSERT RelationshipTYpes off; 

SET IDENTITY_INSERT EntityProperties ON; 
	insert into EntityProperties(id,EntityId, PropertyName) values (0,0,'Unspecified PropertyName')
SET IDENTITY_INSERT EntityProperties off; 


insert into DataProperties(id,DataTypeId, ModelTypeId, MaxLength) values (0,0,0,0)

SET IDENTITY_INSERT Projects ON; 
	insert into Projects(id,ProjectName) values (0,'Unspecified Project')
SET IDENTITY_INSERT Projects off; 

SET IDENTITY_INSERT Entities ON; 
	insert into Entities(id,EntityName,EntitySetName) values (0,'Unspecified Entity', 'Unspecified EntitySet')
SET IDENTITY_INSERT Entities off; 

SET IDENTITY_INSERT PatientVisit ON; 
	insert into PatientVisit(id,PatientId,DateOfVisit,DoctorId) values (0,0,'1/1/1900',0)
SET IDENTITY_INSERT PatientVisit off; 

SET IDENTITY_INSERT EntityAttributes ON; 
	insert into EntityAttributes(id, Entity,Attribute, [type]) values (0,'unspecified','unspecified','unspecified')
SET IDENTITY_INSERT EntityAttributes off; 

SET IDENTITY_INSERT Interview.MedicalCategory ON; 
	insert into Interview.MedicalCategory(Id,[Name] ) values (0,'unspecified')
SET IDENTITY_INSERT Interview.MedicalCategory off; 

SET IDENTITY_INSERT Interview.Phase ON; 
	insert into Interview.Phase(Id,[Name], Code ) values (0,'unspecified', 'xxx')
SET IDENTITY_INSERT Interview.Phase off; 

SET IDENTITY_INSERT Interview.Interviews ON; 
	insert into Interview.Interviews(Id,[Name],MedicalCategoryId,PhaseId ) values (0,'unspecified',0,0)
SET IDENTITY_INSERT Interview.Interviews off; 

SET IDENTITY_INSERT Interview.SyntomStatus ON; 
	insert into Interview.SyntomStatus(Id,[Name]) values (0,'unspecified')
SET IDENTITY_INSERT Interview.SyntomStatus off;

SET IDENTITY_INSERT Interview.Syntoms ON; 
	insert into Interview.Syntoms(Id,[Name]) values (0,'unspecified')
SET IDENTITY_INSERT Interview.Syntoms off;

SET IDENTITY_INSERT Interview.SyntomPriority ON; 
	insert into Interview.SyntomPriority(Id,[Name]) values (0,'unspecified')
SET IDENTITY_INSERT Interview.SyntomPriority off;

SET IDENTITY_INSERT Interview.MedicalSystems ON; 
	insert into Interview.MedicalSystems(Id,[Name]) values (0,'unspecified')
SET IDENTITY_INSERT Interview.MedicalSystems off;

SET IDENTITY_INSERT Interview.MedicalSystemInterviews ON; 
	insert into Interview.MedicalSystemInterviews(Id,MedicalSystemId, InterviewId) values (0,0,0)
SET IDENTITY_INSERT Interview.MedicalSystemInterviews off;

SET IDENTITY_INSERT Interview.QuestionResponseTypes ON; 
	insert into Interview.QuestionResponseTypes(Id,[Name]) values (0,'Text')
SET IDENTITY_INSERT Interview.QuestionResponseTypes off;

set identity_insert Media on

INSERT INTO Media(value, MediaTypeId, Id)
 VALUES(
	(SELECT BulkColumn FROM OPENROWSET(BULK 'C:\Prism\Clients\SAMS-MRManager\images\People-Patient-Female-icon.png', SINGLE_BLOB) AS x), 1, 0)

set identity_insert Media off