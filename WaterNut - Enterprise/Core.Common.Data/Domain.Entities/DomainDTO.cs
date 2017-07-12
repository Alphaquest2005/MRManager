//using System.ComponentModel.DataAnnotations.Schema;
using Core.Common.Data.Contracts;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Domain.DTO
{
   // [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://www.insight-software.com/MRManager")]
    public abstract partial class BaseDTO<T> : IDto where T : IDto
    {

        [IgnoreDataMember]
        public virtual string EntityId { get; }
  
        [IgnoreDataMember]
        public virtual string EntityName
        {
            get
            {
                return EntityId.ToString();
            }
           
        }


    }
}
