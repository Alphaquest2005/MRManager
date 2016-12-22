﻿// <autogenerated>
//   This file was generated by T4 code generator Amoeba-Master.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System.ComponentModel;
using System;
using EF.Entities;
using Interfaces;

namespace DesignTime
{
	public class DesignDataContext 
	{
		private static readonly DesignDataContext _instance = new DesignDataContext();

		public static DesignDataContext Instance { get; } = _instance;

		public static IApplicationEntities ApplicationEntities {get;} = new ApplicationEntities {ApplicationId = Convert.ToInt32(@"2"),EntityId = Convert.ToInt32(@"6396"),Id = Convert.ToInt32(@"7303")};
	 
		public static IApplications Applications {get;} = new Applications {Id = Convert.ToInt32(@"1"),Name = Convert.ToString(@"Amoeba")};
	 
		public static IDataProperties DataProperties {get;} = new DataProperties {DataTypeId = Convert.ToInt32(@"1"),EntityPropertyId = Convert.ToInt32(@"21"),Id = Convert.ToInt32(@"22"),MaxLength = Convert.ToInt32(@"0"),ModelTypeId = Convert.ToInt32(@"1")};
	 
		public static IDataTypes DataTypes {get;} = new DataTypes {Id = Convert.ToInt32(@"12"),DBType = Convert.ToString(@"Null"),Name = Convert.ToString(@"this IEnumerable<string>")};
	 
		public static IEntities Entities {get;} = new Entities {Id = Convert.ToInt32(@"6588"),EntitySetName = Convert.ToString(@"Views"),Name = Convert.ToString(@"Views"),SchemaName = Convert.ToString(@"dbo")};
	 
		public static IEntityProperties EntityProperties {get;} = new EntityProperties {Id = Convert.ToInt32(@"21263"),EntityId = Convert.ToInt32(@"6548"),PropertyName = Convert.ToString(@"RelationshipTypeId")};
	 
		public static IEntityRelationships EntityRelationships {get;} = new EntityRelationships {ChildEntityPropertyId = Convert.ToInt32(@"10095"),Id = Convert.ToInt32(@"3188"),ParentEntityPropertyId = Convert.ToInt32(@"9833"),RelationshipTypeId = Convert.ToInt32(@"2")};
	 
		public static IEntityView EntityView {get;} = new EntityView {EntityId = Convert.ToInt32(@"6399"),Id = Convert.ToInt32(@"900"),Name = Convert.ToString(@"AddressInfo")};
	 
		public static IEntityViewEntityProperties EntityViewEntityProperties {get;} = new EntityViewEntityProperties {EntityPropertyId = Convert.ToInt32(@"20769"),Id = Convert.ToInt32(@"3350")};
	 
		public static IEntityViewProperties EntityViewProperties {get;} = new EntityViewProperties {EntityViewId = Convert.ToInt32(@"900"),Id = Convert.ToInt32(@"3350"),Name = Convert.ToString(@"City")};
	 
		public static IEntityViewViewProperties EntityViewViewProperties {get;} = new EntityViewViewProperties {EntityViewPropertyId = Convert.ToInt32(@"3355"),Id = Convert.ToInt32(@"3416")};
	 
		public static ILayers Layers {get;} = new Layers {Id = Convert.ToInt32(@"1"),Name = Convert.ToString(@"Solution")};
	 
		public static IModelTypes ModelTypes {get;} = new ModelTypes {Id = Convert.ToInt32(@"1"),Name = Convert.ToString(@"EntityId")};
	 
		public static IParameters Parameters {get;} = new Parameters {DataTypeId = Convert.ToInt32(@"12"),Id = Convert.ToInt32(@"1"),Name = Convert.ToString(@"strings")};
	 
		public static IPrimaryKeyOptions PrimaryKeyOptions {get;} = new PrimaryKeyOptions {Id = Convert.ToInt32(@"24900"),};
	 
		public static IProjects Projects {get;} = new Projects {Id = Convert.ToInt32(@"1"),Name = Convert.ToString(@"Entities")};
	 
		public static IRelationshipTypes RelationshipTypes {get;} = new RelationshipTypes {Id = Convert.ToInt32(@"2"),Name = Convert.ToString(@"1:Many")};
	 
		public static ISettings Settings {get;} = new Settings {ApplicationId = Convert.ToInt32(@"1"),Id = Convert.ToInt32(@"1"),LayerId = Convert.ToInt32(@"1"),Name = Convert.ToString(@"ApplicationFolder"),ProjectId = Convert.ToInt32(@"2"),Value = Convert.ToString(@"C:\Prism\Clients\Amoeba\Amoeba")};
	 
		public static ITestValues TestValues {get;} = new TestValues {EntityPropertyId = Convert.ToInt32(@"21183"),Id = Convert.ToInt32(@"24519"),RowId = Convert.ToInt32(@"7303"),Value = Convert.ToString(@"2")};
	 
	}
}
