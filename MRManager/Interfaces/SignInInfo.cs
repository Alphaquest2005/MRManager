﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-EntityViews.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
	[InheritedExport]
	public partial interface ISignInInfo:IEntityView<IUserSignIn>, IUser
    {
		Byte[] Medias { get;}
		string Usersignin { get;}
		string Password { get;}



	}

    [InheritedExport]
    public partial interface IPatientInfo : IEntityView<IPersons_Patient>
    {
        string Email { get; }
        string PhoneNumber { get; }
        string Country { get; }
        DateTime? DateOfBirth { get; }
        int? CountryId { get; }
        string Sexed { get; }
    }
}