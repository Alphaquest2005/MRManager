﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-DataEntities.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using Common.DataEntites;
using Interfaces;

namespace EF.Entities
{
	public partial class ExamDetailsAutoView: BaseEntity, IExamDetailsAutoView
	{
		public string Components { get; set; }
		public string ExamResults_SimpleValues { get; set; }
		public string Exams { get; set; }

	}
}
