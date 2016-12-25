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
	public static partial class QuestionsExpressions
	{
		public static IQueryable<Questions> GetQuestionsById(this IQueryable<Questions> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Interviews> GetInterviews(this IQueryable<Questions> query) => query.Interviews();
    
		public static IQueryable<Syntoms> GetSyntoms(this IQueryable<Questions> query) => query.PatientResponses().PatientSyntoms().Syntoms();
    
		public static IQueryable<Cities> GetCities(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCities().Cities();
    
		public static IQueryable<AddressCities> GetAddressCities(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCities();
    
		public static IQueryable<Countries> GetCountries(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PersonCountryOfResidence().Countries();
    
		public static IQueryable<AddressCountries> GetAddressCountries(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCountries();
    
		public static IQueryable<AddressLines> GetAddressLines(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressLines();
    
		public static IQueryable<Parishes> GetParishes(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressParishes().Parishes();
    
		public static IQueryable<AddressParishes> GetAddressParishes(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressParishes();
    
		public static IQueryable<States> GetStates(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressStates().States();
    
		public static IQueryable<AddressStates> GetAddressStates(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressStates();
    
		public static IQueryable<ZipCodes> GetZipCodes(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressZipCodes().ZipCodes();
    
		public static IQueryable<AddressZipCodes> GetAddressZipCodes(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressZipCodes();
    
		public static IQueryable<BoatInfo> GetBoatInfo(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().BoatInfo();
    
		public static IQueryable<PhoneTypes> GetPhoneTypes(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers().PhoneTypes();
    
		public static IQueryable<ForeignPhoneNumbers> GetForeignPhoneNumbers(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().ForeignPhoneNumbers();
    
		public static IQueryable<NonResidentCompanyInfo> GetNonResidentCompanyInfo(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentCompanyInfo();
    
		public static IQueryable<Organisations> GetOrganisations(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PersonJob().Organisations();
    
		public static IQueryable<Organisations_Hotels> GetOrganisations_Hotels(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo().Organisations_Hotels();
    
		public static IQueryable<NonResidentHotelInfo> GetNonResidentHotelInfo(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo();
    
		public static IQueryable<Persons_ArrivalDepartureInfo> GetPersons_ArrivalDepartureInfo(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().Persons_ArrivalDepartureInfo();
    
		public static IQueryable<StudentInfo> GetStudentInfo(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().StudentInfo();
    
		public static IQueryable<Persons_NonResidentPatient> GetPersons_NonResidentPatient(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient();
    
		public static IQueryable<AddressTypes> GetAddressTypes(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().AddressTypes();
    
		public static IQueryable<ForeignAddresses> GetForeignAddresses(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().ForeignAddresses();
    
		public static IQueryable<OrganisationAddress> GetOrganisationAddress(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().OrganisationAddress();
    
		public static IQueryable<Addresses> GetAddresses(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses();
    
		public static IQueryable<PrimaryPersonAddress> GetPrimaryPersonAddress(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses().PrimaryPersonAddress();
    
		public static IQueryable<PersonAddresses> GetPersonAddresses(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonAddresses();
    
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddress(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonEmailAddress();
    
		public static IQueryable<MediaTypes> GetMediaTypes(this IQueryable<Questions> query) => query.PatientResponses().ResponseImages().Media().MediaTypes();
    
		public static IQueryable<DefaultImages> GetDefaultImages(this IQueryable<Questions> query) => query.PatientResponses().ResponseImages().Media().DefaultImages();
    
		public static IQueryable<ResponseImages> GetResponseImages(this IQueryable<Questions> query) => query.PatientResponses().ResponseImages();
    
		public static IQueryable<Media> GetMedia(this IQueryable<Questions> query) => query.PatientResponses().ResponseImages().Media();
    
		public static IQueryable<PersonMedia> GetPersonMedia(this IQueryable<Questions> query) => query.PatientResponses().ResponseImages().Media().PersonMedia();
    
		public static IQueryable<PersonNames> GetPersonNames(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonNames();
    
		public static IQueryable<PrimaryPersonPhoneNumber> GetPrimaryPersonPhoneNumber(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers().PrimaryPersonPhoneNumber();
    
		public static IQueryable<PersonPhoneNumbers> GetPersonPhoneNumbers(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers();
    
		public static IQueryable<PatientDoctor> GetPatientDoctor(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PatientDoctor();
    
		public static IQueryable<Persons_Doctor> GetPersons_Doctor(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Doctor();
    
		public static IQueryable<Persons_EmergencyContact> GetPersons_EmergencyContact(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_EmergencyContact();
    
		public static IQueryable<Persons_NextOfKin> GetPersons_NextOfKin(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons_NextOfKin();
    
		public static IQueryable<UserSignIn> GetUserSignIn(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons().UserSignIn();
    
		public static IQueryable<Units> GetUnits(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().BloodPressure().Units();
    
		public static IQueryable<BloodPressure> GetBloodPressure(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().BloodPressure();
    
		public static IQueryable<Height> GetHeight(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Height();
    
		public static IQueryable<PatientVisitVitalSigns> GetPatientVisitVitalSigns(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns();
    
		public static IQueryable<Pulse> GetPulse(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Pulse();
    
		public static IQueryable<Respiration> GetRespiration(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Respiration();
    
		public static IQueryable<Temperature> GetTemperature(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Temperature();
    
		public static IQueryable<Weight> GetWeight(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Weight();
    
		public static IQueryable<VitalSigns> GetVitalSigns(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns();
    
		public static IQueryable<Persons> GetPersons(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Persons();
    
		public static IQueryable<Sex> GetSex(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().Sex();
    
		public static IQueryable<Allergies> GetAllergies(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PatientAllergies().Allergies();
    
		public static IQueryable<PatientAllergies> GetPatientAllergies(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PatientAllergies();
    
		public static IQueryable<PatientReligon> GetPatientReligon(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PatientReligon();
    
		public static IQueryable<PersonCountryOfResidence> GetPersonCountryOfResidence(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PersonCountryOfResidence();
    
		public static IQueryable<Occupations> GetOccupations(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PersonJob().Occupations();
    
		public static IQueryable<PersonJob> GetPersonJob(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PersonJob();
    
		public static IQueryable<PersonMaritalStatus> GetPersonMaritalStatus(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient().PersonMaritalStatus();
    
		public static IQueryable<Persons_Patient> GetPersons_Patient(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().Persons_Patient();
    
		public static IQueryable<Exams> GetExams(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamDetails().Exams();
    
		public static IQueryable<Components> GetComponents(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents().Components();
    
		public static IQueryable<ExamDetailComponents> GetExamDetailComponents(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents();
    
		public static IQueryable<ExamDetails> GetExamDetails(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamDetails();
    
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluid(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_AnioticFluid();
    
		public static IQueryable<AssignedDating> GetAssignedDating(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates().AssignedDating();
    
		public static IQueryable<ExamResults_FetalDates> GetExamResults_FetalDates(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates();
    
		public static IQueryable<ResultFieldNames> GetResultFieldNames(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues().ResultFieldNames();
    
		public static IQueryable<ExamResults_SimpleValues> GetExamResults_SimpleValues(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues();
    
		public static IQueryable<ExamResults_UmbilicalArtery> GetExamResults_UmbilicalArtery(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_UmbilicalArtery();
    
		public static IQueryable<ExamResults> GetExamResults(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults().ExamResults();
    
		public static IQueryable<PatientResults> GetPatientResults(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit().PatientResults();
    
		public static IQueryable<PatientVisit> GetPatientVisit(this IQueryable<Questions> query) => query.PatientResponses().PatientVisit();
    
		public static IQueryable<PatientSyntoms> GetPatientSyntoms(this IQueryable<Questions> query) => query.PatientResponses().PatientSyntoms();
    
		public static IQueryable<CarePlan> GetCarePlan(this IQueryable<Questions> query) => query.ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans().CarePlan();
    
		public static IQueryable<ResponseSuggestions_CarePlans> GetResponseSuggestions_CarePlans(this IQueryable<Questions> query) => query.ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans();
    
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_Interviews(this IQueryable<Questions> query) => query.ResponseOptions().ResponseSuggestions().ResponseSuggestions_Interviews();
    
		public static IQueryable<ResponseSuggestions> GetResponseSuggestions(this IQueryable<Questions> query) => query.ResponseOptions().ResponseSuggestions();
    
		public static IQueryable<ResponseOptions> GetResponseOptions(this IQueryable<Questions> query) => query.ResponseOptions();
    
		public static IQueryable<Response> GetResponse(this IQueryable<Questions> query) => query.PatientResponses().Response();
    
		public static IQueryable<PatientResponses> GetPatientResponses(this IQueryable<Questions> query) => query.PatientResponses();
    
		public static IQueryable<Questions> GetQuestions(this IQueryable<Questions> query) => query;

			// Child Properties
				public static IQueryable<PatientResponses> PatientResponses(this IQueryable<Questions> questions) => questions.SelectMany(x => x.PatientResponses);
				public static IQueryable<PatientResponses> PatientResponses(this IQueryable<Questions> questions, int id) => questions.Where(x => x.Id == id).SelectMany(x => x.PatientResponses);
				public static IQueryable<ResponseOptions> ResponseOptions(this IQueryable<Questions> questions) => questions.SelectMany(x => x.ResponseOptions);
				public static IQueryable<ResponseOptions> ResponseOptions(this IQueryable<Questions> questions, int id) => questions.Where(x => x.Id == id).SelectMany(x => x.ResponseOptions);
			//Parent Properties
				//public static IQueryable<Questions> Questions(this IQueryable<Interviews> interviews) => interviews.SelectMany(x => x.Questions);
				public static IQueryable<Interviews> Interviews(this IQueryable<Questions> query) => query.Select(x => x.Interviews);
	}
}
