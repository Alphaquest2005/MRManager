﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using FluentValidation;
using Interfaces;

namespace ValidationSets
{
	
	public class IResponseSuggestionsValidator : AbstractValidator<IResponseSuggestions>
	{
		public IResponseSuggestionsValidator()
		{
			CascadeMode = CascadeMode.Continue;
			RuleFor(x => x.Id).NotNull();
			RuleFor(x => x.Id).NotNull();
		}
	}
}
