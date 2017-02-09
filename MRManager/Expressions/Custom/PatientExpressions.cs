﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Expressions.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.DataEntites;
using MoreLinq;
using EF.Entities;
using Interfaces;

namespace Entity.Expressions
{
    public static partial class PatientExpressions
    {
        //TODO: Double check all data present
        public static Expression<Func<UserSignIn, SignInInfo>> SignInInfoExpression { get; } =

            x => new SignInInfo()
            {
                Id = x.Id,
                Medias = x.Persons.PersonMedia.Select(x2 => x2.Media).Select(x3 => x3.Value).FirstOrDefault(),
                Usersignin = x.Username,
                Password = x.Password,
            };

        public static Expression<Func<Persons_Patient, PatientDetailsInfo>> PatientDetailsInfoExpression { get; } =

            x => new PatientDetailsInfo()
            {
                Id = x.Id,
                Name = string.Join(" ", x.Persons.PersonNames.Select(z => z.PersonName)),
                CountryOfResidence = x.PersonCountryOfResidence.Select(x1 => x1.Countries.Name).FirstOrDefault(),
                Age = DateTime.Now.Year - x.DateOfBirth.Year,
                EmailAddress = x.Persons.PersonEmailAddress.FirstOrDefault().Email,
                PhoneNumber = x.Persons.PersonPhoneNumbers.FirstOrDefault().PhoneNumber,
                Sex = x.Sex.Name,
                Address =
                    string.Join(", ",
                        x.Persons.PersonAddresses.SelectMany(z => z.PrimaryPersonAddress)
                            .SelectMany(u => u.PersonAddresses.Addresses.AddressLines)
                            .Select(h => h.Name)),
                Allergies = string.Join(", ", x.PatientAllergies.Select(d => d.Allergies.Name)),
                BirthCountry = x.Countries.Name,
                MaritalStatus = x.PersonMaritalStatus.Select(z => z.MaritalStatus.Name).LastOrDefault(),
                Addresses = x.Persons.PersonAddresses.Select(z => new PersonAddressInfo()
                {
                    Addresstype = z.AddressTypes.Name,
                    City = z.Addresses.AddressCities.Cities.Name,
                    Country = z.Addresses.AddressCountries.Countries.Name,
                    Parish = z.Addresses.AddressParishes.Parishes.Name,
                    State = z.Addresses.AddressStates.States.Name

                } as IPersonAddressInfo).ToList(),
                PhoneNumbers = x.Persons.PersonPhoneNumbers.Select(z => new PhoneNumbersInfo()
                {
                    PersonId = x.Id,
                    PhoneNumber = z.PhoneNumber,
                    Type = z.PhoneTypes.Name
                } as IPersonPhoneNumberInfo).ToList(),
                NextOfKins = x.Persons_NextOfKin.Select(z => new NextOfKinInfo()
                {
                    Id = z.Id,
                    Name = string.Join(" ", z.Persons.PersonNames.Select(q => q.PersonName)),

                    Email = z.Persons.PersonEmailAddress.FirstOrDefault().Email,
                    PhoneNumber = z.Persons.PersonPhoneNumbers.FirstOrDefault().PhoneNumber,

                    Address =
                        string.Join(", ",
                            z.Persons.PersonAddresses.SelectMany(s => s.PrimaryPersonAddress)
                                .SelectMany(u => u.PersonAddresses.Addresses.AddressLines)
                                .Select(h => h.Name)),

                    Addresses = z.Persons.PersonAddresses.Select(q => new PersonAddressInfo()
                    {
                        Addresstype = q.AddressTypes.Name,
                        City = q.Addresses.AddressCities.Cities.Name,
                        Country = q.Addresses.AddressCountries.Countries.Name,
                        Parish = q.Addresses.AddressParishes.Parishes.Name,
                        State = q.Addresses.AddressStates.States.Name

                    } as IPersonAddressInfo).ToList(),
                    PhoneNumbers = z.Persons.PersonPhoneNumbers.Select(q => new PhoneNumbersInfo()
                    {
                        PersonId = q.Id,
                        PhoneNumber = q.PhoneNumber,
                        Type = q.PhoneTypes.Name
                    } as IPersonPhoneNumberInfo).ToList(),
                } as INextOfKinInfo).ToList(),
                NonResident = new NonResidentInfo()
                {
                    Id = x.Id,
                    BoatName = x.Persons_NonResidentPatient.BoatInfo.BoatName,
                    ArrivalDate = x.Persons_NonResidentPatient.Persons_ArrivalDepartureInfo.ArrivalDate,
                    DepartureDate = x.Persons_NonResidentPatient.Persons_ArrivalDepartureInfo.DepartureDate,
                    HotelName =
                        x.Persons_NonResidentPatient.NonResidentHotelInfo.Organisations_Hotels.Organisations.Name,
                    MarinaList = x.Persons_NonResidentPatient.BoatInfo.MarinaList,
                    School = x.Persons_NonResidentPatient.StudentInfo.School,
                    Addresses = x.Persons.PersonAddresses.Select(z => new ForeignAddressInfo()
                    {
                        Addresstype = z.AddressTypes.Name,
                        Addresslines = string.Join(", ", z.Addresses.AddressLines),
                        City = z.Addresses.AddressCities.Cities.Name,
                        Country = z.Addresses.AddressCountries.Countries.Name,
                        Parish = z.Addresses.AddressParishes.Parishes.Name,
                        State = z.Addresses.AddressStates.States.Name

                    } as IForeignAddressInfo).ToList(),
                    PhoneNumbers = x.Persons.PersonPhoneNumbers.Select(z => new PhoneNumbersInfo()
                    {
                        PersonId = x.Id,
                        PhoneNumber = z.PhoneNumber,
                        Type = z.PhoneTypes.Name
                    } as IPersonPhoneNumberInfo).ToList(),
                } as INonResidentInfo
            };

        public static Expression<Func<Persons_Patient, NextOfKinInfo>> NextOfKinInfoExpression { get; } =

            x => new NextOfKinInfo()
            {
                Id = x.Id,
                Name = string.Join(" ", x.Persons.PersonNames.Select(z => z.PersonName)),

                Email = x.Persons.PersonEmailAddress.FirstOrDefault().Email,
                PhoneNumber = x.Persons.PersonPhoneNumbers.FirstOrDefault().PhoneNumber,

                Address =
                    string.Join(", ",
                        x.Persons.PersonAddresses.Select(z => z.Addresses.AddressLines)
                            .Select(q => q.Select(r => r.Name))),

                Addresses = x.Persons.PersonAddresses.Select(z => new PersonAddressInfo()
                {
                    Addresstype = z.AddressTypes.Name,
                    City = z.Addresses.AddressCities.Cities.Name,
                    Country = z.Addresses.AddressCountries.Countries.Name,
                    Parish = z.Addresses.AddressParishes.Parishes.Name,
                    State = z.Addresses.AddressStates.States.Name

                } as IPersonAddressInfo).ToList(),
                PhoneNumbers = x.Persons.PersonPhoneNumbers.Select(z => new PhoneNumbersInfo()
                {
                    PersonId = x.Id,
                    PhoneNumber = z.PhoneNumber,
                    Type = z.PhoneTypes.Name
                } as IPersonPhoneNumberInfo).ToList(),
            };


        public static Expression<Func<Persons_Patient, PatientInfo>> PatientInfoExpression { get; } =

            x => new PatientInfo()
            {
                Id = x.Id,
                Age = DateTime.Now.Year - x.DateOfBirth.Year,
                Email = x.Persons.PersonEmailAddress.FirstOrDefault().Email,
                PhoneNumber = x.Persons.PersonPhoneNumbers.FirstOrDefault().PhoneNumber,
                Sex = x.Sex.Name,
                Address =
                    string.Join(", ",
                        x.Persons.PersonAddresses.SelectMany(z => z.PrimaryPersonAddress)
                            .SelectMany(u => u.PersonAddresses.Addresses.AddressLines)
                            .Select(h => h.Name)),
                BirthCountry = x.Countries.Name,
                Name = string.Join(" ", x.Persons.PersonNames.Select(z => z.PersonName))
            };

        public static Expression<Func<Persons_Patient, NonResidentInfo>> NonResidentInfoExpression { get; } =

            x => new NonResidentInfo()
            {
                Id = x.Id,
                BoatName = x.Persons_NonResidentPatient.BoatInfo.BoatName,
                ArrivalDate = x.Persons_NonResidentPatient.Persons_ArrivalDepartureInfo.ArrivalDate,
                DepartureDate = x.Persons_NonResidentPatient.Persons_ArrivalDepartureInfo.DepartureDate,
                HotelName = x.Persons_NonResidentPatient.NonResidentHotelInfo.Organisations_Hotels.Organisations.Name,
                MarinaList = x.Persons_NonResidentPatient.BoatInfo.MarinaList,
                School = x.Persons_NonResidentPatient.StudentInfo.School,
                Addresses = x.Persons.PersonAddresses.Select(z => new ForeignAddressInfo()
                {
                    Addresstype = z.AddressTypes.Name,
                    Addresslines = string.Join(", ", z.Addresses.AddressLines.Select(s => s.Name)),
                    City = z.Addresses.AddressCities.Cities.Name,
                    Country = z.Addresses.AddressCountries.Countries.Name,
                    Parish = z.Addresses.AddressParishes.Parishes.Name,
                    State = z.Addresses.AddressStates.States.Name

                } as IForeignAddressInfo).ToList(),
                PhoneNumbers = x.Persons.PersonPhoneNumbers.Select(z => new PhoneNumbersInfo()
                {
                    PersonId = x.Id,
                    PhoneNumber = z.PhoneNumber,
                    Type = z.PhoneTypes.Name
                } as IPersonPhoneNumberInfo).ToList(),
            };

        public static Expression<Func<Persons_Patient, List<PhoneNumbersInfo>>> PatientPhoneNumbersInfoExpression { get;
        } =

            x => x.Persons.PersonPhoneNumbers.Select(z => new PhoneNumbersInfo()
            {
                PersonId = x.Id,
                PhoneNumber = z.PhoneNumber,
                Type = z.PhoneTypes.Name
            }).ToList();

        public static Expression<Func<Interviews, InterviewInfo>> InterviewInfoExpression { get; } =
            x => new InterviewInfo()
            {
                Id = x.Id,
                Interview = x.Name,
                Category = x.MedicalCategory.Name,
                Phase = x.Phase.Name
            };

        public static Expression<Func<PatientResponses, PatientResponseInfo>> PatientResponseInfoExpression { get; } =
            x => new PatientResponseInfo()
            {
                Id = x.Id,
                Category = x.Questions.Interviews.MedicalCategory.Name,
                Question = x.Questions.Description,
                Interview = x.Questions.Interviews.Name,
                PatientSyntomId = x.PatientSyntomId,
                InterviewId = x.Questions.InterviewId,
                PatientVisitId = x.PatientVisitId,
                QuestionId = x.QuestionId,
                PatientId = x.PatientVisit.PatientId,
                ResponseImages = x.ResponseImages.Select(z => new ResponseImage()
                {
                    MediaId = z.MediaId,
                    PatientResponseId = z.PatientResponseId,
                    Media = z.Media.Value
                } as IResponseImage).ToList(),
                ResponseOptions = x.Questions.ResponseOptions.OrderBy(z => z.ResponseNumber).Select(z => new ResponseOptionInfo()
                {
                    Id = z.Id,
                    Description = z.Description,
                    QuestionId = z.QuestionId,
                    ResponseId =
                        (int?)
                            (z.Response.Any()
                                ? z.Response.Where(x1 => x1.PatientResponseId == x.Id).Select(x2 => x2.Id).First()
                                : 0),
                    Value =
                            z.Response.Any()
                            ? z.Response.Where(x1 => x1.PatientResponseId == x.Id).Select(x2 => x2.Value).First()
                            : null, //
                    Type = z.Type,
                    PatientResponseId = x.Id,
                    ResponseNumber = z.ResponseNumber

                } as IResponseOptionInfo).ToList(),


            };
        //ToDo: just a short cut to avoid implementing entity repostiory
        public static Expression<Func<Response, ResponseInfo>> ResponseInfoExpression { get; } =
            x => new ResponseInfo()
            {
                Id = x.Id,
                PatientResponseId = x.PatientResponseId,
                ResponseOptionId = x.ResponseOptionId,
                Value = x.Value
            };


       public static Expression<Func<Questions, QuestionInfo>> QuestionInfoInfoExpression { get; } =
            x => new QuestionInfo()
            {
                Id = x.Id,
                InterviewId = x.InterviewId,
                Description = x.Description,    
                EntityAttributeId = x.EntityAttributeId,
                Interview = x.Interviews.Name,
                Phase = x.Interviews.Phase.Name,
                Category = x.Interviews.MedicalCategory.Name,
                Entity = x.EntityAttributes.Entity,
                Attribute = x.EntityAttributes.Attribute,
                Type = x.EntityAttributes.Type,
                QuestionNumber = x.QuestionNumber

            };

        public static Expression<Func<Questions, QuestionResponseOptionInfo>> QuestionResponseOptionsExpression { get; } =
          (q) => new QuestionResponseOptionInfo()
          {
                Id = q.Id,
                Category = q.Interviews.MedicalCategory.Name,
                Question = q.Description,
                Interview = q.Interviews.Name,
                InterviewId = q.InterviewId,
                ResponseOptions = q.ResponseOptions
                                    .Select(z => new ResponseOptionInfo()
                                    {
                                        Id = z.Id,
                                        Description = z.Description,
                                        QuestionId = z.QuestionId,
                                        ResponseId = 0,
                                        Type = z.Type,
                                       
                                        ResponseNumber = z.ResponseNumber
                                    } as IResponseOptionInfo).ToList(),
                 PatientResponses = q.PatientResponses.SelectMany(pr => pr.Response).Select(z => new ResponseOptionInfo()
                 {
                     PatientResponseId = z.Id,
                     PatientVisitId = z.PatientResponses.PatientVisit.Id,
                     PatientId = z.PatientResponses.PatientVisit.PatientId,
                     Id = z.ResponseOptionId,
                     Description = z.ResponseOptions.Description,
                     QuestionId = z.ResponseOptions.QuestionId,
                     ResponseId = z.Id,
                     Value = z.Value,
                     Type = z.ResponseOptions.Type
                 } as IResponseOptionInfo).ToList()
          };

        public static Expression<Func<Persons_Doctor, DoctorInfo>> DoctorInfoExpression { get; } =
                        x => new DoctorInfo()
                        {
                            Id = x.Id,
                            Name = string.Join(" ", x.Persons.PersonNames.Select(z => z.PersonName)),
                            Code = x.Code
                        };

    }


}
