﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using FluentValidation;
using Interfaces;

namespace ValidationSets
{
	
	public class IEntityViewValidator : AbstractValidator<IEntityView>
	{
		public IEntityViewValidator()
		{
			CascadeMode = CascadeMode.Continue;
			RuleFor(x => x.EntityId).NotNull();
			RuleFor(x => x.Id).NotNull();
			RuleFor(x => x.Name).NotNull();
		}
	}
}
