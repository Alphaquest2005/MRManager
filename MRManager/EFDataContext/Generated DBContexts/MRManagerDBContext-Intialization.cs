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
						Insert Into dbo.Addresses (Id,EntryDateTime) Values('0','2016-05-29')
						Insert Into dbo.Addresses (Id,EntryDateTime) Values('1','2016-05-29')
						Insert Into dbo.Addresses (Id,EntryDateTime) Values('2','2016-05-29')
						Insert Into dbo.Addresses (Id,EntryDateTime) Values('3','2016-01-06')
					SET IDENTITY_INSERT dbo.Addresses OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.AddressTypes ON
						Insert Into dbo.AddressTypes (Id,Name) Values('0','Unspecified')
						Insert Into dbo.AddressTypes (Id,Name) Values('4','Home')
					SET IDENTITY_INSERT dbo.AddressTypes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Allergies ON
						Insert Into dbo.Allergies (Id,Name) Values('0','Unspecified')
						Insert Into dbo.Allergies (Id,Name) Values('1','Milk')
						Insert Into dbo.Allergies (Id,Name) Values('2','Asprin')
					SET IDENTITY_INSERT dbo.Allergies OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.CarePlan ON
						Insert Into Interview.CarePlan (Id,Name,Diagnosis) Values('1','Eye Wash','Redness and Itchy Eyes')
					SET IDENTITY_INSERT Interview.CarePlan OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Cities ON
						Insert Into dbo.Cities (Id,Name) Values('0','Unspecified')
						Insert Into dbo.Cities (Id,Name) Values('3','New York')
						Insert Into dbo.Cities (Id,Name) Values('1','St. Geroge''s')
					SET IDENTITY_INSERT dbo.Cities OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Countries ON
						Insert Into dbo.Countries (Id,Name) Values('0','Unspecified')
						Insert Into dbo.Countries (Id,Name) Values('1','Grenada')
						Insert Into dbo.Countries (Id,Name) Values('2','USA')
					SET IDENTITY_INSERT dbo.Countries OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.EntityAttributes ON
						Insert Into dbo.EntityAttributes (Id,Entity,Attribute,Type) Values('0','unspecified','unspecified','unspecified')
						Insert Into dbo.EntityAttributes (Id,Entity,Attribute,Type) Values('1','Patient','Name','string')
						Insert Into dbo.EntityAttributes (Id,Entity,Attribute,Type) Values('3','Patient','BirthDate','date')
						Insert Into dbo.EntityAttributes (Id,Entity,Attribute,Type) Values('4','Patient','Id','string')
					SET IDENTITY_INSERT dbo.EntityAttributes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.Exams ON
						Insert Into Diagnostics.Exams (Id,ExamTypeId,Name,Describtion) Values('1','1','Fetal Wellbeing','Regular fetal ultrasound')
					SET IDENTITY_INSERT Diagnostics.Exams OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.MaritalStatus ON
						Insert Into dbo.MaritalStatus (Name,Id) Values('Divorced','1')
						Insert Into dbo.MaritalStatus (Name,Id) Values('Unspecified','0')
					SET IDENTITY_INSERT dbo.MaritalStatus OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.MediaTypes ON
						Insert Into dbo.MediaTypes (Id,MediaTypeName,FileExtension) Values('1','Image','jpg,png')
						Insert Into dbo.MediaTypes (Id,MediaTypeName,FileExtension) Values('2','Video','mov')
					SET IDENTITY_INSERT dbo.MediaTypes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.MedicalCategory ON
						Insert Into Interview.MedicalCategory (Id,Category) Values('1','Optical')
						Insert Into Interview.MedicalCategory (Id,Category) Values('3','Personal')
						Insert Into Interview.MedicalCategory (Id,Category) Values('2','General')
					SET IDENTITY_INSERT Interview.MedicalCategory OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Occupations ON
						Insert Into dbo.Occupations (Id,Name) Values('0','Unspecified')
						Insert Into dbo.Occupations (Id,Name) Values('1','Software Developer')
					SET IDENTITY_INSERT dbo.Occupations OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Organisations ON
						Insert Into dbo.Organisations (Id,Name,EntryTimeStamp,VATNumber) Values('0','Unspecified',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66084) as varbinary(max)),'0')
						Insert Into dbo.Organisations (Id,Name,EntryTimeStamp,VATNumber) Values('2','Insight Software',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66088) as varbinary(max)),'0')
						Insert Into dbo.Organisations (Id,Name,EntryTimeStamp,VATNumber) Values('3','Spice Inn Hotel',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66092) as varbinary(max)),'123')
					SET IDENTITY_INSERT dbo.Organisations OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Parishes ON
						Insert Into dbo.Parishes (Name,Id) Values('St. David''s','2')
						Insert Into dbo.Parishes (Name,Id) Values('St. George','1')
						Insert Into dbo.Parishes (Name,Id) Values('Unspecified','0')
					SET IDENTITY_INSERT dbo.Parishes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Patients ON
						Insert Into Interview.Patients (Id,EntryDateTime) Values('1',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66145) as varbinary(max)))
					SET IDENTITY_INSERT Interview.Patients OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Persons ON
						Insert Into dbo.Persons (Id,EntryDateTine) Values('0','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('1','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('3','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('4','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('1001','Jun  5 2016  9:18PM')
						Insert Into dbo.Persons (Id,EntryDateTine) Values('1004','Jun  5 2016 12:00AM')
					SET IDENTITY_INSERT dbo.Persons OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Phase ON
						Insert Into Interview.Phase (Id,Name,Code) Values('1','Chief Complaint','CC')
						Insert Into Interview.Phase (Id,Name,Code) Values('2','Present Illness','PI')
						Insert Into Interview.Phase (Id,Name,Code) Values('1002','Patient History','PH')
						Insert Into Interview.Phase (Id,Name,Code) Values('1003','Patient Personal Info','PPI')
					SET IDENTITY_INSERT Interview.Phase OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PhoneTypes ON
						Insert Into dbo.PhoneTypes (Id,Name) Values('0','Unspecified')
						Insert Into dbo.PhoneTypes (Id,Name) Values('1','Lime')
					SET IDENTITY_INSERT dbo.PhoneTypes OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ResultFieldNames ON
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('1','Indication')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('2','General History')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('3','Past Surgical History')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('4','OB History')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('5','Risk Factors')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('7','Number of Fetues')
						Insert Into Diagnostics.ResultFieldNames (Id,Name) Values('6','Pregnacy Type')
					SET IDENTITY_INSERT Diagnostics.ResultFieldNames OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.Sex ON
						Insert Into dbo.Sex (Name,Id) Values('Female','2')
						Insert Into dbo.Sex (Name,Id) Values('Male','1')
						Insert Into dbo.Sex (Name,Id) Values('Unspecified','0')
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
						Insert Into dbo.Units (Id,Name,ShortName,Description) Values('5','Lbs','lbs','Pounds')
						Insert Into dbo.Units (Id,Name,ShortName,Description) Values('6','Systolic/Dia','Sys/Dia','Systolic / Diatolic')
						Insert Into dbo.Units (Id,Name,ShortName,Description) Values('2','Bpm','bpm','Beats per minute')
						Insert Into dbo.Units (Id,Name,ShortName,Description) Values('3','Fahrenheit','ºF','Temperature')
						Insert Into dbo.Units (Id,Name,ShortName,Description) Values('4','bpm','bpm','Breaths Per Minute')
						Insert Into dbo.Units (Id,Name,ShortName,Description) Values('1','Inches','in.','12 inches make 1 foot')
					SET IDENTITY_INSERT dbo.Units OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.ZipCodes ON
						Insert Into dbo.ZipCodes (Name,Id) Values('12346','1')
					SET IDENTITY_INSERT dbo.ZipCodes OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressCities (Id,CityId) Values('0','0')
						Insert Into dbo.AddressCities (Id,CityId) Values('2','3')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressCountries (Id,CountryId) Values('2','2')
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
						Insert Into dbo.AddressParishes (Id,ParishId) Values('1','2')
						Insert Into dbo.AddressParishes (Id,ParishId) Values('3','1')
						Insert Into dbo.AddressParishes (Id,ParishId) Values('0','0')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressStates (Id,StateId) Values('2','1')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.AddressZipCodes (Id,ZipCodeId) Values('2','1')
");
			//No test data for OrganisationAddress
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonAddresses ON
						Insert Into dbo.PersonAddresses (AddressId,Id,PersonId,AddressTypeId) Values('0','0','0','0')
						Insert Into dbo.PersonAddresses (AddressId,Id,PersonId,AddressTypeId) Values('1','1','1','4')
						Insert Into dbo.PersonAddresses (AddressId,Id,PersonId,AddressTypeId) Values('3','2','4','4')
					SET IDENTITY_INSERT dbo.PersonAddresses OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.PrimaryPersonAddress (PersonAddressesId,Id) Values('1','1')
						Insert Into dbo.PrimaryPersonAddress (PersonAddressesId,Id) Values('0','0')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_Patient (CountryId,Id,DateOfBirth,SexId) Values('0','0','1783-01-01','0')
						Insert Into dbo.Persons_Patient (CountryId,Id,DateOfBirth,SexId) Values('1','1','1980-05-17','1')
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
						Insert Into dbo.PersonCountryOfResidence (CountryId,Id,PersonId,Date) Values('0','1','0','2016-01-01')
					SET IDENTITY_INSERT dbo.PersonCountryOfResidence OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Interviews ON
						Insert Into Interview.Interviews (Id,Name,PhaseId,MedicalCategoryId) Values('3','New Patient Optical','1','1')
						Insert Into Interview.Interviews (Id,Name,PhaseId,MedicalCategoryId) Values('1003','Previous Medical Care','1002','2')
						Insert Into Interview.Interviews (Id,Name,PhaseId,MedicalCategoryId) Values('1004','Personal Information','1003','3')
					SET IDENTITY_INSERT Interview.Interviews OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Questions ON
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('4','31','1004','Patients Identification Number')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('3','30','1004','What is your Birthdate')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('1','29','1004','What is Your Last Name')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('1','27','1004','What is Your First Name')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('0','26','1003','Are you under a physicians care or have you been during the past 5 years, including hospitalization, and surgery?')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('0','25','3','Is the symptom/problem getting better or worse?')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('0','24','3','Have you ever been treated for this complaint?')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('0','23','3','How does this problem interfere with your work or other activities? ')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('0','22','3','Is this symptom/problem constant or occasional?')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('0','21','3','When was the onset?')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('0','20','3','Which Eye?')
						Insert Into Interview.Questions (EntityAttribute,Id,InterviewId,Description) Values('0','4','3','Have you had any Change in vision?')
					SET IDENTITY_INSERT Interview.Questions OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.ResponseOptions ON
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('30','44','BirthDate','DatePicker')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('31','45','Identification #','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('29','43','Name','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('27','42','Name','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','41','Redness, with no discharge?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('26','40','Patient Comments','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('26','39','No','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('26','38','Yes','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('20','37','Patient Comments','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('21','36','Patient Comments','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('22','35','Patient Comments','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('24','34','Patient Comments','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('25','33','Patient Comments','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('25','30','No','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('25','29','Yes','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('24','27','No','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('24','26','Yes','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('23','25','Patient Comments','TextBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('23','24','No','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('23','23','Yes','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('22','22','Occasional','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('22','21','Constant','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('21','20','Date','DatePicker')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('20','19','OU','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('20','18','OD','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('20','17','OS','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','16','Headaches?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','15','Scratchiness?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','14','Excessive tearing?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','13','Dry, itchy eyes?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','12','Redness, discharge?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','11','Pain in the eye?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','10','Sensitivity to light?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','9','Spider-web, or floaters?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','8','Flashes of light?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','7','Rainbows?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','6','Distorted vision?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','5','Double vision?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','4','Blurring/cloudy vision?','CheckBox')
						Insert Into Interview.ResponseOptions (QuestionId,Id,Description,Type) Values('4','1','Patient''s words','TextBox')
					SET IDENTITY_INSERT Interview.ResponseOptions OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Interview.ResponseSuggestions (Id) Values('13')
						Insert Into Interview.ResponseSuggestions (Id) Values('26')
						Insert Into Interview.ResponseSuggestions (Id) Values('41')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Interview.ResponseSuggestions_CarePlans (CarePlanId,Id) Values('1','41')
						Insert Into Interview.ResponseSuggestions_CarePlans (CarePlanId,Id) Values('1','13')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Interview.ResponseSuggestions_Interviews (Id,InterviewId) Values('26','1003')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ExamDetails ON
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('1','1','Indication')
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('2','1','History')
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('3','1','Pregnancy')
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('5','1','General Evaluation')
						Insert Into Diagnostics.ExamDetails (Id,ExamId,Section) Values('4','1','Dating')
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
						Insert Into dbo.Media (Id,Value,MediaTypeId) Values('0',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66062) as varbinary(max)),'1')
						Insert Into dbo.Media (Id,Value,MediaTypeId) Values('6',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66058) as varbinary(max)),'1')
						Insert Into dbo.Media (Id,Value,MediaTypeId) Values('5',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66054) as varbinary(max)),'1')
						Insert Into dbo.Media (Id,Value,MediaTypeId) Values('4',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66050) as varbinary(max)),'1')
						Insert Into dbo.Media (Id,Value,MediaTypeId) Values('3',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66046) as varbinary(max)),'1')
						Insert Into dbo.Media (Id,Value,MediaTypeId) Values('2',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66042) as varbinary(max)),'2')
						Insert Into dbo.Media (Id,Value,MediaTypeId) Values('1',cast((select Value from AmoebaDB.dbo.TestValues where Id = 66038) as varbinary(max)),'1')
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
					SET IDENTITY_INSERT dbo.PersonJob ON
						Insert Into dbo.PersonJob (OccupationId,Id,PersonId,OrganisationId) Values('1','1','1','2')
					SET IDENTITY_INSERT dbo.PersonJob OFF");
			//No test data for OrganisationPhoneNumbers
			//No test data for Organisations_Companys
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Organisations_Hotels (Id) Values('3')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_NonResidentPatient (Id) Values('1')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.NonResidentHotelInfo (Id,HotelId) Values('1','3')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_Doctor (Id,Code) Values('0','000')
						Insert Into dbo.Persons_Doctor (Id,Code) Values('1001','523')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PatientVisit ON
						Insert Into dbo.PatientVisit (Id,PatientId,DateOfVisit,DoctorId) Values('2','1','2016-01-01','1001')
					SET IDENTITY_INSERT dbo.PatientVisit OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.PatientSyntoms ON
						Insert Into Interview.PatientSyntoms (Id,SyntomId,PatientVisitId) Values('1','1','2')
						Insert Into Interview.PatientSyntoms (Id,SyntomId,PatientVisitId) Values('2','2','2')
					SET IDENTITY_INSERT Interview.PatientSyntoms OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.PatientResponses ON
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,QuestionId,PatientVisitId) Values('1','1','4','2')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,QuestionId,PatientVisitId) Values('2','2','21','2')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,QuestionId,PatientVisitId) Values('3','1','27','2')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,QuestionId,PatientVisitId) Values('4','1','29','2')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,QuestionId,PatientVisitId) Values('6','1','31','2')
						Insert Into Interview.PatientResponses (Id,PatientSyntomId,QuestionId,PatientVisitId) Values('5','1','30','2')
					SET IDENTITY_INSERT Interview.PatientResponses OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.Response ON
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('1','1','1','My eyes hurting me and it red too')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('1','2','12','True')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('1','3','11','True')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('1','4','16','False')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('2','5','20','1/1/2016')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('3','6','42','Lisa')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('4','7','43','Thomas')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('5','8','44','1/1/1990')
						Insert Into Interview.Response (PatientResponseId,Id,ResponseOptionId,Value) Values('6','9','45','Alpha1')
					SET IDENTITY_INSERT Interview.Response OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Interview.ResponseImages ON
						Insert Into Interview.ResponseImages (MediaId,Id,PatientResponseId) Values('6','2','2')
						Insert Into Interview.ResponseImages (MediaId,Id,PatientResponseId) Values('5','1','2')
					SET IDENTITY_INSERT Interview.ResponseImages OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.PatientResults ON
						Insert Into Diagnostics.PatientResults (Id,PatientVisitId) Values('3','2')
					SET IDENTITY_INSERT Diagnostics.PatientResults OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ExamResults ON
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('1','1','3')
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('5','5','3')
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('4','4','3')
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('3','3','3')
						Insert Into Diagnostics.ExamResults (ExamDetailsId,Id,PatientResultsId) Values('2','2','3')
					SET IDENTITY_INSERT Diagnostics.ExamResults OFF");
			//No test data for ExamResults_AnioticFluid
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ExamResults_FetalDates ON
						Insert Into Diagnostics.ExamResults_FetalDates (Id,ExamResultsId,StartDate,Method,Details,EstimatedDays,PatientResultsId) Values('4','4','2015-08-25','LMP','Cycle: Irregular Cycle','213','3')
						Insert Into Diagnostics.ExamResults_FetalDates (Id,ExamResultsId,StartDate,Method,Details,EstimatedDays,PatientResultsId) Values('5','4','2013-10-20','External Assessment','GA: 6w + 6d, by PCP Ultrasound','205','3')
					SET IDENTITY_INSERT Diagnostics.ExamResults_FetalDates OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Diagnostics.AssignedDating (Id,ExamResultsId) Values('4','4')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT Diagnostics.ExamResults_SimpleValues ON
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('3','18','6','Singleton Pregnancy','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('2','17','5','Preterm Delivery in Previous Pregnancy','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('3','19','7','1','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('2','15','4','Para:1','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('2','14','4','Gravida:3','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('2','8','3','Right Oophorectomy, 2002','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('2','6','3','Appendectomy 1991','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('2','4','2','Blood Group: Rh Negative','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('1','2','1','Left Multicystic Dyplastic Kidney','3')
						Insert Into Diagnostics.ExamResults_SimpleValues (ExamResultsId,Id,ResultFieldId,Value,PatientResultsId) Values('1','1','1','Diabeties Mellitus','3')
					SET IDENTITY_INSERT Diagnostics.ExamResults_SimpleValues OFF");
			//No test data for ExamResults_UmbilicalArtery
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
						Insert Into dbo.PersonEmailAddress (Id,Email,PersonId) Values('0','johndoe@nowhere.com','0')
						Insert Into dbo.PersonEmailAddress (Id,Email,PersonId) Values('1','josephbartholomew@outlook.com','1')
					SET IDENTITY_INSERT dbo.PersonEmailAddress OFF");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.PersonNames ON
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('0','4','John')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('0','5','Doe')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('1','6','Joseph')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('1','7','Bartholomew')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('1001','8','Lutz')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('1001','9','Amechi')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('3','10','Jonali')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('3','11','St. Louis')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('4','12','Neilani')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('4','13','Jeremiah')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('1004','14','Grace')
						Insert Into dbo.PersonNames (PersonId,Id,PersonName) Values('1004','15','John')
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
						Insert Into dbo.Persons_EmergencyContact (Id,PatientId) Values('3','1')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.Persons_NextOfKin (Id,PatientId,Relationship) Values('4','1','Daughter')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into dbo.UserSignIn (Username,Password,Id) Values('joe','test','1')
");
				Instance.Database.ExecuteSqlCommand(@"
					SET IDENTITY_INSERT dbo.ForeignPhoneNumbers ON
						Insert Into dbo.ForeignPhoneNumbers (Id,PersonId,PhoneNumber,PhoneTypeId) Values('1','1','+1-473-405-8243','1')
					SET IDENTITY_INSERT dbo.ForeignPhoneNumbers OFF");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Vitals.BloodPressure (Id,Systolic,Diastolic,UnitId) Values('1','170','90','6')
");
				Instance.Database.ExecuteSqlCommand(@"
						Insert Into Vitals.Height (Id,Value,UnitId) Values('1','78.5','1')
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
