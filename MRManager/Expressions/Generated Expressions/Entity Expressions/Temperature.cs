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
	public static partial class TemperatureExpressions
	{
		public static IQueryable<Temperature> GetTemperatureById(this IQueryable<Temperature> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Cities> GetCities(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressCities().Cities();
    
		public static IQueryable<AddressCities> GetAddressCities(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressCities();
    
		public static IQueryable<Countries> GetCountries(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PersonCountryOfResidence().Countries();
    
		public static IQueryable<AddressCountries> GetAddressCountries(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressCountries();
    
		public static IQueryable<AddressLines> GetAddressLines(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressLines();
    
		public static IQueryable<Parishes> GetParishes(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressParishes().Parishes();
    
		public static IQueryable<AddressParishes> GetAddressParishes(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressParishes();
    
		public static IQueryable<States> GetStates(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressStates().States();
    
		public static IQueryable<AddressStates> GetAddressStates(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressStates();
    
		public static IQueryable<ZipCodes> GetZipCodes(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressZipCodes().ZipCodes();
    
		public static IQueryable<AddressZipCodes> GetAddressZipCodes(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().AddressZipCodes();
    
		public static IQueryable<Sex> GetSex(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Sex();
    
		public static IQueryable<Allergies> GetAllergies(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PatientAllergies().Allergies();
    
		public static IQueryable<PatientAllergies> GetPatientAllergies(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PatientAllergies();
    
		public static IQueryable<Syntoms> GetSyntoms(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientSyntoms().Syntoms();
    
		public static IQueryable<PatientSyntoms> GetPatientSyntoms(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientSyntoms();
    
		public static IQueryable<Interviews> GetInterviews(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses().Questions().Interviews();
    
		public static IQueryable<Response> GetResponse(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses().Response();
    
		public static IQueryable<CarePlan> GetCarePlan(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans().CarePlan();
    
		public static IQueryable<ResponseSuggestions_CarePlans> GetResponseSuggestions_CarePlans(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_CarePlans();
    
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_Interviews(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions().ResponseSuggestions_Interviews();
    
		public static IQueryable<ResponseSuggestions> GetResponseSuggestions(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses().Questions().ResponseOptions().ResponseSuggestions();
    
		public static IQueryable<ResponseOptions> GetResponseOptions(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses().Questions().ResponseOptions();
    
		public static IQueryable<Questions> GetQuestions(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses().Questions();
    
		public static IQueryable<MediaTypes> GetMediaTypes(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonMedia().Media().MediaTypes();
    
		public static IQueryable<DefaultImages> GetDefaultImages(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonMedia().Media().DefaultImages();
    
		public static IQueryable<PersonMedia> GetPersonMedia(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonMedia();
    
		public static IQueryable<Media> GetMedia(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonMedia().Media();
    
		public static IQueryable<ResponseImages> GetResponseImages(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonMedia().Media().ResponseImages();
    
		public static IQueryable<PatientResponses> GetPatientResponses(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResponses();
    
		public static IQueryable<Exams> GetExams(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamDetails().Exams();
    
		public static IQueryable<Components> GetComponents(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents().Components();
    
		public static IQueryable<ExamDetailComponents> GetExamDetailComponents(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents();
    
		public static IQueryable<ExamDetails> GetExamDetails(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamDetails();
    
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluid(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamResults_AnioticFluid();
    
		public static IQueryable<AssignedDating> GetAssignedDating(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates().AssignedDating();
    
		public static IQueryable<ExamResults_FetalDates> GetExamResults_FetalDates(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates();
    
		public static IQueryable<ResultFieldNames> GetResultFieldNames(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues().ResultFieldNames();
    
		public static IQueryable<ExamResults_SimpleValues> GetExamResults_SimpleValues(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues();
    
		public static IQueryable<ExamResults_UmbilicalArtery> GetExamResults_UmbilicalArtery(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults().ExamResults_UmbilicalArtery();
    
		public static IQueryable<ExamResults> GetExamResults(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults().ExamResults();
    
		public static IQueryable<PatientResults> GetPatientResults(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit().PatientResults();
    
		public static IQueryable<PatientVisitVitalSigns> GetPatientVisitVitalSigns(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns();
    
		public static IQueryable<PatientVisit> GetPatientVisit(this IQueryable<Temperature> query) => query.VitalSigns().PatientVisitVitalSigns().PatientVisit();
    
		public static IQueryable<Persons_Doctor> GetPersons_Doctor(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Doctor();
    
		public static IQueryable<PatientDoctor> GetPatientDoctor(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Doctor().PatientDoctor();
    
		public static IQueryable<PatientReligon> GetPatientReligon(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PatientReligon();
    
		public static IQueryable<PersonCountryOfResidence> GetPersonCountryOfResidence(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PersonCountryOfResidence();
    
		public static IQueryable<Occupations> GetOccupations(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PersonJob().Occupations();
    
		public static IQueryable<Organisations> GetOrganisations(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PersonJob().Organisations();
    
		public static IQueryable<PersonJob> GetPersonJob(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PersonJob();
    
		public static IQueryable<PersonMaritalStatus> GetPersonMaritalStatus(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().PersonMaritalStatus();
    
		public static IQueryable<Persons_EmergencyContact> GetPersons_EmergencyContact(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_EmergencyContact();
    
		public static IQueryable<Persons_NextOfKin> GetPersons_NextOfKin(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_NextOfKin();
    
		public static IQueryable<Persons_Patient> GetPersons_Patient(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient();
    
		public static IQueryable<BoatInfo> GetBoatInfo(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Persons_NonResidentPatient().BoatInfo();
    
		public static IQueryable<PhoneTypes> GetPhoneTypes(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonPhoneNumbers().PhoneTypes();
    
		public static IQueryable<ForeignPhoneNumbers> GetForeignPhoneNumbers(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Persons_NonResidentPatient().ForeignPhoneNumbers();
    
		public static IQueryable<NonResidentCompanyInfo> GetNonResidentCompanyInfo(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Persons_NonResidentPatient().NonResidentCompanyInfo();
    
		public static IQueryable<Organisations_Hotels> GetOrganisations_Hotels(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo().Organisations_Hotels();
    
		public static IQueryable<NonResidentHotelInfo> GetNonResidentHotelInfo(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo();
    
		public static IQueryable<Persons_ArrivalDepartureInfo> GetPersons_ArrivalDepartureInfo(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Persons_NonResidentPatient().Persons_ArrivalDepartureInfo();
    
		public static IQueryable<StudentInfo> GetStudentInfo(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Persons_NonResidentPatient().StudentInfo();
    
		public static IQueryable<Persons_NonResidentPatient> GetPersons_NonResidentPatient(this IQueryable<Temperature> query) => query.VitalSigns().Persons().Persons_Patient().Persons_NonResidentPatient();
    
		public static IQueryable<AddressTypes> GetAddressTypes(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().AddressTypes();
    
		public static IQueryable<ForeignAddresses> GetForeignAddresses(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().ForeignAddresses();
    
		public static IQueryable<OrganisationAddress> GetOrganisationAddress(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses().OrganisationAddress();
    
		public static IQueryable<Addresses> GetAddresses(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().Addresses();
    
		public static IQueryable<PrimaryPersonAddress> GetPrimaryPersonAddress(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses().PrimaryPersonAddress();
    
		public static IQueryable<PersonAddresses> GetPersonAddresses(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonAddresses();
    
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddress(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonEmailAddress();
    
		public static IQueryable<PersonNames> GetPersonNames(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonNames();
    
		public static IQueryable<PrimaryPersonPhoneNumber> GetPrimaryPersonPhoneNumber(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonPhoneNumbers().PrimaryPersonPhoneNumber();
    
		public static IQueryable<PersonPhoneNumbers> GetPersonPhoneNumbers(this IQueryable<Temperature> query) => query.VitalSigns().Persons().PersonPhoneNumbers();
    
		public static IQueryable<UserSignIn> GetUserSignIn(this IQueryable<Temperature> query) => query.VitalSigns().Persons().UserSignIn();
    
		public static IQueryable<Persons> GetPersons(this IQueryable<Temperature> query) => query.VitalSigns().Persons();
    
		public static IQueryable<Units> GetUnits(this IQueryable<Temperature> query) => query.Units();
    
		public static IQueryable<BloodPressure> GetBloodPressure(this IQueryable<Temperature> query) => query.VitalSigns().BloodPressure();
    
		public static IQueryable<Height> GetHeight(this IQueryable<Temperature> query) => query.VitalSigns().Height();
    
		public static IQueryable<Pulse> GetPulse(this IQueryable<Temperature> query) => query.VitalSigns().Pulse();
    
		public static IQueryable<Respiration> GetRespiration(this IQueryable<Temperature> query) => query.VitalSigns().Respiration();
    
		public static IQueryable<Weight> GetWeight(this IQueryable<Temperature> query) => query.VitalSigns().Weight();
    
		public static IQueryable<VitalSigns> GetVitalSigns(this IQueryable<Temperature> query) => query.VitalSigns();
    
		public static IQueryable<Temperature> GetTemperature(this IQueryable<Temperature> query) => query;

			// Child Properties
			//Parent Properties
				//public static IQueryable<Temperature> Temperature(this IQueryable<VitalSigns> vitalsigns) => vitalsigns.Select(x => x.Temperature);
				public static IQueryable<VitalSigns> VitalSigns(this IQueryable<Temperature> query) => query.Select(x => x.VitalSigns);
				//public static IQueryable<Temperature> Temperature(this IQueryable<Units> units) => units.SelectMany(x => x.Temperature);
				public static IQueryable<Units> Units(this IQueryable<Temperature> query) => query.Select(x => x.Units);
	}
}
