﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-EntityViews.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using FluentValidation;
using Interfaces;

namespace ValidationSets
{
	
	public class IHeightInfoValidator : AbstractValidator<IHeightInfo>
	{
		public IHeightInfoValidator()
		{
			CascadeMode = CascadeMode.Continue;
			RuleFor(x => x.Heights).NotNull();
			RuleFor(x => x.Unit).NotNull();
		}
	}
}
