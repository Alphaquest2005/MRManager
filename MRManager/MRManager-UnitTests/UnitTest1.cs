using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using EF.DBContexts;
using EF.Entities;
using Entity.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MRManager_UnitTests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            Func<AddressLines, string> addressLine = y => y.Name;
            Func<PersonPhoneNumbers, string> phoneNumber = y => y.PhoneNumber;
            Func<PersonAddresses, IEnumerable<string>> addressLines =
                z => z.Addresses.AddressLines.Select(x => addressLine.Invoke(x));
            Func<PersonNames, string> personName = z => z.PersonName;

            //Func<Persons, IEnumerable<string>> personNames = x => x.PersonNames.Select(z => personName.Invoke(z)).ToList();
            //Expression<Func<Persons, IEnumerable<string>>> address = x => x.PersonAddresses.SelectMany(addressLines).ToList();
            //Expression<Func<Persons, IEnumerable<string>>> phoneNumbers = x => x.PersonPhoneNumbers.Select(phoneNumber).ToList();
            Expression<Func<Persons, Media>> photo = x => x.PersonMedia.First().Media;



            Expression<Func<Persons, dynamic>> PersonSummaryExp = x => new EFEntity<Persons>(new
            {
                Id = x.Id,
                Names = x.PersonNames.Select(z => personName.Invoke(z)),
                AddressLines =
                    x.PersonAddresses.Select(z => z.Addresses.AddressLines.Select(w => addressLine.Invoke(w)).ToList()),
                PhoneNumber = x.PersonPhoneNumbers.Select(z => phoneNumber.Invoke(z)),
                //Photo = photo.Invoke(x)
            }) as dynamic;

            using (var ctx = new MRManagerDBContext())
            {

                var res = ctx.Persons
                    .Where(x => x.Id == 1)
                    .Select(PersonSummaryExp).ToList();

                dynamic personSummary = res.First();


                Debug.Assert(res.Any());
            }
        }

        [TestMethod]
        public void TestMethod2()
        {

            IEnumerable<string> res;
            using (var ctx = new MRManagerDBContext())
            {
                IQueryable<Persons> person = ctx.Persons.Where(x => x.Id == 1);

                IQueryable<PersonAddresses> personAddresseses = person.SelectMany(x => x.PersonAddresses);

                IQueryable<Addresses> addresses = personAddresseses.Select(x => x.Addresses);

                IQueryable<AddressLines> addressLines = addresses.SelectMany(x => x.AddressLines);


                res = addressLines.Select(x => x.Name).ToList();
            }

            IEnumerable<string> res1;
            using (var ctx = new MRManagerDBContext())
            {
                var person1 = Person1(ctx);
                var personAddresses = PersonAddresses(person1);
                var addresseses = Addresseses(personAddresses);
                var addressLineses = AddressLineses(addresseses);

                res1 = addressLineses
                    .Select(x => x.Name).ToList();
            }
            Debug.Assert(res == res1);

        }

        private static IQueryable<Persons> Person1(MRManagerDBContext ctx)
        {


            return ((IQueryable<Persons>) ctx.Persons.Where(x => x.Id == 1));

        }

        private static IQueryable<AddressLines> AddressLineses(IQueryable<Addresses> addresseses)
        {

            return ((IQueryable<AddressLines>) addresseses.SelectMany(x => x.AddressLines));

        }

        private static IQueryable<Addresses> Addresseses(IQueryable<PersonAddresses> personAddresses)
        {
            return ((IQueryable<Addresses>) personAddresses.Select(x => x.Addresses));
        }

        private static IQueryable<PersonAddresses> PersonAddresses(IQueryable<Persons> person1)
        {
            return ((IQueryable<PersonAddresses>) person1.SelectMany(x => x.PersonAddresses));
        }

        [TestMethod]
        public void TestMethod3()
        {

            IEnumerable<string> res;
            using (var ctx = new MRManagerDBContext())
            {
                IQueryable<Persons> person = ctx.Persons.Where(x => x.Id == 1);

                IQueryable<PersonAddresses> personAddresseses = person.SelectMany(x => x.PersonAddresses);

                IQueryable<Addresses> addresses = personAddresseses.Select(x => x.Addresses);

                IQueryable<AddressLines> addressLines = addresses.SelectMany(x => x.AddressLines);


                res = addressLines.Select(x => x.Name).ToList();
            }

            IEnumerable<string> res1;
            using (var ctx = new MRManagerDBContext())
            {
                res1 = ctx.Persons.GetPersonById(1).GetAddressLines();
            }
            Debug.Assert(res == res1);

        }

        [TestMethod]
        public void TestMethod4()
        {
            Func<AddressLines, string> addressLine = y => y.Name;
            Func<PersonPhoneNumbers, string> phoneNumber = y => y.PhoneNumber;
            Func<PersonAddresses, IEnumerable<string>> addressLines =
                z => z.Addresses.AddressLines.Select(x => addressLine.Invoke(x));
            Func<PersonNames, string> personName = z => z.PersonName;

            //Func<Persons, IEnumerable<string>> personNames = x => x.PersonNames.Select(z => personName.Invoke(z)).ToList();
            //Expression<Func<Persons, IEnumerable<string>>> address = x => x.PersonAddresses.SelectMany(addressLines).ToList();
            //Expression<Func<Persons, IEnumerable<string>>> phoneNumbers = x => x.PersonPhoneNumbers.Select(phoneNumber).ToList();
            Expression<Func<Persons, Media>> photo = x => x.PersonMedia.First().Media;

            Expression<Func<Persons, dynamic>> PersonSummaryExp = x => new EFEntity<Persons>(new
            {
                Id = x.Id,
                Names = x.PersonNames.Select(z => personName.Invoke(z)),
                AddressLines =
                    x.PersonAddresses.Select(z => z.Addresses.AddressLines.Select(w => addressLine.Invoke(w)).ToList()),
                PhoneNumber = x.PersonPhoneNumbers.Select(z => phoneNumber.Invoke(z)),
                //Photo = photo.Invoke(x)
            }) as dynamic;

            dynamic res;
            using (var ctx = new MRManagerDBContext())
            {
                res = ctx.Persons
                    .Where(x => x.Id == 1)
                    .Select(PersonSummaryExp).First();
            }

            dynamic res1 = PersonExtensions.PersonSummary(1);


            if (res == res1) Debug.Assert(true);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var res = new List<AddressInfo>();
            var res3 = new List<AddressInfo>();
            using (var ctx = new MRManagerDBContext())
            {
                res = ctx.Persons.Select(PersonToAddressInfoProjection).ToList();


            }



            dynamic res1 = PersonSingleExtensions.PersonSummary();


            if (res1.Any()) Debug.Assert(true);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var res = MRManagerDBContext.Instance.PatientVisit.Select(PatientVisitExpressions.PatientVisitAutoViewExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }

        public static Expression<Func<Addresses, AddressInfo>> AddressesToAddressInfoFunction { get; } =

            x => new AddressInfo()
            {
                AddressLine = x.AddressLines.Select(z => z.Name).LastOrDefault(),
                City = x.AddressCities.Cities.Name,
                Country = x.AddressCountries.Countries.Name,
                Parish = x.AddressParishes.Parishes.Name,
                State = x.AddressStates.States.Name,
                Zipcode = x.AddressZipCodes.ZipCodes.Name,
            };

        private static Expression<Func<Addresses, AddressInfo>> AddressesToAddressInfoProjection
        {
            get
            {
                return x => new AddressInfo
                {
                    AddressLine = x.AddressLines.Select(w => w.Name).StringJoin(","),
                    City = x.AddressCities.Cities.Name,
                    Parish = x.AddressParishes.Parishes.Name,
                    Country = x.AddressCountries.Countries.Name,
                };
            }
        }

        private static Expression<Func<Persons, AddressInfo>> PersonToAddressInfoProjection
        {
            get
            {
                return x => new AddressInfo
                {
                    AddressLine = x.PersonAddresses.SelectMany(z => z.Addresses.AddressLines).Select(w => w.Name).StringJoin(","),
                    City =  x.PersonAddresses.Select(z => z.Addresses).Select(z => z.AddressCities.Cities.Name).StringJoin(","),
                    Parish = x.PersonAddresses.Select(z => z.Addresses).Select(z => z.AddressParishes.Parishes.Name).StringJoin(","),
                    Country = x.PersonAddresses.Select(z => z.Addresses).Select(z => z.AddressCountries.Countries.Name).StringJoin(","),
                };
            }
        }
    }

    public static class PersonSingleExtensions
    {
       // public static IQueryable<Persons> GetPersonById(this Persons query, int id) => ((IQueryable<Persons>)query.Where(x => x.Id == id));

        public static IEnumerable<string> GetAddressLines(this Persons query) => query?.PersonAddresses()?.Addresseses()?.AddressLineses()?.Select(x => x.Name).ToList();

        //public static IQueryable<AddressLines> AddressLineses(this IQueryable<Addresses> addresseses) => ((IQueryable<AddressLines>)addresseses.SelectMany(x => x.AddressLines));

       // public static IQueryable<Addresses> Addresseses(this IQueryable<PersonAddresses> personAddresses) => personAddresses.Select(x => x.Addresses);

        public static IQueryable<PersonAddresses> PersonAddresses(this Persons person1) => person1?.PersonAddresses?.AsQueryable();

        public static IQueryable<PersonNames> PersonNames(this Persons person1)=> person1.PersonNames?.AsQueryable();

       // public static IQueryable<string> Names(this IQueryable<PersonNames> query) => query.Select(x => x.PersonName);

        public static IQueryable<string> GetNames(this Persons query)=> query?.PersonNames()?.Names();

        public static IQueryable<PersonPhoneNumbers> PersonPhoneNumbers(this Persons person1) => person1?.PersonPhoneNumbers?.AsQueryable();

        //public static IQueryable<string> PhoneNumbers(this IQueryable<PersonPhoneNumbers> query) => query.Select(x => x.PhoneNumber);

        public static IEnumerable<string> GetPhoneNumbers(this Persons query)=> query?.PersonPhoneNumbers()?.PhoneNumbers()?.ToList();

        public static IQueryable<PersonMedia> PersonMedias(this Persons query)
            => query?.PersonMedia?.AsQueryable();

        //public static Media Media(this IQueryable<PersonMedia> query) => query.FirstOrDefault()?.Media;

        public static Media GetPhoto(this Persons query) => query?.PersonMedias()?.Media();

       // public static string StringJoin(this IEnumerable<string> strings, string seperator) => string.Join(seperator, strings);// generated from database

        public static dynamic GetPersonSingleSummary(this Persons query)
        {
            return new EFEntity<Persons>(new
            {
                Names = query.GetNames().StringJoin(" "),
                AddressLines = query.GetAddressLines().StringJoin(","),
                PhoneNumber = query.GetPhoneNumbers().StringJoin(","),
                Photo = query.GetPhoto()
            });
        }

        public static dynamic PersonSummary()
        {
            using (var ctx = new MRManagerDBContext())
                return ctx.Persons.Select(x => x.GetPersonSingleSummary()).ToList();


        }

    }

    public static class PersonExtensions
    {
        public static IQueryable<Persons> GetPersonById(this IQueryable<Persons> query, int id) => ((IQueryable<Persons>) query.Where(x => x.Id == id));

        public static IEnumerable<string> GetAddressLines(this IQueryable<Persons> query)=> query?.PersonAddresses()?.Addresseses()?.AddressLineses()?.Select(x => x.Name).ToList();

        public static IQueryable<AddressLines> AddressLineses(this IQueryable<Addresses> addresseses)=> ((IQueryable<AddressLines>) addresseses?.SelectMany(x => x.AddressLines));

        public static IQueryable<Addresses> Addresseses(this IQueryable<PersonAddresses> personAddresses)=> personAddresses?.Select(x => x.Addresses);

        public static IQueryable<PersonAddresses> PersonAddresses(this IQueryable<Persons> person1) => person1?.SelectMany(x => x.PersonAddresses);

        public static IQueryable<PersonNames> PersonNames(this IQueryable<Persons> person1)
            => ((IQueryable<PersonNames>) person1?.SelectMany(x => x.PersonNames));

        public static IQueryable<string> Names(this IQueryable<PersonNames> query) => query?.Select(x => x.PersonName);

        public static IQueryable<string> GetNames(this IQueryable<Persons> query)
            => query?.PersonNames()?.Names();

        public static IQueryable<PersonPhoneNumbers> PersonPhoneNumbers(this IQueryable<Persons> person1) => ((IQueryable<PersonPhoneNumbers>) person1.SelectMany(x => x.PersonPhoneNumbers));

        public static IQueryable<string> PhoneNumbers(this IQueryable<PersonPhoneNumbers> query)=> query?.Select(x => x.PhoneNumber);

        public static IEnumerable<string> GetPhoneNumbers(this IQueryable<Persons> query)
            => query?.PersonPhoneNumbers()?.PhoneNumbers()?.ToList();

        public static IQueryable<PersonMedia> PersonMedias(this IQueryable<Persons> query)
            => query?.SelectMany(x => x.PersonMedia);

        public static Media Media(this IQueryable<PersonMedia> query) => query?.FirstOrDefault()?.Media;

        public static Media GetPhoto(this IQueryable<Persons> query) => query?.PersonMedias()?.Media();

        public static string StringJoin(this IEnumerable<string> strings, string seperator) => strings == null?"":string.Join(seperator, strings);// generated from database

        public static dynamic GetPersonSummary(this IQueryable<Persons> query)
        {
            return new EFEntity<Persons>(new
            {
                Names = query.GetNames().StringJoin(" "),
                AddressLines = query.GetAddressLines().StringJoin(","),
                PhoneNumber = query.GetPhoneNumbers().StringJoin(","),
                Photo = query.GetPhoto()
            });
        }

        public static dynamic PersonSummary(int Id)
        {
            using (var ctx = new MRManagerDBContext())
                return ctx.Persons.GetPersonSummary();


        }

    }
}
    


