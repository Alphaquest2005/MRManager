﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-EntityViews.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using FluentValidation;
using Interfaces;

namespace ValidationSets
{
	
	public class ISignInInfoValidator : AbstractValidator<ISignInInfo>
	{
		public ISignInInfoValidator()
		{
			CascadeMode = CascadeMode.Continue;
			RuleFor(x => x.Medias).NotNull();
			RuleFor(x => x.Usersignin).NotNull();
			RuleFor(x => x.Password).NotNull();
		}
	}
}