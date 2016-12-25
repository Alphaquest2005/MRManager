﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-UnitTests.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EF.DBContexts;
using Entity.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Expressions
{
	[TestClass]
	public class  AutoExpressionTests
	{
		[TestMethod]
		public void ApplicationSettingExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ApplicationSettings.Select(ApplicationSettingExpressions.ApplicationSettingAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AddressCitiesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.AddressCities.Select(AddressCitiesExpressions.AddressCitiesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AddressCountriesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.AddressCountries.Select(AddressCountriesExpressions.AddressCountriesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AddressesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Addresses.Select(AddressesExpressions.AddressesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AddressLinesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.AddressLines.Select(AddressLinesExpressions.AddressLinesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AddressParishesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.AddressParishes.Select(AddressParishesExpressions.AddressParishesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AddressStatesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.AddressStates.Select(AddressStatesExpressions.AddressStatesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AddressTypesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.AddressTypes.Select(AddressTypesExpressions.AddressTypesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AddressZipCodesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.AddressZipCodes.Select(AddressZipCodesExpressions.AddressZipCodesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AllergiesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Allergies.Select(AllergiesExpressions.AllergiesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void AssignedDatingExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.AssignedDating.Select(AssignedDatingExpressions.AssignedDatingAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void BloodPressureExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.BloodPressure.Select(BloodPressureExpressions.BloodPressureAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void BoatInfoExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.BoatInfo.Select(BoatInfoExpressions.BoatInfoAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void CarePlanExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.CarePlan.Select(CarePlanExpressions.CarePlanAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void CarePlanDetailsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.CarePlanDetails.Select(CarePlanDetailsExpressions.CarePlanDetailsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void CarePlanDetailsSuggestedMedicationExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.CarePlanDetailsSuggestedMedication.Select(CarePlanDetailsSuggestedMedicationExpressions.CarePlanDetailsSuggestedMedicationAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void CitiesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Cities.Select(CitiesExpressions.CitiesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ComponentsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Components.Select(ComponentsExpressions.ComponentsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void CountriesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Countries.Select(CountriesExpressions.CountriesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void DefaultImagesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.DefaultImages.Select(DefaultImagesExpressions.DefaultImagesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamDetailComponentsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ExamDetailComponents.Select(ExamDetailComponentsExpressions.ExamDetailComponentsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamDetailsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ExamDetails.Select(ExamDetailsExpressions.ExamDetailsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamResultsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ExamResults.Select(ExamResultsExpressions.ExamResultsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamResults_AnioticFluidExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ExamResults_AnioticFluid.Select(ExamResults_AnioticFluidExpressions.ExamResults_AnioticFluidAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamResults_FetalDatesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ExamResults_FetalDates.Select(ExamResults_FetalDatesExpressions.ExamResults_FetalDatesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamResults_SimpleValuesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ExamResults_SimpleValues.Select(ExamResults_SimpleValuesExpressions.ExamResults_SimpleValuesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamResults_UmbilicalArteryExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ExamResults_UmbilicalArtery.Select(ExamResults_UmbilicalArteryExpressions.ExamResults_UmbilicalArteryAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Exams.Select(ExamsExpressions.ExamsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ExamTypesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ExamTypes.Select(ExamTypesExpressions.ExamTypesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ForeignAddressesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ForeignAddresses.Select(ForeignAddressesExpressions.ForeignAddressesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ForeignPhoneNumbersExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ForeignPhoneNumbers.Select(ForeignPhoneNumbersExpressions.ForeignPhoneNumbersAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void HeightExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Height.Select(HeightExpressions.HeightAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void InterviewsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Interviews.Select(InterviewsExpressions.InterviewsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void MaritalStatusExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.MaritalStatus.Select(MaritalStatusExpressions.MaritalStatusAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void MarketingMediumExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.MarketingMedium.Select(MarketingMediumExpressions.MarketingMediumAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void MediaExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Media.Select(MediaExpressions.MediaAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void MediaTypesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.MediaTypes.Select(MediaTypesExpressions.MediaTypesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void MedicalCategoryExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.MedicalCategory.Select(MedicalCategoryExpressions.MedicalCategoryAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void NonResidentCompanyInfoExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.NonResidentCompanyInfo.Select(NonResidentCompanyInfoExpressions.NonResidentCompanyInfoAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void NonResidentHotelInfoExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.NonResidentHotelInfo.Select(NonResidentHotelInfoExpressions.NonResidentHotelInfoAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void OccupationsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Occupations.Select(OccupationsExpressions.OccupationsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void OrganisationAddressExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.OrganisationAddress.Select(OrganisationAddressExpressions.OrganisationAddressAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void OrganisationPhoneNumbersExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.OrganisationPhoneNumbers.Select(OrganisationPhoneNumbersExpressions.OrganisationPhoneNumbersAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void OrganisationsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Organisations.Select(OrganisationsExpressions.OrganisationsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Organisations_CompanysExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Organisations_Companys.Select(Organisations_CompanysExpressions.Organisations_CompanysAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Organisations_HotelsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Organisations_Hotels.Select(Organisations_HotelsExpressions.Organisations_HotelsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ParishCitiesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ParishCities.Select(ParishCitiesExpressions.ParishCitiesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ParishesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Parishes.Select(ParishesExpressions.ParishesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PatientAllergiesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PatientAllergies.Select(PatientAllergiesExpressions.PatientAllergiesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PatientDoctorExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PatientDoctor.Select(PatientDoctorExpressions.PatientDoctorAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PatientReligonExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PatientReligon.Select(PatientReligonExpressions.PatientReligonAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PatientResponsesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PatientResponses.Select(PatientResponsesExpressions.PatientResponsesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PatientResultsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PatientResults.Select(PatientResultsExpressions.PatientResultsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PatientSyntomsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PatientSyntoms.Select(PatientSyntomsExpressions.PatientSyntomsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PatientVisitExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PatientVisit.Select(PatientVisitExpressions.PatientVisitAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PatientVisitVitalSignsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PatientVisitVitalSigns.Select(PatientVisitVitalSignsExpressions.PatientVisitVitalSignsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonAddressesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PersonAddresses.Select(PersonAddressesExpressions.PersonAddressesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonCountryOfResidenceExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PersonCountryOfResidence.Select(PersonCountryOfResidenceExpressions.PersonCountryOfResidenceAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonEmailAddressExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PersonEmailAddress.Select(PersonEmailAddressExpressions.PersonEmailAddressAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonJobExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PersonJob.Select(PersonJobExpressions.PersonJobAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonMaritalStatusExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PersonMaritalStatus.Select(PersonMaritalStatusExpressions.PersonMaritalStatusAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonMediaExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PersonMedia.Select(PersonMediaExpressions.PersonMediaAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonNamesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PersonNames.Select(PersonNamesExpressions.PersonNamesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonPhoneNumbersExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PersonPhoneNumbers.Select(PersonPhoneNumbersExpressions.PersonPhoneNumbersAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PersonsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Persons.Select(PersonsExpressions.PersonsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Persons_ArrivalDepartureInfoExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Persons_ArrivalDepartureInfo.Select(Persons_ArrivalDepartureInfoExpressions.Persons_ArrivalDepartureInfoAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Persons_DoctorExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Persons_Doctor.Select(Persons_DoctorExpressions.Persons_DoctorAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Persons_EmergencyContactExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Persons_EmergencyContact.Select(Persons_EmergencyContactExpressions.Persons_EmergencyContactAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Persons_NextOfKinExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Persons_NextOfKin.Select(Persons_NextOfKinExpressions.Persons_NextOfKinAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Persons_NonResidentPatientExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Persons_NonResidentPatient.Select(Persons_NonResidentPatientExpressions.Persons_NonResidentPatientAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Persons_NursesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Persons_Nurses.Select(Persons_NursesExpressions.Persons_NursesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void Persons_PatientExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Persons_Patient.Select(Persons_PatientExpressions.Persons_PatientAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PhaseExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Phase.Select(PhaseExpressions.PhaseAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PhoneTypesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PhoneTypes.Select(PhoneTypesExpressions.PhoneTypesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PrimaryPersonAddressExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PrimaryPersonAddress.Select(PrimaryPersonAddressExpressions.PrimaryPersonAddressAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PrimaryPersonPhoneNumberExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.PrimaryPersonPhoneNumber.Select(PrimaryPersonPhoneNumberExpressions.PrimaryPersonPhoneNumberAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void PulseExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Pulse.Select(PulseExpressions.PulseAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void QuestionsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Questions.Select(QuestionsExpressions.QuestionsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ReligonsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Religons.Select(ReligonsExpressions.ReligonsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void RespirationExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Respiration.Select(RespirationExpressions.RespirationAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ResponseExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Response.Select(ResponseExpressions.ResponseAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ResponseImagesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ResponseImages.Select(ResponseImagesExpressions.ResponseImagesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ResponseOptionsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ResponseOptions.Select(ResponseOptionsExpressions.ResponseOptionsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ResponseSuggestionsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ResponseSuggestions.Select(ResponseSuggestionsExpressions.ResponseSuggestionsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ResponseSuggestions_CarePlansExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ResponseSuggestions_CarePlans.Select(ResponseSuggestions_CarePlansExpressions.ResponseSuggestions_CarePlansAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ResponseSuggestions_InterviewsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ResponseSuggestions_Interviews.Select(ResponseSuggestions_InterviewsExpressions.ResponseSuggestions_InterviewsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ResultFieldNamesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ResultFieldNames.Select(ResultFieldNamesExpressions.ResultFieldNamesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void SexExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Sex.Select(SexExpressions.SexAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void StatesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.States.Select(StatesExpressions.StatesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void StudentInfoExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.StudentInfo.Select(StudentInfoExpressions.StudentInfoAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void SyntomsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Syntoms.Select(SyntomsExpressions.SyntomsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void TemperatureExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Temperature.Select(TemperatureExpressions.TemperatureAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void UltraSoundGeneralEvaluationExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.UltraSoundGeneralEvaluation.Select(UltraSoundGeneralEvaluationExpressions.UltraSoundGeneralEvaluationAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void UnitsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Units.Select(UnitsExpressions.UnitsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void UserSignInExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.UserSignIn.Select(UserSignInExpressions.UserSignInAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void VitalSignsExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.VitalSigns.Select(VitalSignsExpressions.VitalSignsAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void WeightExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.Weight.Select(WeightExpressions.WeightAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
		[TestMethod]
		public void ZipCodesExpressionGetData()
		{
			var res = MRManagerDBContext.Instance.ZipCodes.Select(ZipCodesExpressions.ZipCodesAutoViewExpression).ToList();
			if (res.Any()) Debug.Assert(true);
		}
	}
}
