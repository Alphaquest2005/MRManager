﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-EntityViews.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.ComponentModel.Composition;
using SystemInterfaces;

namespace Interfaces
{
	
	public partial interface ISignInInfo:IEntityView<IUserSignIn>, IUser
    {
		Byte[] Medias { get;}
		string Usersignin { get;}
		string Password { get;}



	}
}
