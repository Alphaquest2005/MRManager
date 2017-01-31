﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DBContext.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Data;
using EF.Entities;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace EF.DBContexts
{
	public partial class MRManagerDBContext
	{
		private static readonly MRManagerDBContext _instance = new MRManagerDBContext();

		public static MRManagerDBContext Instance => _instance;

		static MRManagerDBContext()
		{
			if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
			Instance.Database.EnsureDeleted();
			Instance.Database.EnsureCreated();
			CreateSeedData();
		}

		private static void CreateSeedData()
		{
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Addresses ON
						Insert Into dbo.Addresses (Id,EntryDateTime) Values('3','2016-01-06')
						Insert Into dbo.Addresses (Id,EntryDateTime) Values('2','2016-05-29')
						Insert Into dbo.Addresses (Id,EntryDateTime) Values('1','2016-05-29')
						Insert Into dbo.Addresses (Id,EntryDateTime) Values('0','2016-05-29')
					SET IDENTITY_INSERT dbo.Addresses OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.AddressTypes ON
						Insert Into dbo.AddressTypes (Id,Name) Values('0','Unspecified')
						Insert Into dbo.AddressTypes (Id,Name) Values('4','Home')
					SET IDENTITY_INSERT dbo.AddressTypes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Allergies ON
						Insert Into dbo.Allergies (Id,Name) Values('1','Milk')
						Insert Into dbo.Allergies (Id,Name) Values('0','Unspecified')
						Insert Into dbo.Allergies (Id,Name) Values('2','Asprin')
					SET IDENTITY_INSERT dbo.Allergies OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.CarePlan ON
						Insert Into Interview.CarePlan (Diagnosis,Id,Name) Values('Redness and Itchy Eyes','1','Eye Wash')
					SET IDENTITY_INSERT Interview.CarePlan OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Cities ON
						Insert Into dbo.Cities (Id,Name) Values('0','Unspecified')
						Insert Into dbo.Cities (Id,Name) Values('1','St. Geroge''s')
						Insert Into dbo.Cities (Id,Name) Values('3','New York')
					SET IDENTITY_INSERT dbo.Cities OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Countries ON
						Insert Into dbo.Countries (Id,Name) Values('0','Unspecified')
						Insert Into dbo.Countries (Id,Name) Values('1','Grenada')
						Insert Into dbo.Countries (Id,Name) Values('2','USA')
					SET IDENTITY_INSERT dbo.Countries OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.EntityAttributes ON
						Insert Into dbo.EntityAttributes (Attribute,Entity,Id,Type) Values('Name','Patient','1','string')
						Insert Into dbo.EntityAttributes (Attribute,Entity,Id,Type) Values('BirthDate','Patient','3','date')
						Insert Into dbo.EntityAttributes (Attribute,Entity,Id,Type) Values('Id','Patient','4','string')
						Insert Into dbo.EntityAttributes (Attribute,Entity,Id,Type) Values('unspecified','unspecified','0','unspecified')
					SET IDENTITY_INSERT dbo.EntityAttributes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.Exams ON
						Insert Into Diagnostics.Exams (Id,Describtion,ExamTypeId,Name) Values('1','Regular fetal ultrasound','1','Fetal Wellbeing')
					SET IDENTITY_INSERT Diagnostics.Exams OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.MaritalStatus ON
						Insert Into dbo.MaritalStatus (Name,Id) Values('Divorced','1')
						Insert Into dbo.MaritalStatus (Name,Id) Values('Unspecified','0')
					SET IDENTITY_INSERT dbo.MaritalStatus OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.MediaTypes ON
						Insert Into dbo.MediaTypes (Id,FileExtension,MediaTypeName) Values('2','mov','Video')
						Insert Into dbo.MediaTypes (Id,FileExtension,MediaTypeName) Values('1','jpg,png','Image')
					SET IDENTITY_INSERT dbo.MediaTypes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.MedicalCategory ON
						Insert Into Interview.MedicalCategory (Name,Id) Values('Optical','1')
						Insert Into Interview.MedicalCategory (Name,Id) Values('Personal','3')
						Insert Into Interview.MedicalCategory (Name,Id) Values('General','2')
					SET IDENTITY_INSERT Interview.MedicalCategory OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.MedicalSystems ON
						Insert Into Interview.MedicalSystems (Id,Name) Values('1','Patient')
						Insert Into Interview.MedicalSystems (Id,Name) Values('2','Optical')
					SET IDENTITY_INSERT Interview.MedicalSystems OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Occupations ON
						Insert Into dbo.Occupations (Id,Name) Values('0','Unspecified')
						Insert Into dbo.Occupations (Id,Name) Values('1','Software Developer')
					SET IDENTITY_INSERT dbo.Occupations OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Organisations ON
						Insert Into dbo.Organisations (Id,EntryTimeStamp,Name,VATNumber) Values('2',cast((select Value from AmoebaDB.dbo.TestValues where Id = 68750) as varbinary(max)),'Insight Software','0')
						Insert Into dbo.Organisations (Id,EntryTimeStamp,Name,VATNumber) Values('0',cast((select Value from AmoebaDB.dbo.TestValues where Id = 68746) as varbinary(max)),'Unspecified','0')
						Insert Into dbo.Organisations (Id,EntryTimeStamp,Name,VATNumber) Values('3',cast((select Value from AmoebaDB.dbo.TestValues where Id = 68754) as varbinary(max)),'Spice Inn Hotel','123')
					SET IDENTITY_INSERT dbo.Organisations OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Parishes ON
						Insert Into dbo.Parishes (Name,Id) Values('St. David''s','2')
						Insert Into dbo.Parishes (Name,Id) Values('St. George','1')
						Insert Into dbo.Parishes (Name,Id) Values('Unspecified','0')
					SET IDENTITY_INSERT dbo.Parishes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Patients ON
						Insert Into Interview.Patients (EntryDateTime,Id) Values(cast((select Value from AmoebaDB.dbo.TestValues where Id = 69111) as varbinary(max)),'1')
					SET IDENTITY_INSERT Interview.Patients OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Persons ON
						Insert Into dbo.Persons (Id,EntryDateTine) Values('1001','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('4','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('1004','Jun  5 2016 12:00AM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('3','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('1','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('0','Jun  5 2016  9:18PM')
					SET IDENTITY_INSERT dbo.Persons OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Phase ON
						Insert Into Interview.Phase (Name,Id,Code) Values('Chief Complaint','1','CC')
						Insert Into Interview.Phase (Name,Id,Code) Values('Present Illness','2','PI')
						Insert Into Interview.Phase (Name,Id,Code) Values('Patient History','1002','PH')
						Insert Into Interview.Phase (Name,Id,Code) Values('Patient Personal Info','1003','PPI')
					SET IDENTITY_INSERT Interview.Phase OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PhoneTypes ON
						Insert Into dbo.PhoneTypes (Id,Name) Values('0','Unspecified')
						Insert Into dbo.PhoneTypes (Id,Name) Values('1','Lime')
					SET IDENTITY_INSERT dbo.PhoneTypes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ResultFieldNames ON
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('6','Pregnacy Type')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('7','Number of Fetues')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('1','Indication')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('2','General History')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('3','Past Surgical History')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('4','OB History')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('5','Risk Factors')
					SET IDENTITY_INSERT Diagnostics.ResultFieldNames OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Sex ON
						Insert Into dbo.Sex (Name,Id) Values('Male','1')
						Insert Into dbo.Sex (Name,Id) Values('Unspecified','0')
						Insert Into dbo.Sex (Name,Id) Values('Female','2')
					SET IDENTITY_INSERT dbo.Sex OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.States ON
						Insert Into dbo.States (Name,Id) Values('New York','1')
					SET IDENTITY_INSERT dbo.States OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Syntoms ON
						Insert Into Interview.Syntoms (Id,Name) Values('1','General')
						Insert Into Interview.Syntoms (Id,Name) Values('2','Redness')
					SET IDENTITY_INSERT Interview.Syntoms OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Units ON
						Insert Into dbo.Units (Id,Description,Name,ShortName) Values('1','12 inches make 1 foot','Inches','in.')
						Insert Into dbo.Units (Id,Description,Name,ShortName) Values('2','Beats per minute','Bpm','bpm')
						Insert Into dbo.Units (Id,Description,Name,ShortName) Values('6','Systolic / Diatolic','Systolic/Dia','Sys/Dia')
						Insert Into dbo.Units (Id,Description,Name,ShortName) Values('5','Pounds','Lbs','lbs')
						Insert Into dbo.Units (Id,Description,Name,ShortName) Values('4','Breaths Per Minute','bpm','bpm')
						Insert Into dbo.Units (Id,Description,Name,ShortName) Values('3','Temperature','Fahrenheit','ºF')
					SET IDENTITY_INSERT dbo.Units OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.ZipCodes ON
						Insert Into dbo.ZipCodes (Name,Id) Values('12346','1')
					SET IDENTITY_INSERT dbo.ZipCodes OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressCities (CityId,Id) Values('3','2')
						Insert Into dbo.AddressCities (CityId,Id) Values('0','0')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressCountries (CountryId,Id) Values('2','2')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressZipCodes (Id,ZipCodeId) Values('2','1')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.AddressLines ON
						Insert Into dbo.AddressLines (Name,AddressId,Id) Values('Unspecified','0','0')
						Insert Into dbo.AddressLines (Name,AddressId,Id) Values('Old Westerhall','1','1')
						Insert Into dbo.AddressLines (Name,AddressId,Id) Values('Flat Bush','2','2')
						Insert Into dbo.AddressLines (Name,AddressId,Id) Values('43rd Apt# 5','2','3')
						Insert Into dbo.AddressLines (Name,AddressId,Id) Values('Limes','3','4')
						Insert Into dbo.AddressLines (Name,AddressId,Id) Values('Grand Anse','3','5')
					SET IDENTITY_INSERT dbo.AddressLines OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressStates (Id,StateId) Values('2','1')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressParishes (Id,ParishId) Values('0','0')
						Insert Into dbo.AddressParishes (Id,ParishId) Values('1','2')
						Insert Into dbo.AddressParishes (Id,ParishId) Values('3','1')
");
			//No test data for OrganisationAddress
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonAddresses ON
						Insert Into dbo.PersonAddresses (AddressId,AddressTypeId,Id,PersonId) Values('1','4','1','1')
						Insert Into dbo.PersonAddresses (AddressId,AddressTypeId,Id,PersonId) Values('3','4','2','4')
						Insert Into dbo.PersonAddresses (AddressId,AddressTypeId,Id,PersonId) Values('0','0','0','0')
					SET IDENTITY_INSERT dbo.PersonAddresses OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.PrimaryPersonAddress (PersonAddressesId,Id) Values('1','1')
						Insert Into dbo.PrimaryPersonAddress (PersonAddressesId,Id) Values('0','0')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_Patient (CountryId,DateOfBirth,Id,SexId) Values('0','1783-01-01','0','0')
						Insert Into dbo.Persons_Patient (CountryId,DateOfBirth,Id,SexId) Values('1','1980-05-17','1','1')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PatientAllergies ON
						Insert Into dbo.PatientAllergies (AllergyId,Id,PatientId) Values('1','2','1')
						Insert Into dbo.PatientAllergies (AllergyId,Id,PatientId) Values('2','3','1')
					SET IDENTITY_INSERT dbo.PatientAllergies OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.CarePlanDetails ON
						Insert Into Interview.CarePlanDetails (CarePlanId,Id,Instructions) Values('1','1','A few Eye drops when eyes feeling red or itchy')
					SET IDENTITY_INSERT Interview.CarePlanDetails OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.CarePlanDetailsSuggestedMedication ON
						Insert Into Interview.CarePlanDetailsSuggestedMedication (CarePlanDetailId,Id,ItemId) Values('1','2','2247')
						Insert Into Interview.CarePlanDetailsSuggestedMedication (CarePlanDetailId,Id,ItemId) Values('1','1','2231')
					SET IDENTITY_INSERT Interview.CarePlanDetailsSuggestedMedication OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.ParishCities (CityId,Id) Values('1','1')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonCountryOfResidence ON
						Insert Into dbo.PersonCountryOfResidence (CountryId,Date,Id,PersonId) Values('0','2016-01-01','1','0')
					SET IDENTITY_INSERT dbo.PersonCountryOfResidence OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Interviews ON
						Insert Into Interview.Interviews (Name,Id,MedicalCategoryId,PhaseId) Values('Personal Information','1004','3','1003')
						Insert Into Interview.Interviews (Name,Id,MedicalCategoryId,PhaseId) Values('New Patient Optical','3','1','1')
						Insert Into Interview.Interviews (Name,Id,MedicalCategoryId,PhaseId) Values('Previous Medical Care','1003','2','1002')
					SET IDENTITY_INSERT Interview.Interviews OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Questions ON
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('1','What is Your First Name','27','1004','2')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('0','Are you under a physicians care or have you been during the past 5 years, including hospitalization, and surgery?','26','1003','1')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('0','Is the symptom/problem getting better or worse?','25','3','7')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('0','Have you ever been treated for this complaint?','24','3','6')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('0','How does this problem interfere with your work or other activities? ','23','3','5')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('0','Is this symptom/problem constant or occasional?','22','3','4')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('0','When was the onset?','21','3','3')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('0','Which Eye?','20','3','2')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('3','What is your Birthdate','30','1004','4')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('1','What is Your Last Name','29','1004','3')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('4','Patients Identification Number','31','1004','1')
						Insert Into Interview.Questions (EntityAttributeId,Description,Id,InterviewId,QuestionNumber) Values('0','Have you had any Change in vision?','4','3','1')
					SET IDENTITY_INSERT Interview.Questions OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.ResponseOptions ON
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Blurring/cloudy vision?','4','2','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Patient''s words','1','1','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Double vision?','5','3','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Distorted vision?','6','4','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Rainbows?','7','5','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Flashes of light?','8','6','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Spider-web, or floaters?','9','7','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Sensitivity to light?','10','8','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Pain in the eye?','11','9','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Redness, discharge?','12','10','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Dry, itchy eyes?','13','11','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Excessive tearing?','14','12','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Scratchiness?','15','13','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Headaches?','16','14','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('20','OS','17','1','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('20','OD','18','2','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('20','OU','19','3','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('21','Date','20','4','DatePicker')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('22','Constant','21','5','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('22','Occasional','22','6','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('23','Yes','23','7','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('23','No','24','8','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('23','Patient Comments','25','1','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('24','Yes','26','1','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('24','No','27','2','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('25','Yes','29','3','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('30','BirthDate','44','1','DatePicker')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('31','Identification #','45','1','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('26','Patient Comments','40','0','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('4','Redness, with no discharge?','41','0','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('27','Name','42','1','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('29','Name','43','1','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('25','No','30','4','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('25','Patient Comments','33','5','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('24','Patient Comments','34','0','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('22','Patient Comments','35','0','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('21','Patient Comments','36','0','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('20','Patient Comments','37','0','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('26','Yes','38','0','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Description,Id,ResponseNumber,Type) Values('26','No','39','0','CheckBox')
					SET IDENTITY_INSERT Interview.ResponseOptions OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Interview.ResponseSuggestions (Id) Values('13')
						Insert Into Interview.ResponseSuggestions (Id) Values('26')
						Insert Into Interview.ResponseSuggestions (Id) Values('41')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Interview.ResponseSuggestions_CarePlans (CarePlanId,Id) Values('1','13')
						Insert Into Interview.ResponseSuggestions_CarePlans (CarePlanId,Id) Values('1','41')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Interview.ResponseSuggestions_Interviews (Id,InterviewId) Values('26','1003')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ExamDetails ON
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('5','1','General Evaluation')
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('4','1','Dating')
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('3','1','Pregnancy')
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('2','1','History')
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('1','1','Indication')
					SET IDENTITY_INSERT Diagnostics.ExamDetails OFF");
			//No test data for Components
			//No test data for ExamDetailComponents
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonMaritalStatus ON
						Insert Into dbo.PersonMaritalStatus (MaritalStatusId,Id,PersonId) Values('0','2','0')
						Insert Into dbo.PersonMaritalStatus (MaritalStatusId,Id,PersonId) Values('1','3','1')
					SET IDENTITY_INSERT dbo.PersonMaritalStatus OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Media ON
						Insert Into dbo.Media (Id,MediaTypeId,Value) Values('1','1',cast((select Value from AmoebaDB.dbo.TestValues where Id = 68999) as varbinary(max)))
						Insert Into dbo.Media (Id,MediaTypeId,Value) Values('2','2',cast((select Value from AmoebaDB.dbo.TestValues where Id = 69003) as varbinary(max)))
						Insert Into dbo.Media (Id,MediaTypeId,Value) Values('3','1',cast((select Value from AmoebaDB.dbo.TestValues where Id = 69007) as varbinary(max)))
						Insert Into dbo.Media (Id,MediaTypeId,Value) Values('4','1',cast((select Value from AmoebaDB.dbo.TestValues where Id = 69011) as varbinary(max)))
						Insert Into dbo.Media (Id,MediaTypeId,Value) Values('5','1',cast((select Value from AmoebaDB.dbo.TestValues where Id = 69015) as varbinary(max)))
						Insert Into dbo.Media (Id,MediaTypeId,Value) Values('6','1',cast((select Value from AmoebaDB.dbo.TestValues where Id = 69019) as varbinary(max)))
						Insert Into dbo.Media (Id,MediaTypeId,Value) Values('0','1',cast((select Value from AmoebaDB.dbo.TestValues where Id = 69023) as varbinary(max)))
					SET IDENTITY_INSERT dbo.Media OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.DefaultImages ON
						Insert Into dbo.DefaultImages (Id,MediaId,Type) Values('1','3','Male Patient')
						Insert Into dbo.DefaultImages (Id,MediaId,Type) Values('2','4','Female Patient')
					SET IDENTITY_INSERT dbo.DefaultImages OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonMedia ON
						Insert Into dbo.PersonMedia (MediaId,Id,PersonId) Values('1','2','1')
					SET IDENTITY_INSERT dbo.PersonMedia OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.MedicalSystemInterviews ON
						Insert Into Interview.MedicalSystemInterviews (InterviewId,Id,MedicalSystemId) Values('1003','3','2')
						Insert Into Interview.MedicalSystemInterviews (InterviewId,Id,MedicalSystemId) Values('1004','1','1')
						Insert Into Interview.MedicalSystemInterviews (InterviewId,Id,MedicalSystemId) Values('3','2','2')
					SET IDENTITY_INSERT Interview.MedicalSystemInterviews OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.SyntomMedicalSystems ON
						Insert Into Interview.SyntomMedicalSystems (MedicalSystemId,Id,SyntomId) Values('1','1','1')
						Insert Into Interview.SyntomMedicalSystems (MedicalSystemId,Id,SyntomId) Values('2','2','2')
					SET IDENTITY_INSERT Interview.SyntomMedicalSystems OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonJob ON
						Insert Into dbo.PersonJob (OccupationId,Id,OrganisationId,PersonId) Values('1','1','2','1')
					SET IDENTITY_INSERT dbo.PersonJob OFF");
			//No test data for OrganisationPhoneNumbers
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Organisations_Hotels (Id) Values('3')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_NonResidentPatient (Id) Values('1')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.NonResidentHotelInfo (HotelId,Id) Values('3','1')
");
			//No test data for Organisations_Companys
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_Doctor (Id,Code) Values('1001','523')
						Insert Into dbo.Persons_Doctor (Id,Code) Values('0','000')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.PatientVisit ON
						Insert Into Interview.PatientVisit (Id,DateOfVisit,DoctorId,PatientId) Values('2','2016-01-01','1001','1')
					SET IDENTITY_INSERT Interview.PatientVisit OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.PatientSyntoms ON
						Insert Into Interview.PatientSyntoms (Id,PatientVisitId,Priority,Status,SyntomId) Values('1','2','0','Open','1')
						Insert Into Interview.PatientSyntoms (Id,PatientVisitId,Priority,Status,SyntomId) Values('2','2','1','Open','2')
					SET IDENTITY_INSERT Interview.PatientSyntoms OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.PatientResponses ON
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,PatientVisitId,QuestionId) Values('5','1','2','30')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,PatientVisitId,QuestionId) Values('6','1','2','31')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,PatientVisitId,QuestionId) Values('1','1','2','4')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,PatientVisitId,QuestionId) Values('2','2','2','21')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,PatientVisitId,QuestionId) Values('3','1','2','27')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,PatientVisitId,QuestionId) Values('4','1','2','29')
					SET IDENTITY_INSERT Interview.PatientResponses OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Response ON
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('6','9','45','Alpha1')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('5','8','44','1/1/1990')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('4','7','43','Thomas')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('3','6','42','Lisa')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('2','5','20','1/1/2016')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('1','4','16','False')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('1','3','11','True')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('1','2','12','True')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('1','1','1','My eyes hurting me and it red too')
					SET IDENTITY_INSERT Interview.Response OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.ResponseImages ON
						Insert Into Interview.ResponseImages (MediaId,Id,PatientResponseId) Values('5','1','2')
						Insert Into Interview.ResponseImages (MediaId,Id,PatientResponseId) Values('6','2','2')
					SET IDENTITY_INSERT Interview.ResponseImages OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.PatientResults ON
						Insert Into Diagnostics.PatientResults (Id,PatientVisitId) Values('3','2')
					SET IDENTITY_INSERT Diagnostics.PatientResults OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ExamResults ON
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('3','3','3')
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('4','4','3')
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('5','5','3')
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('1','1','3')
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('2','2','3')
					SET IDENTITY_INSERT Diagnostics.ExamResults OFF");
			//No test data for ExamResults_UmbilicalArtery
			//No test data for ExamResults_AnioticFluid
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ExamResults_SimpleValues ON
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('1','1','3','1','Diabeties Mellitus')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('1','2','3','1','Left Multicystic Dyplastic Kidney')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('2','4','3','2','Blood Group: Rh Negative')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('2','6','3','3','Appendectomy 1991')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('3','18','3','6','Singleton Pregnancy')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('3','19','3','7','1')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('2','8','3','3','Right Oophorectomy, 2002')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('2','14','3','4','Gravida:3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('2','15','3','4','Para:1')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,PatientResultsId,ResultFieldId,Value) Values('2','17','3','5','Preterm Delivery in Previous Pregnancy')
					SET IDENTITY_INSERT Diagnostics.ExamResults_SimpleValues OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ExamResults_FetalDates ON
						Insert Into Diagnostics.ExamResults_FetalDates (Id,Details,EstimatedDays,ExamResultsId,Method,PatientResultsId,StartDate) Values('5','GA: 6w + 6d, by PCP Ultrasound','205','4','External Assessment','3','2013-10-20')
						Insert Into Diagnostics.ExamResults_FetalDates (Id,Details,EstimatedDays,ExamResultsId,Method,PatientResultsId,StartDate) Values('4','Cycle: Irregular Cycle','213','4','LMP','3','2015-08-25')
					SET IDENTITY_INSERT Diagnostics.ExamResults_FetalDates OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Diagnostics.AssignedDating (ExamResultsId,Id) Values('4','4')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Vitals.VitalSigns ON
						Insert Into Vitals.VitalSigns (Id,DateTimeOfReading,ReaderId) Values('1','Jun  5 2016  9:22PM','1004')
					SET IDENTITY_INSERT Vitals.VitalSigns OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PatientVisitVitalSigns ON
						Insert Into dbo.PatientVisitVitalSigns (PatientVisitId,Id,VitalSignsId) Values('2','2','1')
					SET IDENTITY_INSERT dbo.PatientVisitVitalSigns OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonEmailAddress ON
						Insert Into dbo.PersonEmailAddress (Email,Id,PersonId) Values('josephbartholomew@outlook.com','1','1')
						Insert Into dbo.PersonEmailAddress (Email,Id,PersonId) Values('johndoe@nowhere.com','0','0')
					SET IDENTITY_INSERT dbo.PersonEmailAddress OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonNames ON
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('12','4','Neilani')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('13','4','Jeremiah')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('14','1004','Grace')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('15','1004','John')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('4','0','John')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('5','0','Doe')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('6','1','Joseph')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('7','1','Bartholomew')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('8','1001','Lutz')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('9','1001','Amechi')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('10','3','Jonali')
						Insert Into dbo.PersonNames (Id,PersonId,PersonName) Values('11','3','St. Louis')
					SET IDENTITY_INSERT dbo.PersonNames OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonPhoneNumbers ON
						Insert Into dbo.PersonPhoneNumbers (Id,PersonId,PhoneNumber,PhoneTypeId) Values('0','0','00000000','0')
						Insert Into dbo.PersonPhoneNumbers (Id,PersonId,PhoneNumber,PhoneTypeId) Values('1','1','4058243','1')
						Insert Into dbo.PersonPhoneNumbers (Id,PersonId,PhoneNumber,PhoneTypeId) Values('3','3','4564724','1')
						Insert Into dbo.PersonPhoneNumbers (Id,PersonId,PhoneNumber,PhoneTypeId) Values('4','4','4095656','1')
					SET IDENTITY_INSERT dbo.PersonPhoneNumbers OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.PrimaryPersonPhoneNumber (PersonPhoneNumberId,Id) Values('1','1')
						Insert Into dbo.PrimaryPersonPhoneNumber (PersonPhoneNumberId,Id) Values('0','0')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.UserSignIn (Password,Username,Id) Values('test','joe','1')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_NextOfKin (Id,PatientId,Relationship) Values('4','1','Daughter')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_EmergencyContact (Id,PatientId) Values('3','1')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.ForeignPhoneNumbers ON
						Insert Into dbo.ForeignPhoneNumbers (Id,PersonId,PhoneNumber,PhoneTypeId) Values('1','1','+1-473-405-8243','1')
					SET IDENTITY_INSERT dbo.ForeignPhoneNumbers OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Vitals.BloodPressure (Diastolic,Id,Systolic,UnitId) Values('90','1','170','6')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Vitals.Height (Id,UnitId,Value) Values('1','1','78.5')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Vitals.Pulse (Id,UnitId,Value) Values('1','2','100')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Vitals.Respiration (Id,UnitId,Value) Values('1','4','60')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Vitals.Temperature (Id,UnitId,Value) Values('1','3','98.6')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Vitals.Weight (Value,UnitId,Id) Values('180','5','1')
");
		}
               
			
		
	}
}
