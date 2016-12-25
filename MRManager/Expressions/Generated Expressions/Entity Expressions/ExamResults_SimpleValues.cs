﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Expressions.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using Common.DataEntites;
using EF.Entities;
using Interfaces;

namespace Entity.Expressions
{
	public static partial class ExamResults_SimpleValuesExpressions
	{
		public static IQueryable<ExamResults_SimpleValues> GetExamResults_SimpleValuesById(this IQueryable<ExamResults_SimpleValues> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Exams> GetExams(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().ExamDetails().Exams();
    
		public static IQueryable<Components> GetComponents(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().ExamDetails().ExamDetailComponents().Components();
    
		public static IQueryable<ExamDetailComponents> GetExamDetailComponents(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().ExamDetails().ExamDetailComponents();
    
		public static IQueryable<ExamDetails> GetExamDetails(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().ExamDetails();
    
		public static IQueryable<Cities> GetCities(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCities().Cities();
    
		public static IQueryable<AddressCities> GetAddressCities(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCities();
    
		public static IQueryable<Countries> GetCountries(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PersonCountryOfResidence().Countries();
    
		public static IQueryable<AddressCountries> GetAddressCountries(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCountries();
    
		public static IQueryable<AddressLines> GetAddressLines(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressLines();
    
		public static IQueryable<Parishes> GetParishes(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressParishes().Parishes();
    
		public static IQueryable<AddressParishes> GetAddressParishes(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressParishes();
    
		public static IQueryable<States> GetStates(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressStates().States();
    
		public static IQueryable<AddressStates> GetAddressStates(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressStates();
    
		public static IQueryable<ZipCodes> GetZipCodes(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressZipCodes().ZipCodes();
    
		public static IQueryable<AddressZipCodes> GetAddressZipCodes(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressZipCodes();
    
		public static IQueryable<BoatInfo> GetBoatInfo(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient().BoatInfo();
    
		public static IQueryable<PhoneTypes> GetPhoneTypes(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers().PhoneTypes();
    
		public static IQueryable<ForeignPhoneNumbers> GetForeignPhoneNumbers(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient().ForeignPhoneNumbers();
    
		public static IQueryable<NonResidentCompanyInfo> GetNonResidentCompanyInfo(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentCompanyInfo();
    
		public static IQueryable<Organisations> GetOrganisations(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PersonJob().Organisations();
    
		public static IQueryable<Organisations_Hotels> GetOrganisations_Hotels(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo().Organisations_Hotels();
    
		public static IQueryable<NonResidentHotelInfo> GetNonResidentHotelInfo(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo();
    
		public static IQueryable<Persons_ArrivalDepartureInfo> GetPersons_ArrivalDepartureInfo(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient().Persons_ArrivalDepartureInfo();
    
		public static IQueryable<StudentInfo> GetStudentInfo(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient().StudentInfo();
    
		public static IQueryable<Persons_NonResidentPatient> GetPersons_NonResidentPatient(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient();
    
		public static IQueryable<AddressTypes> GetAddressTypes(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().AddressTypes();
    
		public static IQueryable<ForeignAddresses> GetForeignAddresses(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NonResidentPatient().ForeignAddresses();
    
		public static IQueryable<OrganisationAddress> GetOrganisationAddress(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().OrganisationAddress();
    
		public static IQueryable<Addresses> GetAddresses(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses();
    
		public static IQueryable<PrimaryPersonAddress> GetPrimaryPersonAddress(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses().PrimaryPersonAddress();
    
		public static IQueryable<PersonAddresses> GetPersonAddresses(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonAddresses();
    
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddress(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonEmailAddress();
    
		public static IQueryable<MediaTypes> GetMediaTypes(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().ResponseImages().Media().MediaTypes();
    
		public static IQueryable<DefaultImages> GetDefaultImages(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().ResponseImages().Media().DefaultImages();
    
		public static IQueryable<Syntoms> GetSyntoms(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientSyntoms().Syntoms();
    
		public static IQueryable<PatientSyntoms> GetPatientSyntoms(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientSyntoms();
    
		public static IQueryable<Interviews> GetInterviews(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().Questions().Interviews();
    
		public static IQueryable<Response> GetResponse(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().Response();
    
		public static IQueryable<CarePlan> GetCarePlan(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans().CarePlan();
    
		public static IQueryable<ResponseSuggestions_CarePlans> GetResponseSuggestions_CarePlans(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans();
    
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_Interviews(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_Interviews();
    
		public static IQueryable<ResponseSuggestions> GetResponseSuggestions(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions();
    
		public static IQueryable<ResponseOptions> GetResponseOptions(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().Questions().ResponseOptions();
    
		public static IQueryable<Questions> GetQuestions(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().Questions();
    
		public static IQueryable<PatientResponses> GetPatientResponses(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses();
    
		public static IQueryable<ResponseImages> GetResponseImages(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().ResponseImages();
    
		public static IQueryable<Media> GetMedia(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientResponses().ResponseImages().Media();
    
		public static IQueryable<PersonMedia> GetPersonMedia(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonMedia();
    
		public static IQueryable<PersonNames> GetPersonNames(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonNames();
    
		public static IQueryable<PrimaryPersonPhoneNumber> GetPrimaryPersonPhoneNumber(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers().PrimaryPersonPhoneNumber();
    
		public static IQueryable<PersonPhoneNumbers> GetPersonPhoneNumbers(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers();
    
		public static IQueryable<PatientDoctor> GetPatientDoctor(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PatientDoctor();
    
		public static IQueryable<Persons_Doctor> GetPersons_Doctor(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Doctor();
    
		public static IQueryable<Persons_EmergencyContact> GetPersons_EmergencyContact(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_EmergencyContact();
    
		public static IQueryable<Persons_NextOfKin> GetPersons_NextOfKin(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons_NextOfKin();
    
		public static IQueryable<UserSignIn> GetUserSignIn(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons().UserSignIn();
    
		public static IQueryable<Units> GetUnits(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns().VitalSigns().BloodPressure().Units();
    
		public static IQueryable<BloodPressure> GetBloodPressure(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns().VitalSigns().BloodPressure();
    
		public static IQueryable<Height> GetHeight(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns().VitalSigns().Height();
    
		public static IQueryable<PatientVisitVitalSigns> GetPatientVisitVitalSigns(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns();
    
		public static IQueryable<Pulse> GetPulse(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns().VitalSigns().Pulse();
    
		public static IQueryable<Respiration> GetRespiration(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns().VitalSigns().Respiration();
    
		public static IQueryable<Temperature> GetTemperature(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns().VitalSigns().Temperature();
    
		public static IQueryable<Weight> GetWeight(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns().VitalSigns().Weight();
    
		public static IQueryable<VitalSigns> GetVitalSigns(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().PatientVisitVitalSigns().VitalSigns();
    
		public static IQueryable<Persons> GetPersons(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Persons();
    
		public static IQueryable<Sex> GetSex(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().Sex();
    
		public static IQueryable<Allergies> GetAllergies(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PatientAllergies().Allergies();
    
		public static IQueryable<PatientAllergies> GetPatientAllergies(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PatientAllergies();
    
		public static IQueryable<PatientReligon> GetPatientReligon(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PatientReligon();
    
		public static IQueryable<PersonCountryOfResidence> GetPersonCountryOfResidence(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PersonCountryOfResidence();
    
		public static IQueryable<Occupations> GetOccupations(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PersonJob().Occupations();
    
		public static IQueryable<PersonJob> GetPersonJob(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PersonJob();
    
		public static IQueryable<PersonMaritalStatus> GetPersonMaritalStatus(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient().PersonMaritalStatus();
    
		public static IQueryable<Persons_Patient> GetPersons_Patient(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit().Persons_Patient();
    
		public static IQueryable<PatientVisit> GetPatientVisit(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults().PatientVisit();
    
		public static IQueryable<PatientResults> GetPatientResults(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().PatientResults();
    
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluid(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().ExamResults_AnioticFluid();
    
		public static IQueryable<AssignedDating> GetAssignedDating(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().ExamResults_FetalDates().AssignedDating();
    
		public static IQueryable<ExamResults_FetalDates> GetExamResults_FetalDates(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().ExamResults_FetalDates();
    
		public static IQueryable<ExamResults_UmbilicalArtery> GetExamResults_UmbilicalArtery(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults().ExamResults_UmbilicalArtery();
    
		public static IQueryable<ExamResults> GetExamResults(this IQueryable<ExamResults_SimpleValues> query) => query.ExamResults();
    
		public static IQueryable<ResultFieldNames> GetResultFieldNames(this IQueryable<ExamResults_SimpleValues> query) => query.ResultFieldNames();
    
		public static IQueryable<ExamResults_SimpleValues> GetExamResults_SimpleValues(this IQueryable<ExamResults_SimpleValues> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<ExamResults_SimpleValues> ExamResults_SimpleValues(this IQueryable<ExamResults> examresults) => examresults.SelectMany(x => x.ExamResults_SimpleValues);
				public static IQueryable<ExamResults> ExamResults(this IQueryable<ExamResults_SimpleValues> query) => query.Select(x => x.ExamResults);
				//public static IQueryable<ExamResults_SimpleValues> ExamResults_SimpleValues(this IQueryable<ResultFieldNames> resultfieldnames) => resultfieldnames.SelectMany(x => x.ExamResults_SimpleValues);
				public static IQueryable<ResultFieldNames> ResultFieldNames(this IQueryable<ExamResults_SimpleValues> query) => query.Select(x => x.ResultFieldNames);
	}
}
