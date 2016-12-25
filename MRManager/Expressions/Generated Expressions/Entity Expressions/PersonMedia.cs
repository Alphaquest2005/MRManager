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
	public static partial class PersonMediaExpressions
	{
		public static IQueryable<PersonMedia> GetPersonMediaById(this IQueryable<PersonMedia> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<MediaTypes> GetMediaTypes(this IQueryable<PersonMedia> query) => query.Media().MediaTypes();
    
		public static IQueryable<DefaultImages> GetDefaultImages(this IQueryable<PersonMedia> query) => query.Media().DefaultImages();
    
		public static IQueryable<Syntoms> GetSyntoms(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().PatientSyntoms().Syntoms();
    
		public static IQueryable<Cities> GetCities(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressCities().Cities();
    
		public static IQueryable<AddressCities> GetAddressCities(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressCities();
    
		public static IQueryable<Countries> GetCountries(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PersonCountryOfResidence().Countries();
    
		public static IQueryable<AddressCountries> GetAddressCountries(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressCountries();
    
		public static IQueryable<AddressLines> GetAddressLines(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressLines();
    
		public static IQueryable<Parishes> GetParishes(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressParishes().Parishes();
    
		public static IQueryable<AddressParishes> GetAddressParishes(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressParishes();
    
		public static IQueryable<States> GetStates(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressStates().States();
    
		public static IQueryable<AddressStates> GetAddressStates(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressStates();
    
		public static IQueryable<ZipCodes> GetZipCodes(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressZipCodes().ZipCodes();
    
		public static IQueryable<AddressZipCodes> GetAddressZipCodes(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().AddressZipCodes();
    
		public static IQueryable<BoatInfo> GetBoatInfo(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Persons_NonResidentPatient().BoatInfo();
    
		public static IQueryable<PhoneTypes> GetPhoneTypes(this IQueryable<PersonMedia> query) => query.Persons().PersonPhoneNumbers().PhoneTypes();
    
		public static IQueryable<ForeignPhoneNumbers> GetForeignPhoneNumbers(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Persons_NonResidentPatient().ForeignPhoneNumbers();
    
		public static IQueryable<NonResidentCompanyInfo> GetNonResidentCompanyInfo(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Persons_NonResidentPatient().NonResidentCompanyInfo();
    
		public static IQueryable<Organisations> GetOrganisations(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PersonJob().Organisations();
    
		public static IQueryable<Organisations_Hotels> GetOrganisations_Hotels(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo().Organisations_Hotels();
    
		public static IQueryable<NonResidentHotelInfo> GetNonResidentHotelInfo(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo();
    
		public static IQueryable<Persons_ArrivalDepartureInfo> GetPersons_ArrivalDepartureInfo(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Persons_NonResidentPatient().Persons_ArrivalDepartureInfo();
    
		public static IQueryable<StudentInfo> GetStudentInfo(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Persons_NonResidentPatient().StudentInfo();
    
		public static IQueryable<Persons_NonResidentPatient> GetPersons_NonResidentPatient(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Persons_NonResidentPatient();
    
		public static IQueryable<AddressTypes> GetAddressTypes(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().AddressTypes();
    
		public static IQueryable<ForeignAddresses> GetForeignAddresses(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().ForeignAddresses();
    
		public static IQueryable<OrganisationAddress> GetOrganisationAddress(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses().OrganisationAddress();
    
		public static IQueryable<Addresses> GetAddresses(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().Addresses();
    
		public static IQueryable<PrimaryPersonAddress> GetPrimaryPersonAddress(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses().PrimaryPersonAddress();
    
		public static IQueryable<PersonAddresses> GetPersonAddresses(this IQueryable<PersonMedia> query) => query.Persons().PersonAddresses();
    
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddress(this IQueryable<PersonMedia> query) => query.Persons().PersonEmailAddress();
    
		public static IQueryable<PersonNames> GetPersonNames(this IQueryable<PersonMedia> query) => query.Persons().PersonNames();
    
		public static IQueryable<PrimaryPersonPhoneNumber> GetPrimaryPersonPhoneNumber(this IQueryable<PersonMedia> query) => query.Persons().PersonPhoneNumbers().PrimaryPersonPhoneNumber();
    
		public static IQueryable<PersonPhoneNumbers> GetPersonPhoneNumbers(this IQueryable<PersonMedia> query) => query.Persons().PersonPhoneNumbers();
    
		public static IQueryable<PatientDoctor> GetPatientDoctor(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientDoctor();
    
		public static IQueryable<Persons_Doctor> GetPersons_Doctor(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor();
    
		public static IQueryable<Persons_EmergencyContact> GetPersons_EmergencyContact(this IQueryable<PersonMedia> query) => query.Persons().Persons_EmergencyContact();
    
		public static IQueryable<Persons_NextOfKin> GetPersons_NextOfKin(this IQueryable<PersonMedia> query) => query.Persons().Persons_NextOfKin();
    
		public static IQueryable<UserSignIn> GetUserSignIn(this IQueryable<PersonMedia> query) => query.Persons().UserSignIn();
    
		public static IQueryable<Units> GetUnits(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns().BloodPressure().Units();
    
		public static IQueryable<BloodPressure> GetBloodPressure(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns().BloodPressure();
    
		public static IQueryable<Height> GetHeight(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns().Height();
    
		public static IQueryable<PatientVisitVitalSigns> GetPatientVisitVitalSigns(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns().PatientVisitVitalSigns();
    
		public static IQueryable<Pulse> GetPulse(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns().Pulse();
    
		public static IQueryable<Respiration> GetRespiration(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns().Respiration();
    
		public static IQueryable<Temperature> GetTemperature(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns().Temperature();
    
		public static IQueryable<Weight> GetWeight(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns().Weight();
    
		public static IQueryable<VitalSigns> GetVitalSigns(this IQueryable<PersonMedia> query) => query.Persons().VitalSigns();
    
		public static IQueryable<Persons> GetPersons(this IQueryable<PersonMedia> query) => query.Persons();
    
		public static IQueryable<Sex> GetSex(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().Sex();
    
		public static IQueryable<Allergies> GetAllergies(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PatientAllergies().Allergies();
    
		public static IQueryable<PatientAllergies> GetPatientAllergies(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PatientAllergies();
    
		public static IQueryable<PatientReligon> GetPatientReligon(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PatientReligon();
    
		public static IQueryable<PersonCountryOfResidence> GetPersonCountryOfResidence(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PersonCountryOfResidence();
    
		public static IQueryable<Occupations> GetOccupations(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PersonJob().Occupations();
    
		public static IQueryable<PersonJob> GetPersonJob(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PersonJob();
    
		public static IQueryable<PersonMaritalStatus> GetPersonMaritalStatus(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient().PersonMaritalStatus();
    
		public static IQueryable<Persons_Patient> GetPersons_Patient(this IQueryable<PersonMedia> query) => query.Persons().Persons_Patient();
    
		public static IQueryable<Exams> GetExams(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamDetails().Exams();
    
		public static IQueryable<Components> GetComponents(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents().Components();
    
		public static IQueryable<ExamDetailComponents> GetExamDetailComponents(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents();
    
		public static IQueryable<ExamDetails> GetExamDetails(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamDetails();
    
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluid(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamResults_AnioticFluid();
    
		public static IQueryable<AssignedDating> GetAssignedDating(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates().AssignedDating();
    
		public static IQueryable<ExamResults_FetalDates> GetExamResults_FetalDates(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates();
    
		public static IQueryable<ResultFieldNames> GetResultFieldNames(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues().ResultFieldNames();
    
		public static IQueryable<ExamResults_SimpleValues> GetExamResults_SimpleValues(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues();
    
		public static IQueryable<ExamResults_UmbilicalArtery> GetExamResults_UmbilicalArtery(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults().ExamResults_UmbilicalArtery();
    
		public static IQueryable<ExamResults> GetExamResults(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults().ExamResults();
    
		public static IQueryable<PatientResults> GetPatientResults(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit().PatientResults();
    
		public static IQueryable<PatientVisit> GetPatientVisit(this IQueryable<PersonMedia> query) => query.Persons().Persons_Doctor().PatientVisit();
    
		public static IQueryable<PatientSyntoms> GetPatientSyntoms(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().PatientSyntoms();
    
		public static IQueryable<Interviews> GetInterviews(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().Questions().Interviews();
    
		public static IQueryable<Response> GetResponse(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().Response();
    
		public static IQueryable<CarePlan> GetCarePlan(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans().CarePlan();
    
		public static IQueryable<ResponseSuggestions_CarePlans> GetResponseSuggestions_CarePlans(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans();
    
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_Interviews(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_Interviews();
    
		public static IQueryable<ResponseSuggestions> GetResponseSuggestions(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().Questions().ResponseOptions().ResponseSuggestions();
    
		public static IQueryable<ResponseOptions> GetResponseOptions(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().Questions().ResponseOptions();
    
		public static IQueryable<Questions> GetQuestions(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses().Questions();
    
		public static IQueryable<PatientResponses> GetPatientResponses(this IQueryable<PersonMedia> query) => query.Media().ResponseImages().PatientResponses();
    
		public static IQueryable<ResponseImages> GetResponseImages(this IQueryable<PersonMedia> query) => query.Media().ResponseImages();
    
		public static IQueryable<Media> GetMedia(this IQueryable<PersonMedia> query) => query.Media();
    
		public static IQueryable<PersonMedia> GetPersonMedia(this IQueryable<PersonMedia> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<PersonMedia> PersonMedia(this IQueryable<Media> media) => media.SelectMany(x => x.PersonMedia);
				public static IQueryable<Media> Media(this IQueryable<PersonMedia> query) => query.Select(x => x.Media);
				//public static IQueryable<PersonMedia> PersonMedia(this IQueryable<Persons> persons) => persons.SelectMany(x => x.PersonMedia);
				public static IQueryable<Persons> Persons(this IQueryable<PersonMedia> query) => query.Select(x => x.Persons);
	}
}
