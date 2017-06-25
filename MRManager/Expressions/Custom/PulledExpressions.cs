﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Expressions.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.DataEntites;
using EF.Entities;
using Interfaces;
using MoreLinq;

namespace Entity.Expressions
{
    public static class Entities
    {
        public const string Patient = "Patient";
    }



    public static partial class PulledExpressions
    {

        public static Expression<Func<Patients, PatientDetailsInfo>> PatientDetailsInfoExpression { get; } =

            x => new PatientDetailsInfo()
            {
                Id = x.Id,
                IdNumber =
                    x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                        .Where(
                            x2 =>
                                x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.IdNumber))
                        .SelectMany(x4 => x4.Response)
                        .Select(x5 => x5.Value)
                        .FirstOrDefault(),
                Name =

                    x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                        .Where(
                            x2 =>
                                x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.Name))
                        .SelectMany(x4 => x4.Response)
                        .Select(x5 => x5.Value).FirstOrDefault(),
                Age =
                    DateTime.Now.Year -
                    Convert.ToDateTime(
                        x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                            .Where(
                                x2 =>
                                    x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                    x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.BirthDate))
                            .SelectMany(x4 => x4.Response)
                            .Select(x5 => x5.Value)
                            .FirstOrDefault()).Year,
                BirthDate =
                    Convert.ToDateTime(
                        x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                            .Where(
                                x2 =>
                                    x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                    x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.BirthDate))
                            .SelectMany(x4 => x4.Response)
                            .Select(x5 => x5.Value)
                            .FirstOrDefault()),
                PhoneNumber = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.PhoneNumber))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

                Address = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.Address))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

                EmailAddress = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.EmailAddress))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

                Allergies = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.Allergies))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

                Job = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.Job))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

                Religion = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.Religion))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),
                BirthCountry = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.BirthCountry))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),
                CountryOfResidence = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.CountryOfResidence))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

                MaritalStatus = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.MaritalStatus))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

                Sex = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.Sex))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

            };

        public static Expression<Func<Patients, PatientPhoneNumbersInfo>> PatientPhoneNumbersInfoExpression { get; } =
            x => new PatientPhoneNumbersInfo()
            {
                Id = x.Id,
                PhoneNumbers = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == "Contact" &&
                            x2.Questions.EntityAttributes.Attribute == "PhoneNumber")
                    .SelectMany(x4 => x4.Response).Where(x6 => x6.ResponseOptions.QuestionResponseTypes.Name == "Text")
                    .Select(x5 => new PersonPhoneNumberInfo()
                    {
                        Id = x5.Id,
                        PersonId = x.Id,
                        PhoneNumber = x5.Value,
                        PhoneType = x5.ResponseOptions.Description,
                    } as IPersonPhoneNumberInfo).ToList(),
            };


        //public static Expression<Func<IList<IResponseOptions>, List<IPhoneTypes>>> PhoneTypesExpression { get; } =
        //    x => x.Where(x1 => x1.Description == "PhoneType")
        //        .Select(x2 => new PhoneTypes(){
        //            Id = x2.Id,
        //            Name = x2.Description} as IPhoneTypes).ToList();



        public static Expression<Func<Patients, PatientNextOfKinsInfo>> PatientNextOfKinInfoExpression { get; } =
            x => new PatientNextOfKinsInfo()
            {
                Id = x.Id,
                NextOfKins = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == "Contact" &&
                            x2.Questions.EntityAttributes.Attribute == "NextOfKin")
                    .GroupBy(x6 => x6.QuestionId)
                    .Select(x5 => new NextOfKinInfo()
                    {
                        Id = x5.Key,
                        PatientId = x.Id,
                        Relationship =
                            x5.SelectMany(x7 => x7.Response)
                                .Where(x6 => x6.ResponseOptions.Description == "Relationship")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                        Name = x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Name")
                            .Select(x6 => x6.Value).FirstOrDefault(),
                        Address =
                            x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Address")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                        PhoneNumber =
                            x5.SelectMany(x7 => x7.Response)
                                .Where(x6 => x6.ResponseOptions.Description == "PhoneNumber")
                                .Select(x6 => x6.Value).FirstOrDefault(),

                    } as INextOfKinInfo).ToList(),
            };


        public static Expression<Func<Patients, NonResidentInfo>> PatientNonResidentInfoExpression { get; } =
            // x => new NonResidentInfo() {Id = x.Id};
            x =>
                x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == "NonResident")
                    .GroupBy(x6 => x6.Questions.EntityAttributes.Entity)
                    .Select(x5 => new NonResidentInfo()
                    {
                        Id = x.Id,
                        Type = x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Type")
                            .Select(x6 => x6.Value).FirstOrDefault(),
                        BoatName =
                            x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Boat Name")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                        Marina =
                            x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Marina")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                        School =
                            x5.SelectMany(x7 => x7.Response)
                                .Where(x6 => x6.ResponseOptions.Description == "School Name")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                        DepartureDate =
                            Convert.ToDateTime(
                                x5.SelectMany(x7 => x7.Response)
                                    .Where(x6 => x6.ResponseOptions.Description == "Departure Date")

                                    .Select(x6 => x6.Value).FirstOrDefault()),
                        ArrivalDate =
                            Convert.ToDateTime(
                                x5.SelectMany(x7 => x7.Response)
                                    .Where(x6 => x6.ResponseOptions.Description == "Arrival Date")
                                    .Select(x6 => x6.Value).FirstOrDefault()),
                    }).FirstOrDefault();


        public static Expression<Func<Patients, PatientAddressesInfo>> PatientPersonAddressInfoExpression { get; } =

            x => new PatientAddressesInfo()
            {
                Id = x.Id,
                
                Addresses = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == "Contact" &&
                            x2.Questions.EntityAttributes.Attribute == "Address")
                    .GroupBy(x6 => x6.QuestionId)
                    .Select(x5 => new PersonAddressInfo()
                    {
                        Id = x5.Key,
                        PersonId = x.Id,
                        AddressType =
                            x5.SelectMany(x7 => x7.Response)
                                .Where(x6 => x6.ResponseOptions.Description == "Address Type")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                        Parish =
                            x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Parish")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                        Address =
                            x5.SelectMany(x7 => x7.Response)
                                .Where(x6 => x6.ResponseOptions.Description == "Address")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                        City = x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "City")
                            .Select(x6 => x6.Value).FirstOrDefault(),
                        State = x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "State")
                            .Select(x6 => x6.Value).FirstOrDefault(),
                        Country =
                            x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Country")
                                .Select(x6 => x6.Value).FirstOrDefault(),

                    } as IPersonAddressInfo).ToList()
            };



        public static Expression<Func<Patients, PatientInfo>> PatientInfoExpression { get; } =

            x => new PatientInfo()
            {
                Id = x.Id,
                Name =

                    x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                        .Where(
                            x2 =>
                                x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                x2.Questions.EntityAttributes.Attribute == nameof(IPatientInfo.Name))
                        .SelectMany(x4 => x4.Response)
                        .Select(x5 => x5.Value).FirstOrDefault(),

                BirthDate =
                    Convert.ToDateTime(
                        x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                            .Where(
                                x2 =>
                                    x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                    x2.Questions.EntityAttributes.Attribute == nameof(IPatientInfo.BirthDate))
                            .SelectMany(x4 => x4.Response)
                            .Select(x5 => x5.Value)
                            .FirstOrDefault()),
                Address = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.Address))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),

                PhoneNumber = x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                            x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.PhoneNumber))
                    .SelectMany(x4 => x4.Response)
                    .Select(x5 => x5.Value)
                    .FirstOrDefault(),
            };

        public static Expression<Func<PatientVisit, PatientVisitInfo>> PatientVistInfoExpression { get; } =
            x => new PatientVisitInfo()
            {
                Id = x.Id,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                DateOfVisit = x.DateOfVisit,
                Purpose = x.VisitType.Name,
                VisitTypeId = x.VisitTypeId,
                AttendingDoctor = string.Join(" ", x.Persons_Doctor.Persons.PersonNames.Select(z => z.PersonName)),

            };

        public static Expression<Func<PatientSyntoms, PatientSyntomInfo>> PatientSyntomInfoExpression { get; } =
            z => new PatientSyntomInfo()
            {
                Id = z.Id,
                SyntomName = z.Syntoms.Name,
                SyntomId = z.SyntomId,
                Syntom = z.Syntoms,
                Priority = z.SyntomPriority.Name,
                PatientVisitId = z.PatientVisitId,
                PriorityId = z.PriorityId,
                StatusId = z.StatusId,
                Status = z.SyntomStatus.Name,
            };

        public static Expression<Func<SyntomMedicalSystems, SyntomMedicalSystemInfo>> SyntomMedicalSystemInfoExpression
        { get; } =
            z => new SyntomMedicalSystemInfo()
            {
                Id = z.Id,
                SyntomId = z.SyntomId,
                SyntomName = z.Syntoms.Name,
                MedicalSystemId = z.MedicalSystemId,
                System = z.MedicalSystems.Name,
                Interviews = z.MedicalSystems.MedicalSystemInterviews.Select(i => new InterviewInfo()
                {
                    Id = i.InterviewId,
                    Interview = i.Interviews.Name,
                    Category = i.Interviews.MedicalCategory.Name,
                    CategoryId = i.Interviews.MedicalCategoryId,
                    PhaseId = i.Interviews.PhaseId,
                    Phase = i.Interviews.Phase.Name,
                    SystemId = z.MedicalSystemId,
                    System = z.MedicalSystems.Name,

                } as IInterviewInfo).ToList()
            };

        public static Expression<Func<Patients, PatientVitalsInfo>> PatientVitalsInfoExpression { get; } =

          x =>   x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                    .Where(
                        x2 =>
                            x2.Questions.EntityAttributes.Entity == "Vitals")
                    .GroupBy(x6 => x6.Questions.EntityAttributes.Entity)
                    .Select(x5 => new PatientVitalsInfo()
                    {
                        Id = x.Id,
                        Temperature = Convert.ToInt32(x5.SelectMany(x7 => x7.Response)
                                .Where(x6 => x6.ResponseOptions.Description == "Temperature")
                                .Select(x6 => x6.Value).FirstOrDefault()),
                        BloodPressure = x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "BloodPressure")
                            .Select(x6 => x6.Value).FirstOrDefault(),
                        Pulse =
                            Convert.ToInt32(x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Pulse")
                                .Select(x6 => x6.Value).FirstOrDefault()),
                        SaO2 =
                            x5.SelectMany(x7 => x7.Response)
                                .Where(x6 => x6.ResponseOptions.Description == "SaO2")
                                .Select(x6 => x6.Value).FirstOrDefault(),
                    }).FirstOrDefault();
        //x => x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
        //            .Where(
        //                x2 =>
        //                    x2.Questions.EntityAttributes.Entity == "Patient" &&
        //                    x2.Questions.EntityAttributes.Attribute == "Vitals")
        //            .GroupBy(x6 => x6.QuestionId)
        //            .Select(x5 => new PatientVitalsInfo()
        //            {
        //                Id = x5.Key,
        //                Temperature = 
        //                    Convert.ToInt32(x5.SelectMany(x7 => x7.Response)
        //                        .Where(x6 => x6.ResponseOptions.Description == "Temperature")
        //                        .Select(x6 => x6.Value).FirstOrDefault()),
        //                BloodPressure = x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "BloodPressure")
        //                    .Select(x6 => x6.Value).FirstOrDefault(),
        //                Pulse =
        //                    Convert.ToInt32(x5.SelectMany(x7 => x7.Response).Where(x6 => x6.ResponseOptions.Description == "Pulse")
        //                        .Select(x6 => x6.Value).FirstOrDefault()),
        //                SaO2 =
        //                    x5.SelectMany(x7 => x7.Response)
        //                        .Where(x6 => x6.ResponseOptions.Description == "SaO2")
        //                        .Select(x6 => x6.Value).FirstOrDefault(),

        //            }).LastOrDefault()?? new PatientVitalsInfo() {Id = x.Id};


    }


}
