﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using FluentValidation;
using Interfaces;

namespace ValidationSets
{
	
	public class IPersonInfoValidator : AbstractValidator<IPersonInfo>
	{
		public IPersonInfoValidator()
		{
			CascadeMode = CascadeMode.Continue;
			RuleFor(x => x.Email).NotNull();
			RuleFor(x => x.PhoneNumber).NotNull();
		}
	}
}
