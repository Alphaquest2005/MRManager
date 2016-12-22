﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.Linq;
using EF.Entities;

namespace Entity.Expressions
{
	public static partial class ResponseOptionsExpressions
	{
		public static IQueryable<ResponseOptions> GetResponseOptionsById(this IQueryable<ResponseOptions> query, int Id) => query.Where(x => x.Id == Id);


// Get Leaf Properties
    
		public static IQueryable<Interviews> GetInterviews(this IQueryable<ResponseOptions> query) => query.Questions().Interviews();
    
		public static IQueryable<Cities> GetCities(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressCities().Cities();
    
		public static IQueryable<AddressCities> GetAddressCities(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressCities();
    
		public static IQueryable<Countries> GetCountries(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PersonCountryOfResidence().Countries();
    
		public static IQueryable<AddressCountries> GetAddressCountries(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressCountries();
    
		public static IQueryable<AddressLines> GetAddressLines(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressLines();
    
		public static IQueryable<Parishes> GetParishes(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressParishes().Parishes();
    
		public static IQueryable<AddressParishes> GetAddressParishes(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressParishes();
    
		public static IQueryable<States> GetStates(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressStates().States();
    
		public static IQueryable<AddressStates> GetAddressStates(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressStates();
    
		public static IQueryable<ZipCodes> GetZipCodes(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressZipCodes().ZipCodes();
    
		public static IQueryable<AddressZipCodes> GetAddressZipCodes(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().AddressZipCodes();
    
		public static IQueryable<AddressTypes> GetAddressTypes(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().AddressTypes();
    
		public static IQueryable<Sex> GetSex(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Sex();
    
		public static IQueryable<Allergies> GetAllergies(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PatientAllergies().Allergies();
    
		public static IQueryable<PatientAllergies> GetPatientAllergies(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PatientAllergies();
    
		public static IQueryable<PatientDoctor> GetPatientDoctor(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().PatientDoctor();
    
		public static IQueryable<PatientReligon> GetPatientReligon(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PatientReligon();
    
		public static IQueryable<PersonCountryOfResidence> GetPersonCountryOfResidence(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PersonCountryOfResidence();
    
		public static IQueryable<Occupations> GetOccupations(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PersonJob().Occupations();
    
		public static IQueryable<Organisations> GetOrganisations(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PersonJob().Organisations();
    
		public static IQueryable<PersonJob> GetPersonJob(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PersonJob();
    
		public static IQueryable<PersonMaritalStatus> GetPersonMaritalStatus(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().PersonMaritalStatus();
    
		public static IQueryable<Persons_EmergencyContact> GetPersons_EmergencyContact(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_EmergencyContact();
    
		public static IQueryable<Persons_NextOfKin> GetPersons_NextOfKin(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NextOfKin();
    
		public static IQueryable<Persons_Patient> GetPersons_Patient(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient();
    
		public static IQueryable<BoatInfo> GetBoatInfo(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().BoatInfo();
    
		public static IQueryable<PhoneTypes> GetPhoneTypes(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonPhoneNumbers().PhoneTypes();
    
		public static IQueryable<ForeignPhoneNumbers> GetForeignPhoneNumbers(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().ForeignPhoneNumbers();
    
		public static IQueryable<NonResidentCompanyInfo> GetNonResidentCompanyInfo(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentCompanyInfo();
    
		public static IQueryable<Organisations_Hotels> GetOrganisations_Hotels(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo().Organisations_Hotels();
    
		public static IQueryable<NonResidentHotelInfo> GetNonResidentHotelInfo(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().NonResidentHotelInfo();
    
		public static IQueryable<Persons_ArrivalDepartureInfo> GetPersons_ArrivalDepartureInfo(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().Persons_ArrivalDepartureInfo();
    
		public static IQueryable<StudentInfo> GetStudentInfo(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().StudentInfo();
    
		public static IQueryable<Persons_NonResidentPatient> GetPersons_NonResidentPatient(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient();
    
		public static IQueryable<ForeignAddresses> GetForeignAddresses(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Patient().Persons_NonResidentPatient().ForeignAddresses();
    
		public static IQueryable<OrganisationAddress> GetOrganisationAddress(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses().OrganisationAddress();
    
		public static IQueryable<Addresses> GetAddresses(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().Addresses();
    
		public static IQueryable<PrimaryPersonAddress> GetPrimaryPersonAddress(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses().PrimaryPersonAddress();
    
		public static IQueryable<PersonAddresses> GetPersonAddresses(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonAddresses();
    
		public static IQueryable<PersonEmailAddress> GetPersonEmailAddress(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonEmailAddress();
    
		public static IQueryable<MediaTypes> GetMediaTypes(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().ResponseImages().Media().MediaTypes();
    
		public static IQueryable<DefaultImages> GetDefaultImages(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().ResponseImages().Media().DefaultImages();
    
		public static IQueryable<ResponseImages> GetResponseImages(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().ResponseImages();
    
		public static IQueryable<Media> GetMedia(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().ResponseImages().Media();
    
		public static IQueryable<PersonMedia> GetPersonMedia(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().ResponseImages().Media().PersonMedia();
    
		public static IQueryable<PersonNames> GetPersonNames(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonNames();
    
		public static IQueryable<PrimaryPersonPhoneNumber> GetPrimaryPersonPhoneNumber(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonPhoneNumbers().PrimaryPersonPhoneNumber();
    
		public static IQueryable<PersonPhoneNumbers> GetPersonPhoneNumbers(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons().PersonPhoneNumbers();
    
		public static IQueryable<Units> GetUnits(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().BloodPressure().Units();
    
		public static IQueryable<BloodPressure> GetBloodPressure(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().BloodPressure();
    
		public static IQueryable<Height> GetHeight(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Height();
    
		public static IQueryable<PatientVisitVitalSigns> GetPatientVisitVitalSigns(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns();
    
		public static IQueryable<Pulse> GetPulse(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Pulse();
    
		public static IQueryable<Respiration> GetRespiration(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Respiration();
    
		public static IQueryable<Temperature> GetTemperature(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Temperature();
    
		public static IQueryable<Weight> GetWeight(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns().Weight();
    
		public static IQueryable<VitalSigns> GetVitalSigns(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientVisitVitalSigns().VitalSigns();
    
		public static IQueryable<Persons> GetPersons(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor().Persons();
    
		public static IQueryable<Persons_Doctor> GetPersons_Doctor(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().Persons_Doctor();
    
		public static IQueryable<Exams> GetExams(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamDetails().Exams();
    
		public static IQueryable<Components> GetComponents(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents().Components();
    
		public static IQueryable<ExamDetailComponents> GetExamDetailComponents(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamDetails().ExamDetailComponents();
    
		public static IQueryable<ExamDetails> GetExamDetails(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamDetails();
    
		public static IQueryable<ExamResults_AnioticFluid> GetExamResults_AnioticFluid(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_AnioticFluid();
    
		public static IQueryable<AssignedDating> GetAssignedDating(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates().AssignedDating();
    
		public static IQueryable<ExamResults_FetalDates> GetExamResults_FetalDates(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_FetalDates();
    
		public static IQueryable<ResultFieldNames> GetResultFieldNames(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues().ResultFieldNames();
    
		public static IQueryable<ExamResults_SimpleValues> GetExamResults_SimpleValues(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_SimpleValues();
    
		public static IQueryable<ExamResults_UmbilicalArtery> GetExamResults_UmbilicalArtery(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults().ExamResults_UmbilicalArtery();
    
		public static IQueryable<ExamResults> GetExamResults(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults().ExamResults();
    
		public static IQueryable<PatientResults> GetPatientResults(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit().PatientResults();
    
		public static IQueryable<PatientVisit> GetPatientVisit(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientVisit();
    
		public static IQueryable<Syntoms> GetSyntoms(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientSyntoms().Syntoms();
    
		public static IQueryable<PatientSyntoms> GetPatientSyntoms(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses().PatientSyntoms();
    
		public static IQueryable<Response> GetResponse(this IQueryable<ResponseOptions> query) => query.Response();
    
		public static IQueryable<PatientResponses> GetPatientResponses(this IQueryable<ResponseOptions> query) => query.Questions().PatientResponses();
    
		public static IQueryable<Questions> GetQuestions(this IQueryable<ResponseOptions> query) => query.Questions();
    
		public static IQueryable<CarePlan> GetCarePlan(this IQueryable<ResponseOptions> query) => query.ResponseSuggestions().ResponseSuggestions_CarePlans().CarePlan();
    
		public static IQueryable<ResponseSuggestions_CarePlans> GetResponseSuggestions_CarePlans(this IQueryable<ResponseOptions> query) => query.ResponseSuggestions().ResponseSuggestions_CarePlans();
    
		public static IQueryable<ResponseSuggestions_Interviews> GetResponseSuggestions_Interviews(this IQueryable<ResponseOptions> query) => query.ResponseSuggestions().ResponseSuggestions_Interviews();
    
		public static IQueryable<ResponseSuggestions> GetResponseSuggestions(this IQueryable<ResponseOptions> query) => query.ResponseSuggestions();
    
		public static IQueryable<ResponseOptions> GetResponseOptions(this IQueryable<ResponseOptions> query) => query;

			// Child Properties
				public static IQueryable<Response> Response(this IQueryable<ResponseOptions> responseoptions) => responseoptions.SelectMany(x => x.Response);
				public static IQueryable<Response> Response(this IQueryable<ResponseOptions> responseoptions, int id) => responseoptions.Where(x => x.Id == id).SelectMany(x => x.Response);
				public static IQueryable<ResponseSuggestions> ResponseSuggestions(this IQueryable<ResponseOptions> responseoptions) => responseoptions.Select(x => x.ResponseSuggestions);
				public static IQueryable<ResponseSuggestions> ResponseSuggestions(this IQueryable<ResponseOptions> responseoptions, int id) => responseoptions.Where(x => x.Id == id).Select(x => x.ResponseSuggestions);
			//Parent Properties
				//public static IQueryable<ResponseOptions> ResponseOptions(this IQueryable<Questions> questions) => questions.SelectMany(x => x.ResponseOptions);
				public static IQueryable<Questions> Questions(this IQueryable<ResponseOptions> query) => query.Select(x => x.Questions);
	}
}
