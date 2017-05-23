﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-UnitTests.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using Common;
using Common.Dynamic;
using EF.DBContexts;
using EF.Entities;
using Entity.Expressions;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;

namespace UnitTests.Expressions
{
	[TestClass]
	public class  AutoExpressionTests
	{

        [TestMethod]
        public void SignInInfoExpressionGetData()
        {
            var res = MRManagerDBContext.Instance.UserSignIn.Select(PatientExpressions.SignInInfoExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }
        [TestMethod]
        public void InterviewInfoExpressionGetData()
        {
            var res = MRManagerDBContext.Instance.Interviews.Select(PatientExpressions.InterviewInfoExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }

        [TestMethod]
        public void PatientResponseInfoExpressionGetData()
        {
            var res = MRManagerDBContext.Instance.PatientResponses.Select(PatientExpressions.PatientResponseInfoExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }

        [TestMethod]
        public void PulledPatientDetailsExpressionGetData()
        {

            var res = MRManagerDBContext.Instance.Patients.Select(PulledExpressions.PatientDetailsInfoExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }

        [TestMethod]
        public void QuestionResponseOptionsExpressionExpressionGetData()
        {
            var res = MRManagerDBContext.Instance.Questions.Select(PatientExpressions.QuestionResponseOptionsExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }

        [TestMethod]
        public void PatientVistInfoExpressionGetData()
        {
            var res = MRManagerDBContext.Instance.PatientVisit.Select(PulledExpressions.PatientVistInfoExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }

        [TestMethod]
        public void PulledPatientNonResidentExpressionGetData()
        {
            var res = MRManagerDBContext.Instance.Patients.Select(PulledExpressions.PatientNonResidentInfoExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }

        [TestMethod]
        public void PulledPatientVitalsExpressionGetData()
        {
            var res = MRManagerDBContext.Instance.Patients.Select(PulledExpressions.PatientVitalsInfoExpression).ToList();
            if (res.Any()) Debug.Assert(true);
        }

        [TestMethod]
        public void DistinctTest()
        {
            var res = MRManagerDBContext.Instance.UserSignIn.Select(PatientExpressions.SignInInfoExpression).Where("Usersignin = \"joe\"").ToList();
            if (res.Any()) Debug.Assert(true);
           
        }

        [TestMethod]
        public void RefactorPatientDetailsTest()
        {
            using (var ctx = new MRManagerDBContext())
            {
                var res =
                    ctx.PatientResponses.Where(x => x.PatientVisit.PatientId == 1)
                                        .Where(x => x.Questions.EntityAttributes.Entity == "Patient")
                                        .SelectMany(x => x.Response)
                                        .GroupBy(x => x.PatientResponses.Questions.EntityAttributes.Attribute)
                                        .Select(g => new KeyValuePair<string, dynamic>(g.Key, g.Any()?g.First().Value:null)).ToList();
                var p = new PatientDetailsInfo();
                res.ForEach(x => p.ApplyChanges(x));
                if (res != null) Debug.Assert(true);
            }
        }

        [TestMethod]
        public void OnlyPullInterfacePropertiesTest()
        {
            using (var ctx = new MRManagerDBContext())
            {
                var props = typeof (IPatientInfo).GetProperties().ToList();
                var res =
                    ctx.PatientResponses.Where(x => x.PatientVisit.PatientId == 1)
                                        .Where(x => x.Questions.EntityAttributes.Entity == "Patient" && props.Any(z => z.Name == x.Questions.EntityAttributes.Attribute))
                                        .SelectMany(x => x.Response)
                                        .GroupBy(x => x.PatientResponses.Questions.EntityAttributes.Attribute)
                                        .Select(g => new KeyValuePair<string, dynamic>(g.Key, g.Any() ? g.First().Value : null)).ToList();
                var p = new PatientInfo();
                res.ForEach(x => p.ApplyChanges(x));
                if (res != null) Debug.Assert(true);
            }
        }

        [TestMethod]
        public void PulledMainEntitiesTest()
        {
            var starttime = DateTime.Now;

            using (var ctx = new MRManagerDBContext())
            {
                var props = typeof(IPatientInfo).GetProperties().ToList();
                var entities =
                    ctx.PatientResponses.Where(
                        x =>
                            x.Questions.EntityAttributes.Entity == "Patient" &&
                            props.Any(z => z.Name == x.Questions.EntityAttributes.Attribute))
                        .GroupBy(x => new {x.PatientVisit.PatientId})
                        .Select(g => new EntityKeyPair(){ Id = g.Key.PatientId,
                                                          Changes = g.SelectMany(q => q.Response)
                                                                   .GroupBy(w => w.PatientResponses.Questions.EntityAttributes.Attribute)
                                                                                .Select(rg => new KeyValuePair<string, dynamic>(
                                                                                          rg.Key,
                                                                                          rg.Any() ? rg.First().Value : null)).ToList()}).ToList();



              var res = entities.Select(x => new PatientInfo().ApplyChanges(x.Changes));
              var ticks = (DateTime.Now - starttime).Milliseconds;
              if (res.Any()) Debug.Assert(true);
            }
        }

        [TestMethod]
        public void PulledSubEntitiesTest()
        {
            var starttime = DateTime.Now;

            using (var ctx = new MRManagerDBContext())
            {
                var props = typeof(IPatientInfo).GetProperties().ToList();
                var entities =
                    ctx.PatientResponses.Where(
                        x =>
                            x.Questions.EntityAttributes.Entity == "Contact" &&
                            props.Any(z => z.Name == x.Questions.EntityAttributes.Attribute))
                        .GroupBy(x => new { x.PatientVisit.PatientId, x.QuestionId })
                        .Select(g => new SubEntitiesKeyPair()
                        {
                            Id = g.Key.QuestionId,
                            EntityId = g.Key.PatientId,
                            Changes = g.SelectMany(q => q.Response)
                                                                   .GroupBy(w => w.PatientResponses.Questions.EntityAttributes.Attribute)
                                                                                .Select(rg => new KeyValuePair<string, dynamic>(
                                                                                          rg.Key,
                                                                                          rg.Any() ? rg.First().Value : null)).ToList()
                        }).ToList();



                var res = entities.Select(x => new Expando().ApplyChanges(x.Changes)).ToList();
                var ticks = (DateTime.Now - starttime).Milliseconds;
                if (res.Any()) Debug.Assert(true);
            }
        }

        [TestMethod]
        public void PulledPatientInfoTest()
        {
            var starttime = DateTime.Now;
            var res = MRManagerDBContext.Instance.Patients.Select(PulledExpressions.PatientInfoExpression).ToList();
            var ticks = (DateTime.Now - starttime).Milliseconds;
            if (res.Any()) Debug.Assert(true);

        }

        [TestMethod]
        public void RefactorNonResidentTest()
        {
            using (var ctx = new MRManagerDBContext())
            {
                var res =
                    ctx.PatientResponses.Where(x => x.PatientVisit.PatientId == 1)
                                        .Where(x => x.Questions.EntityAttributes.Entity == "NonResident")
                                        .SelectMany(x => x.Response)
                                        .GroupBy(x => x.PatientResponses.Questions.EntityAttributes.Attribute)
                                        .Select(g => new KeyValuePair<string, dynamic>(g.Key, g.Any() ? g.First().Value : null)).ToList();
                var p = new NonResidentInfo();
                res.ForEach(x => p.ApplyChanges(x));
                if (res != null) Debug.Assert(true);
            }
        }

        [TestMethod]
        public void ConvertToNullable()
        {
            var type = typeof (Nullable<DateTime>);
            var s = "1/1/2011";
            TypeConverter conv = TypeDescriptor.GetConverter(type);
            var result = conv.ConvertFrom(s);

            var res1 = test.ToNullable(s, type);


            Assert.AreEqual(res1, result);
        }

	    

	}
        public static class test
	    {
	        public static dynamic ToNullable(this object s, Type type)
	        {
	            try
	            {
	                TypeConverter conv = TypeDescriptor.GetConverter(type);
	                var result = conv.ConvertFrom(s);
	                return result;
	            }
	            catch
	            {
	            }
	            return null;
	        }
	    }
    public class SubEntitiesKeyPair
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public List<KeyValuePair<string, dynamic>> Changes { get; set; }
    }

    public class EntityKeyPair
    {
        public int Id { get; set; }
        public List<KeyValuePair<string, dynamic>> Changes { get; set; }
    }
}
