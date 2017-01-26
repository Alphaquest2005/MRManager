﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DBContext.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using EF.Entities;
using EF.Mappings;
using Microsoft.EntityFrameworkCore;

namespace EF.DBContexts
{
	public partial class MRManagerDBContext:DbContext
	{
		public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
		public DbSet<AddressCities> AddressCities { get; set; }
		public DbSet<AddressCountries> AddressCountries { get; set; }
		public DbSet<Addresses> Addresses { get; set; }
		public DbSet<AddressLines> AddressLines { get; set; }
		public DbSet<AddressParishes> AddressParishes { get; set; }
		public DbSet<AddressStates> AddressStates { get; set; }
		public DbSet<AddressTypes> AddressTypes { get; set; }
		public DbSet<AddressZipCodes> AddressZipCodes { get; set; }
		public DbSet<Allergies> Allergies { get; set; }
		public DbSet<AssignedDating> AssignedDating { get; set; }
		public DbSet<BloodPressure> BloodPressure { get; set; }
		public DbSet<BoatInfo> BoatInfo { get; set; }
		public DbSet<CarePlan> CarePlan { get; set; }
		public DbSet<CarePlanDetails> CarePlanDetails { get; set; }
		public DbSet<CarePlanDetailsSuggestedMedication> CarePlanDetailsSuggestedMedication { get; set; }
		public DbSet<Cities> Cities { get; set; }
		public DbSet<Components> Components { get; set; }
		public DbSet<Countries> Countries { get; set; }
		public DbSet<DefaultImages> DefaultImages { get; set; }
		public DbSet<EntityAttributes> EntityAttributes { get; set; }
		public DbSet<ExamDetailComponents> ExamDetailComponents { get; set; }
		public DbSet<ExamDetails> ExamDetails { get; set; }
		public DbSet<ExamResults> ExamResults { get; set; }
		public DbSet<ExamResults_AnioticFluid> ExamResults_AnioticFluid { get; set; }
		public DbSet<ExamResults_FetalDates> ExamResults_FetalDates { get; set; }
		public DbSet<ExamResults_SimpleValues> ExamResults_SimpleValues { get; set; }
		public DbSet<ExamResults_UmbilicalArtery> ExamResults_UmbilicalArtery { get; set; }
		public DbSet<Exams> Exams { get; set; }
		public DbSet<ExamTypes> ExamTypes { get; set; }
		public DbSet<ForeignAddresses> ForeignAddresses { get; set; }
		public DbSet<ForeignPhoneNumbers> ForeignPhoneNumbers { get; set; }
		public DbSet<Height> Height { get; set; }
		public DbSet<Interviews> Interviews { get; set; }
		public DbSet<MaritalStatus> MaritalStatus { get; set; }
		public DbSet<MarketingMedium> MarketingMedium { get; set; }
		public DbSet<Media> Media { get; set; }
		public DbSet<MediaTypes> MediaTypes { get; set; }
		public DbSet<MedicalCategory> MedicalCategory { get; set; }
		public DbSet<NonResidentCompanyInfo> NonResidentCompanyInfo { get; set; }
		public DbSet<NonResidentHotelInfo> NonResidentHotelInfo { get; set; }
		public DbSet<Occupations> Occupations { get; set; }
		public DbSet<OrganisationAddress> OrganisationAddress { get; set; }
		public DbSet<OrganisationPhoneNumbers> OrganisationPhoneNumbers { get; set; }
		public DbSet<Organisations> Organisations { get; set; }
		public DbSet<Organisations_Companys> Organisations_Companys { get; set; }
		public DbSet<Organisations_Hotels> Organisations_Hotels { get; set; }
		public DbSet<ParishCities> ParishCities { get; set; }
		public DbSet<Parishes> Parishes { get; set; }
		public DbSet<PatientAllergies> PatientAllergies { get; set; }
		public DbSet<PatientDoctor> PatientDoctor { get; set; }
		public DbSet<PatientReligon> PatientReligon { get; set; }
		public DbSet<PatientResponses> PatientResponses { get; set; }
		public DbSet<PatientResults> PatientResults { get; set; }
		public DbSet<Patients> Patients { get; set; }
		public DbSet<PatientSyntoms> PatientSyntoms { get; set; }
		public DbSet<PatientVisit> PatientVisit { get; set; }
		public DbSet<PatientVisitVitalSigns> PatientVisitVitalSigns { get; set; }
		public DbSet<PersonAddresses> PersonAddresses { get; set; }
		public DbSet<PersonCountryOfResidence> PersonCountryOfResidence { get; set; }
		public DbSet<PersonEmailAddress> PersonEmailAddress { get; set; }
		public DbSet<PersonJob> PersonJob { get; set; }
		public DbSet<PersonMaritalStatus> PersonMaritalStatus { get; set; }
		public DbSet<PersonMedia> PersonMedia { get; set; }
		public DbSet<PersonNames> PersonNames { get; set; }
		public DbSet<PersonPhoneNumbers> PersonPhoneNumbers { get; set; }
		public DbSet<Persons> Persons { get; set; }
		public DbSet<Persons_ArrivalDepartureInfo> Persons_ArrivalDepartureInfo { get; set; }
		public DbSet<Persons_Doctor> Persons_Doctor { get; set; }
		public DbSet<Persons_EmergencyContact> Persons_EmergencyContact { get; set; }
		public DbSet<Persons_NextOfKin> Persons_NextOfKin { get; set; }
		public DbSet<Persons_NonResidentPatient> Persons_NonResidentPatient { get; set; }
		public DbSet<Persons_Nurses> Persons_Nurses { get; set; }
		public DbSet<Persons_Patient> Persons_Patient { get; set; }
		public DbSet<Phase> Phase { get; set; }
		public DbSet<PhoneTypes> PhoneTypes { get; set; }
		public DbSet<PrimaryPersonAddress> PrimaryPersonAddress { get; set; }
		public DbSet<PrimaryPersonPhoneNumber> PrimaryPersonPhoneNumber { get; set; }
		public DbSet<Pulse> Pulse { get; set; }
		public DbSet<Questions> Questions { get; set; }
		public DbSet<Religons> Religons { get; set; }
		public DbSet<Respiration> Respiration { get; set; }
		public DbSet<Response> Response { get; set; }
		public DbSet<ResponseImages> ResponseImages { get; set; }
		public DbSet<ResponseOptions> ResponseOptions { get; set; }
		public DbSet<ResponseSuggestions> ResponseSuggestions { get; set; }
		public DbSet<ResponseSuggestions_CarePlans> ResponseSuggestions_CarePlans { get; set; }
		public DbSet<ResponseSuggestions_Interviews> ResponseSuggestions_Interviews { get; set; }
		public DbSet<ResultFieldNames> ResultFieldNames { get; set; }
		public DbSet<Sex> Sex { get; set; }
		public DbSet<States> States { get; set; }
		public DbSet<StudentInfo> StudentInfo { get; set; }
		public DbSet<Syntoms> Syntoms { get; set; }
		public DbSet<Temperature> Temperature { get; set; }
		public DbSet<UltraSoundGeneralEvaluation> UltraSoundGeneralEvaluation { get; set; }
		public DbSet<Units> Units { get; set; }
		public DbSet<UserSignIn> UserSignIn { get; set; }
		public DbSet<VitalSigns> VitalSigns { get; set; }
		public DbSet<Weight> Weight { get; set; }
		public DbSet<ZipCodes> ZipCodes { get; set; }
	
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(DBContext.Properties.Settings.Default.DbConnectionString);
		}
	
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
				ApplicationSettingMap.Map(modelBuilder.Entity<ApplicationSetting>());
				AddressCitiesMap.Map(modelBuilder.Entity<AddressCities>());
				AddressCountriesMap.Map(modelBuilder.Entity<AddressCountries>());
				AddressesMap.Map(modelBuilder.Entity<Addresses>());
				AddressLinesMap.Map(modelBuilder.Entity<AddressLines>());
				AddressParishesMap.Map(modelBuilder.Entity<AddressParishes>());
				AddressStatesMap.Map(modelBuilder.Entity<AddressStates>());
				AddressTypesMap.Map(modelBuilder.Entity<AddressTypes>());
				AddressZipCodesMap.Map(modelBuilder.Entity<AddressZipCodes>());
				AllergiesMap.Map(modelBuilder.Entity<Allergies>());
				AssignedDatingMap.Map(modelBuilder.Entity<AssignedDating>());
				BloodPressureMap.Map(modelBuilder.Entity<BloodPressure>());
				BoatInfoMap.Map(modelBuilder.Entity<BoatInfo>());
				CarePlanMap.Map(modelBuilder.Entity<CarePlan>());
				CarePlanDetailsMap.Map(modelBuilder.Entity<CarePlanDetails>());
				CarePlanDetailsSuggestedMedicationMap.Map(modelBuilder.Entity<CarePlanDetailsSuggestedMedication>());
				CitiesMap.Map(modelBuilder.Entity<Cities>());
				ComponentsMap.Map(modelBuilder.Entity<Components>());
				CountriesMap.Map(modelBuilder.Entity<Countries>());
				DefaultImagesMap.Map(modelBuilder.Entity<DefaultImages>());
				EntityAttributesMap.Map(modelBuilder.Entity<EntityAttributes>());
				ExamDetailComponentsMap.Map(modelBuilder.Entity<ExamDetailComponents>());
				ExamDetailsMap.Map(modelBuilder.Entity<ExamDetails>());
				ExamResultsMap.Map(modelBuilder.Entity<ExamResults>());
				ExamResults_AnioticFluidMap.Map(modelBuilder.Entity<ExamResults_AnioticFluid>());
				ExamResults_FetalDatesMap.Map(modelBuilder.Entity<ExamResults_FetalDates>());
				ExamResults_SimpleValuesMap.Map(modelBuilder.Entity<ExamResults_SimpleValues>());
				ExamResults_UmbilicalArteryMap.Map(modelBuilder.Entity<ExamResults_UmbilicalArtery>());
				ExamsMap.Map(modelBuilder.Entity<Exams>());
				ExamTypesMap.Map(modelBuilder.Entity<ExamTypes>());
				ForeignAddressesMap.Map(modelBuilder.Entity<ForeignAddresses>());
				ForeignPhoneNumbersMap.Map(modelBuilder.Entity<ForeignPhoneNumbers>());
				HeightMap.Map(modelBuilder.Entity<Height>());
				InterviewsMap.Map(modelBuilder.Entity<Interviews>());
				MaritalStatusMap.Map(modelBuilder.Entity<MaritalStatus>());
				MarketingMediumMap.Map(modelBuilder.Entity<MarketingMedium>());
				MediaMap.Map(modelBuilder.Entity<Media>());
				MediaTypesMap.Map(modelBuilder.Entity<MediaTypes>());
				MedicalCategoryMap.Map(modelBuilder.Entity<MedicalCategory>());
				NonResidentCompanyInfoMap.Map(modelBuilder.Entity<NonResidentCompanyInfo>());
				NonResidentHotelInfoMap.Map(modelBuilder.Entity<NonResidentHotelInfo>());
				OccupationsMap.Map(modelBuilder.Entity<Occupations>());
				OrganisationAddressMap.Map(modelBuilder.Entity<OrganisationAddress>());
				OrganisationPhoneNumbersMap.Map(modelBuilder.Entity<OrganisationPhoneNumbers>());
				OrganisationsMap.Map(modelBuilder.Entity<Organisations>());
				Organisations_CompanysMap.Map(modelBuilder.Entity<Organisations_Companys>());
				Organisations_HotelsMap.Map(modelBuilder.Entity<Organisations_Hotels>());
				ParishCitiesMap.Map(modelBuilder.Entity<ParishCities>());
				ParishesMap.Map(modelBuilder.Entity<Parishes>());
				PatientAllergiesMap.Map(modelBuilder.Entity<PatientAllergies>());
				PatientDoctorMap.Map(modelBuilder.Entity<PatientDoctor>());
				PatientReligonMap.Map(modelBuilder.Entity<PatientReligon>());
				PatientResponsesMap.Map(modelBuilder.Entity<PatientResponses>());
				PatientResultsMap.Map(modelBuilder.Entity<PatientResults>());
				PatientsMap.Map(modelBuilder.Entity<Patients>());
				PatientSyntomsMap.Map(modelBuilder.Entity<PatientSyntoms>());
				PatientVisitMap.Map(modelBuilder.Entity<PatientVisit>());
				PatientVisitVitalSignsMap.Map(modelBuilder.Entity<PatientVisitVitalSigns>());
				PersonAddressesMap.Map(modelBuilder.Entity<PersonAddresses>());
				PersonCountryOfResidenceMap.Map(modelBuilder.Entity<PersonCountryOfResidence>());
				PersonEmailAddressMap.Map(modelBuilder.Entity<PersonEmailAddress>());
				PersonJobMap.Map(modelBuilder.Entity<PersonJob>());
				PersonMaritalStatusMap.Map(modelBuilder.Entity<PersonMaritalStatus>());
				PersonMediaMap.Map(modelBuilder.Entity<PersonMedia>());
				PersonNamesMap.Map(modelBuilder.Entity<PersonNames>());
				PersonPhoneNumbersMap.Map(modelBuilder.Entity<PersonPhoneNumbers>());
				PersonsMap.Map(modelBuilder.Entity<Persons>());
				Persons_ArrivalDepartureInfoMap.Map(modelBuilder.Entity<Persons_ArrivalDepartureInfo>());
				Persons_DoctorMap.Map(modelBuilder.Entity<Persons_Doctor>());
				Persons_EmergencyContactMap.Map(modelBuilder.Entity<Persons_EmergencyContact>());
				Persons_NextOfKinMap.Map(modelBuilder.Entity<Persons_NextOfKin>());
				Persons_NonResidentPatientMap.Map(modelBuilder.Entity<Persons_NonResidentPatient>());
				Persons_NursesMap.Map(modelBuilder.Entity<Persons_Nurses>());
				Persons_PatientMap.Map(modelBuilder.Entity<Persons_Patient>());
				PhaseMap.Map(modelBuilder.Entity<Phase>());
				PhoneTypesMap.Map(modelBuilder.Entity<PhoneTypes>());
				PrimaryPersonAddressMap.Map(modelBuilder.Entity<PrimaryPersonAddress>());
				PrimaryPersonPhoneNumberMap.Map(modelBuilder.Entity<PrimaryPersonPhoneNumber>());
				PulseMap.Map(modelBuilder.Entity<Pulse>());
				QuestionsMap.Map(modelBuilder.Entity<Questions>());
				ReligonsMap.Map(modelBuilder.Entity<Religons>());
				RespirationMap.Map(modelBuilder.Entity<Respiration>());
				ResponseMap.Map(modelBuilder.Entity<Response>());
				ResponseImagesMap.Map(modelBuilder.Entity<ResponseImages>());
				ResponseOptionsMap.Map(modelBuilder.Entity<ResponseOptions>());
				ResponseSuggestionsMap.Map(modelBuilder.Entity<ResponseSuggestions>());
				ResponseSuggestions_CarePlansMap.Map(modelBuilder.Entity<ResponseSuggestions_CarePlans>());
				ResponseSuggestions_InterviewsMap.Map(modelBuilder.Entity<ResponseSuggestions_Interviews>());
				ResultFieldNamesMap.Map(modelBuilder.Entity<ResultFieldNames>());
				SexMap.Map(modelBuilder.Entity<Sex>());
				StatesMap.Map(modelBuilder.Entity<States>());
				StudentInfoMap.Map(modelBuilder.Entity<StudentInfo>());
				SyntomsMap.Map(modelBuilder.Entity<Syntoms>());
				TemperatureMap.Map(modelBuilder.Entity<Temperature>());
				UltraSoundGeneralEvaluationMap.Map(modelBuilder.Entity<UltraSoundGeneralEvaluation>());
				UnitsMap.Map(modelBuilder.Entity<Units>());
				UserSignInMap.Map(modelBuilder.Entity<UserSignIn>());
				VitalSignsMap.Map(modelBuilder.Entity<VitalSigns>());
				WeightMap.Map(modelBuilder.Entity<Weight>());
				ZipCodesMap.Map(modelBuilder.Entity<ZipCodes>());
			}
	}
}
