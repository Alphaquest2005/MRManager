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
                    string.Join(" ",
                        x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                            .Where(
                                x2 =>
                                    x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                    x2.Questions.EntityAttributes.Attribute == nameof(IPatientDetailsInfo.Name))
                            .SelectMany(x4 => x4.Response)
                            .Select(x5 => x5.Value)),
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


            };

        public static Expression<Func<Patients, PatientInfo>> PatientInfoExpression { get; } =

            x => new PatientInfo()
            {
                Id = x.Id,
                Name =
                    string.Join(" ",
                        x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                            .Where(
                                x2 =>
                                    x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                    x2.Questions.EntityAttributes.Attribute == nameof(IPatientInfo.Name))
                            .SelectMany(x4 => x4.Response)
                            .Select(x5 => x5.Value)),
                Age =
                    DateTime.Now.Year -
                    Convert.ToDateTime(
                        x.PatientVisit.OrderByDescending(x3 => x3.Id).SelectMany(x3 => x3.PatientResponses)
                            .Where(
                                x2 =>
                                    x2.Questions.EntityAttributes.Entity == Entities.Patient &&
                                    x2.Questions.EntityAttributes.Attribute == nameof(IPatientInfo.BirthDate))
                            .SelectMany(x4 => x4.Response)
                            .Select(x5 => x5.Value)
                            .FirstOrDefault()).Year,
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
                //PatientSyntoms = x.PatientSyntoms.Select(z => new PatientSyntomInfo()
                //{
                //    Id = z.Id,
                //    SyntomName = z.Syntoms.Name,
                //    SyntomId = z.SyntomId,
                //    Syntom = z.Syntoms,
                //    Priority = z.SyntomPriority.Name,
                //    PatientVisitId = z.PatientVisitId,
                //    PriorityId = z.PriorityId,
                //    StatusId = z.StatusId,
                //    Status = z.SyntomStatus.Name,
                //    Systems = z.Syntoms.SyntomMedicalSystems.Select(s => new SyntomMedicalSystemInfo()
                //    {
                //        Id = s.Id,
                //        MedicalSystemId = s.MedicalSystemId,
                //        System = s.MedicalSystems.Name,
                //        Interviews = s.MedicalSystems.MedicalSystemInterviews.Select(i => new InterviewInfo()
                //        {
                //            Id = i.InterviewId,
                //            Interview = i.Interviews.Name,
                //            Category = i.Interviews.MedicalCategory.Name,
                //            CategoryId = i.Interviews.MedicalCategoryId,
                //            PhaseId = i.Interviews.PhaseId,
                //            Phase = i.Interviews.Phase.Name,
                //            SystemId = s.MedicalSystemId,
                //            System = s.MedicalSystems.Name,
                           
                //        } as IInterviewInfo).ToList()
                //    } as ISyntomMedicalSystemInfo).ToList(),
                //} as IPatientSyntomInfo).ToList()
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

        public static Expression<Func<SyntomMedicalSystems, SyntomMedicalSystemInfo>> SyntomMedicalSystemInfoExpression { get; } =
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

     

    }


}
