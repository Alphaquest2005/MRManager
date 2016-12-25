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
	public static partial class ForeignPhoneNumbersExpressions
	{
		public static IQueryable<ForeignPhoneNumbers> GetForeignPhoneNumbersById(this IQueryable<ForeignPhoneNumbers> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Cities> GetCities(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressCities().Cities();
    
		public static IQueryable<AddressCities> GetAddressCities(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressCities();
    
		public static IQueryable<Countries> GetCountries(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PersonCountryOfResidence().Countries();
    
		public static IQueryable<AddressCountries> GetAddressCountries(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressCountries();
    
		public static IQueryable<AddressLines> GetAddressLines(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressLines();
    
		public static IQueryable<Parishes> GetParishes(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressParishes().Parishes();
    
		public static IQueryable<AddressParishes> GetAddressParishes(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressParishes();
    
		public static IQueryable<States> GetStates(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressStates().States();
    
		public static IQueryable<AddressStates> GetAddressStates(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressStates();
    
		public static IQueryable<ZipCodes> GetZipCodes(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressZipCodes().ZipCodes();
    
		public static IQueryable<AddressZipCodes> GetAddressZipCodes(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().AddressZipCodes();
    
		public static IQueryable<AddressTypes> GetAddressTypes(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().AddressTypes();
    
		public static IQueryable<ForeignAddresses> GetForeignAddresses(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses();
    
		public static IQueryable<Organisations> GetOrganisations(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PersonJob().Organisations();
    
		public static IQueryable<OrganisationAddress> GetOrganisationAddress(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses().OrganisationAddress();
    
		public static IQueryable<Addresses> GetAddresses(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().ForeignAddresses().Addresses();
    
		public static IQueryable<PrimaryPersonAddress> GetPrimaryPersonAddress(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonAddresses().PrimaryPersonAddress();
    
		public static IQueryable<PersonAddresses> GetPersonAddresses(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonAddresses();
    
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddress(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonEmailAddress();
    
		public static IQueryable<MediaTypes> GetMediaTypes(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonMedia().Media().MediaTypes();
    
		public static IQueryable<DefaultImages> GetDefaultImages(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonMedia().Media().DefaultImages();
    
		public static IQueryable<Syntoms> GetSyntoms(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientSyntoms().Syntoms();
    
		public static IQueryable<PatientDoctor> GetPatientDoctor(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientDoctor();
    
		public static IQueryable<Persons_Doctor> GetPersons_Doctor(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().Persons_Doctor();
    
		public static IQueryable<Exams> GetExams(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamDetails().Exams();
    
		public static IQueryable<Components> GetComponents(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents().Components();
    
		public static IQueryable<ExamDetailComponents> GetExamDetailComponents(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents();
    
		public static IQueryable<ExamDetails> GetExamDetails(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamDetails();
    
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluid(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamResults_AnioticFluid();
    
		public static IQueryable<AssignedDating> GetAssignedDating(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates().AssignedDating();
    
		public static IQueryable<ExamResults_FetalDates> GetExamResults_FetalDates(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates();
    
		public static IQueryable<ResultFieldNames> GetResultFieldNames(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues().ResultFieldNames();
    
		public static IQueryable<ExamResults_SimpleValues> GetExamResults_SimpleValues(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues();
    
		public static IQueryable<ExamResults_UmbilicalArtery> GetExamResults_UmbilicalArtery(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults().ExamResults_UmbilicalArtery();
    
		public static IQueryable<ExamResults> GetExamResults(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults().ExamResults();
    
		public static IQueryable<PatientResults> GetPatientResults(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResults();
    
		public static IQueryable<Units> GetUnits(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().VitalSigns().BloodPressure().Units();
    
		public static IQueryable<BloodPressure> GetBloodPressure(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().VitalSigns().BloodPressure();
    
		public static IQueryable<Height> GetHeight(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().VitalSigns().Height();
    
		public static IQueryable<Pulse> GetPulse(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().VitalSigns().Pulse();
    
		public static IQueryable<Respiration> GetRespiration(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().VitalSigns().Respiration();
    
		public static IQueryable<Temperature> GetTemperature(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().VitalSigns().Temperature();
    
		public static IQueryable<Weight> GetWeight(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().VitalSigns().Weight();
    
		public static IQueryable<VitalSigns> GetVitalSigns(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().VitalSigns();
    
		public static IQueryable<PatientVisitVitalSigns> GetPatientVisitVitalSigns(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientVisitVitalSigns();
    
		public static IQueryable<PatientVisit> GetPatientVisit(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit();
    
		public static IQueryable<PatientSyntoms> GetPatientSyntoms(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientSyntoms();
    
		public static IQueryable<Interviews> GetInterviews(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().Questions().Interviews();
    
		public static IQueryable<Response> GetResponse(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().Response();
    
		public static IQueryable<CarePlan> GetCarePlan(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans().CarePlan();
    
		public static IQueryable<ResponseSuggestions_CarePlans> GetResponseSuggestions_CarePlans(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans();
    
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_Interviews(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_Interviews();
    
		public static IQueryable<ResponseSuggestions> GetResponseSuggestions(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions();
    
		public static IQueryable<ResponseOptions> GetResponseOptions(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().Questions().ResponseOptions();
    
		public static IQueryable<Questions> GetQuestions(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().Questions();
    
		public static IQueryable<PatientResponses> GetPatientResponses(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses();
    
		public static IQueryable<ResponseImages> GetResponseImages(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientVisit().PatientResponses().ResponseImages();
    
		public static IQueryable<Media> GetMedia(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonMedia().Media();
    
		public static IQueryable<PersonMedia> GetPersonMedia(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonMedia();
    
		public static IQueryable<PersonNames> GetPersonNames(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonNames();
    
		public static IQueryable<PhoneTypes> GetPhoneTypes(this IQueryable<ForeignPhoneNumbers> query) => query.PhoneTypes();
    
		public static IQueryable<PrimaryPersonPhoneNumber> GetPrimaryPersonPhoneNumber(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonPhoneNumbers().PrimaryPersonPhoneNumber();
    
		public static IQueryable<PersonPhoneNumbers> GetPersonPhoneNumbers(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().PersonPhoneNumbers();
    
		public static IQueryable<Persons_EmergencyContact> GetPersons_EmergencyContact(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons_EmergencyContact();
    
		public static IQueryable<Persons_NextOfKin> GetPersons_NextOfKin(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons_NextOfKin();
    
		public static IQueryable<UserSignIn> GetUserSignIn(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons().UserSignIn();
    
		public static IQueryable<Persons> GetPersons(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Persons();
    
		public static IQueryable<Sex> GetSex(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().Sex();
    
		public static IQueryable<Allergies> GetAllergies(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientAllergies().Allergies();
    
		public static IQueryable<PatientAllergies> GetPatientAllergies(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientAllergies();
    
		public static IQueryable<PatientReligon> GetPatientReligon(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PatientReligon();
    
		public static IQueryable<PersonCountryOfResidence> GetPersonCountryOfResidence(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PersonCountryOfResidence();
    
		public static IQueryable<Occupations> GetOccupations(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PersonJob().Occupations();
    
		public static IQueryable<PersonJob> GetPersonJob(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PersonJob();
    
		public static IQueryable<PersonMaritalStatus> GetPersonMaritalStatus(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient().PersonMaritalStatus();
    
		public static IQueryable<Persons_Patient> GetPersons_Patient(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_Patient();
    
		public static IQueryable<BoatInfo> GetBoatInfo(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().BoatInfo();
    
		public static IQueryable<NonResidentCompanyInfo> GetNonResidentCompanyInfo(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().NonResidentCompanyInfo();
    
		public static IQueryable<Organisations_Hotels> GetOrganisations_Hotels(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().NonResidentHotelInfo().Organisations_Hotels();
    
		public static IQueryable<NonResidentHotelInfo> GetNonResidentHotelInfo(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().NonResidentHotelInfo();
    
		public static IQueryable<Persons_ArrivalDepartureInfo> GetPersons_ArrivalDepartureInfo(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().Persons_ArrivalDepartureInfo();
    
		public static IQueryable<StudentInfo> GetStudentInfo(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient().StudentInfo();
    
		public static IQueryable<Persons_NonResidentPatient> GetPersons_NonResidentPatient(this IQueryable<ForeignPhoneNumbers> query) => query.Persons_NonResidentPatient();
    
		public static IQueryable<ForeignPhoneNumbers> GetForeignPhoneNumbers(this IQueryable<ForeignPhoneNumbers> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<ForeignPhoneNumbers> ForeignPhoneNumbers(this IQueryable<Persons_NonResidentPatient> persons_nonresidentpatient) => persons_nonresidentpatient.SelectMany(x => x.ForeignPhoneNumbers);
				public static IQueryable<Persons_NonResidentPatient> Persons_NonResidentPatient(this IQueryable<ForeignPhoneNumbers> query) => query.Select(x => x.Persons_NonResidentPatient);
				//public static IQueryable<ForeignPhoneNumbers> ForeignPhoneNumbers(this IQueryable<PhoneTypes> phonetypes) => phonetypes.SelectMany(x => x.ForeignPhoneNumbers);
				public static IQueryable<PhoneTypes> PhoneTypes(this IQueryable<ForeignPhoneNumbers> query) => query.Select(x => x.PhoneTypes);
	}
}
