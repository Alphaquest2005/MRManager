﻿

//using System;
//using System.ComponentModel.Composition;
//using System.Xml;
//using DataInterfaces;

//namespace DataInterfaces
//{
//    [InheritedExport]
//    public partial interface IMedia : IEntity
//    {

//        Byte[] Value { get; }

//        int MediaTypeId { get; }
//    }

//    public partial class Media : IMedia
//    {
//        public Media(byte[] media, int id, int mediaTypeId)
//        {
//            Value = media;
//            Id = id;
//            MediaTypeId = mediaTypeId;
//        }

//        public Media()
//        {
//        }

//        public Byte[] Value { get; set; }

//        public int Id { get; set; }
//        public RowState RowState { get; set; } = RowState.Loaded;
//        public int MediaTypeId { get; set; }
//    }

//}
