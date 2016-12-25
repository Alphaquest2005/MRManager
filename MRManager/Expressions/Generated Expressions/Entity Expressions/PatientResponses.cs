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
	public static partial class PatientResponsesExpressions
	{
		public static IQueryable<PatientResponses> GetPatientResponsesById(this IQueryable<PatientResponses> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Syntoms> GetSyntoms(this IQueryable<PatientResponses> query) => query.PatientSyntoms().Syntoms();
    
		public static IQueryable<Cities> GetCities(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCities().Cities();
    
		public static IQueryable<AddressCities> GetAddressCities(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCities();
    
		public static IQueryable<Countries> GetCountries(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PersonCountryOfResidence().Countries();
    
		public static IQueryable<AddressCountries> GetAddressCountries(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressCountries();
    
		public static IQueryable<AddressLines> GetAddressLines(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressLines();
    
		public static IQueryable<Parishes> GetParishes(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressParishes().Parishes();
    
		public static IQueryable<AddressParishes> GetAddressParishes(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressParishes();
    
		public static IQueryable<States> GetStates(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressStates().States();
    
		public static IQueryable<AddressStates> GetAddressStates(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressStates();
    
		public static IQueryable<ZipCodes> GetZipCodes(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressZipCodes().ZipCodes();
    
		public static IQueryable<AddressZipCodes> GetAddressZipCodes(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().AddressZipCodes();
    
		public static IQueryable<BoatInfo> GetBoatInfo(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient().BoatInfo();
    
		public static IQueryable<PhoneTypes> GetPhoneTypes(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers().PhoneTypes();
    
		public static IQueryable<ForeignPhoneNumbers> GetForeignPhoneNumbers(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient().ForeignPhoneNumbers();
    
		public static IQueryable<NonResidentCompanyInfo> GetNonResidentCompanyInfo(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentCompanyInfo();
    
		public static IQueryable<Organisations> GetOrganisations(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PersonJob().Organisations();
    
		public static IQueryable<Organisations_Hotels> GetOrganisations_Hotels(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo().Organisations_Hotels();
    
		public static IQueryable<NonResidentHotelInfo> GetNonResidentHotelInfo(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo();
    
		public static IQueryable<Persons_ArrivalDepartureInfo> GetPersons_ArrivalDepartureInfo(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient().Persons_ArrivalDepartureInfo();
    
		public static IQueryable<StudentInfo> GetStudentInfo(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient().StudentInfo();
    
		public static IQueryable<Persons_NonResidentPatient> GetPersons_NonResidentPatient(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient();
    
		public static IQueryable<AddressTypes> GetAddressTypes(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().AddressTypes();
    
		public static IQueryable<ForeignAddresses> GetForeignAddresses(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NonResidentPatient().ForeignAddresses();
    
		public static IQueryable<OrganisationAddress> GetOrganisationAddress(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses().OrganisationAddress();
    
		public static IQueryable<Addresses> GetAddresses(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().Addresses();
    
		public static IQueryable<PrimaryPersonAddress> GetPrimaryPersonAddress(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses().PrimaryPersonAddress();
    
		public static IQueryable<PersonAddresses> GetPersonAddresses(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonAddresses();
    
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddress(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonEmailAddress();
    
		public static IQueryable<MediaTypes> GetMediaTypes(this IQueryable<PatientResponses> query) => query.ResponseImages().Media().MediaTypes();
    
		public static IQueryable<DefaultImages> GetDefaultImages(this IQueryable<PatientResponses> query) => query.ResponseImages().Media().DefaultImages();
    
		public static IQueryable<ResponseImages> GetResponseImages(this IQueryable<PatientResponses> query) => query.ResponseImages();
    
		public static IQueryable<Media> GetMedia(this IQueryable<PatientResponses> query) => query.ResponseImages().Media();
    
		public static IQueryable<PersonMedia> GetPersonMedia(this IQueryable<PatientResponses> query) => query.ResponseImages().Media().PersonMedia();
    
		public static IQueryable<PersonNames> GetPersonNames(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonNames();
    
		public static IQueryable<PrimaryPersonPhoneNumber> GetPrimaryPersonPhoneNumber(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers().PrimaryPersonPhoneNumber();
    
		public static IQueryable<PersonPhoneNumbers> GetPersonPhoneNumbers(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().PersonPhoneNumbers();
    
		public static IQueryable<PatientDoctor> GetPatientDoctor(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PatientDoctor();
    
		public static IQueryable<Persons_Doctor> GetPersons_Doctor(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Doctor();
    
		public static IQueryable<Persons_EmergencyContact> GetPersons_EmergencyContact(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_EmergencyContact();
    
		public static IQueryable<Persons_NextOfKin> GetPersons_NextOfKin(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons_NextOfKin();
    
		public static IQueryable<UserSignIn> GetUserSignIn(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons().UserSignIn();
    
		public static IQueryable<Units> GetUnits(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns().VitalSigns().BloodPressure().Units();
    
		public static IQueryable<BloodPressure> GetBloodPressure(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns().VitalSigns().BloodPressure();
    
		public static IQueryable<Height> GetHeight(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns().VitalSigns().Height();
    
		public static IQueryable<PatientVisitVitalSigns> GetPatientVisitVitalSigns(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns();
    
		public static IQueryable<Pulse> GetPulse(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns().VitalSigns().Pulse();
    
		public static IQueryable<Respiration> GetRespiration(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns().VitalSigns().Respiration();
    
		public static IQueryable<Temperature> GetTemperature(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns().VitalSigns().Temperature();
    
		public static IQueryable<Weight> GetWeight(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns().VitalSigns().Weight();
    
		public static IQueryable<VitalSigns> GetVitalSigns(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientVisitVitalSigns().VitalSigns();
    
		public static IQueryable<Persons> GetPersons(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Persons();
    
		public static IQueryable<Sex> GetSex(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().Sex();
    
		public static IQueryable<Allergies> GetAllergies(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PatientAllergies().Allergies();
    
		public static IQueryable<PatientAllergies> GetPatientAllergies(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PatientAllergies();
    
		public static IQueryable<PatientReligon> GetPatientReligon(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PatientReligon();
    
		public static IQueryable<PersonCountryOfResidence> GetPersonCountryOfResidence(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PersonCountryOfResidence();
    
		public static IQueryable<Occupations> GetOccupations(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PersonJob().Occupations();
    
		public static IQueryable<PersonJob> GetPersonJob(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PersonJob();
    
		public static IQueryable<PersonMaritalStatus> GetPersonMaritalStatus(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient().PersonMaritalStatus();
    
		public static IQueryable<Persons_Patient> GetPersons_Patient(this IQueryable<PatientResponses> query) => query.PatientVisit().Persons_Patient();
    
		public static IQueryable<Exams> GetExams(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamDetails().Exams();
    
		public static IQueryable<Components> GetComponents(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents().Components();
    
		public static IQueryable<ExamDetailComponents> GetExamDetailComponents(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents();
    
		public static IQueryable<ExamDetails> GetExamDetails(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamDetails();
    
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluid(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamResults_AnioticFluid();
    
		public static IQueryable<AssignedDating> GetAssignedDating(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates().AssignedDating();
    
		public static IQueryable<ExamResults_FetalDates> GetExamResults_FetalDates(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates();
    
		public static IQueryable<ResultFieldNames> GetResultFieldNames(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues().ResultFieldNames();
    
		public static IQueryable<ExamResults_SimpleValues> GetExamResults_SimpleValues(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues();
    
		public static IQueryable<ExamResults_UmbilicalArtery> GetExamResults_UmbilicalArtery(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults().ExamResults_UmbilicalArtery();
    
		public static IQueryable<ExamResults> GetExamResults(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults().ExamResults();
    
		public static IQueryable<PatientResults> GetPatientResults(this IQueryable<PatientResponses> query) => query.PatientVisit().PatientResults();
    
		public static IQueryable<PatientVisit> GetPatientVisit(this IQueryable<PatientResponses> query) => query.PatientVisit();
    
		public static IQueryable<PatientSyntoms> GetPatientSyntoms(this IQueryable<PatientResponses> query) => query.PatientSyntoms();
    
		public static IQueryable<Interviews> GetInterviews(this IQueryable<PatientResponses> query) => query.Questions().Interviews();
    
		public static IQueryable<Response> GetResponse(this IQueryable<PatientResponses> query) => query.Response();
    
		public static IQueryable<CarePlan> GetCarePlan(this IQueryable<PatientResponses> query) => query.Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans().CarePlan();
    
		public static IQueryable<ResponseSuggestions_CarePlans> GetResponseSuggestions_CarePlans(this IQueryable<PatientResponses> query) => query.Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans();
    
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_Interviews(this IQueryable<PatientResponses> query) => query.Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_Interviews();
    
		public static IQueryable<ResponseSuggestions> GetResponseSuggestions(this IQueryable<PatientResponses> query) => query.Questions().ResponseOptions().ResponseSuggestions();
    
		public static IQueryable<ResponseOptions> GetResponseOptions(this IQueryable<PatientResponses> query) => query.Questions().ResponseOptions();
    
		public static IQueryable<Questions> GetQuestions(this IQueryable<PatientResponses> query) => query.Questions();
    
		public static IQueryable<PatientResponses> GetPatientResponses(this IQueryable<PatientResponses> query) => query;

			// Child Properties
				public static IQueryable<Response> Response(this IQueryable<PatientResponses> patientresponses) => patientresponses.SelectMany(x => x.Response);
				public static IQueryable<Response> Response(this IQueryable<PatientResponses> patientresponses, int id) => patientresponses.Where(x => x.Id == id).SelectMany(x => x.Response);
				public static IQueryable<ResponseImages> ResponseImages(this IQueryable<PatientResponses> patientresponses) => patientresponses.SelectMany(x => x.ResponseImages);
				public static IQueryable<ResponseImages> ResponseImages(this IQueryable<PatientResponses> patientresponses, int id) => patientresponses.Where(x => x.Id == id).SelectMany(x => x.ResponseImages);
			//Parent Properties
				//public static IQueryable<PatientResponses> PatientResponses(this IQueryable<PatientSyntoms> patientsyntoms) => patientsyntoms.SelectMany(x => x.PatientResponses);
				public static IQueryable<PatientSyntoms> PatientSyntoms(this IQueryable<PatientResponses> query) => query.Select(x => x.PatientSyntoms);
				//public static IQueryable<PatientResponses> PatientResponses(this IQueryable<Questions> questions) => questions.SelectMany(x => x.PatientResponses);
				public static IQueryable<Questions> Questions(this IQueryable<PatientResponses> query) => query.Select(x => x.Questions);
				//public static IQueryable<PatientResponses> PatientResponses(this IQueryable<PatientVisit> patientvisit) => patientvisit.SelectMany(x => x.PatientResponses);
				public static IQueryable<PatientVisit> PatientVisit(this IQueryable<PatientResponses> query) => query.Select(x => x.PatientVisit);
	}
}
