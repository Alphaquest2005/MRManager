﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-EntityViews.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using FluentValidation;
using Interfaces;

namespace ValidationSets
{
	
	public class ITemperatureInfoValidator : AbstractValidator<ITemperatureInfo>
	{
		public ITemperatureInfoValidator()
		{
			CascadeMode = CascadeMode.Continue;
			RuleFor(x => x.Temperatures).NotNull();
			RuleFor(x => x.Unit).NotNull();
		}
	}
}
