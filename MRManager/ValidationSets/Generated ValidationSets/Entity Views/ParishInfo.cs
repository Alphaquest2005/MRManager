﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using FluentValidation;
using Interfaces;

namespace ValidationSets
{
	
	public class IParishInfoValidator : AbstractValidator<IParishInfo>
	{
		public IParishInfoValidator()
		{
			CascadeMode = CascadeMode.Continue;
			RuleFor(x => x.City).NotNull();
			RuleFor(x => x.Parish).NotNull();
		}
	}
}
