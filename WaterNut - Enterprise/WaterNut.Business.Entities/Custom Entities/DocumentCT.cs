﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using DocumentDS.Business.Entities;
using DocumentItemDS.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterNut.Business.Entities
{
    public class DocumentCT
    {

        public DocumentCT()
        {
            DocumentItems = new List<xcuda_Item>();
            Document = new xcuda_ASYCUDA(true);
           
        }
        [IgnoreDataMember]
        [NotMapped]
        public xcuda_ASYCUDA Document { get; set; }
        [IgnoreDataMember]
        [NotMapped]
        public List<xcuda_Item> DocumentItems { get; set; }

        
    }
}
