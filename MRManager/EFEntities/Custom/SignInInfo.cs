﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-EntityViews.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.ComponentModel.Composition;
using System.Windows.Media.Imaging;
using SystemInterfaces;
using Common.DataEntites;
using EF.Entities;
using Interfaces;
using Utilities;

namespace EF.Entities
{
	[Export]
	public partial class SignInInfo: EntityView<IUserSignIn>, ISignInInfo
	{
		public Byte[] Medias { get; set; }
		public string Usersignin { get; set; }
		public string Password { get; set; }

		public BitmapImage Image => Medias.ConvertByteToImage();


		public string UserId => Usersignin;
	}
}
