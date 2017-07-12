
using Core.Common.Data.Contracts;
//using Newtonsoft.Json;
using System.Runtime.Serialization;



namespace Core.Common.DataLayer.Entities
{
   // [JsonObject(IsReference = true)]
    [DataContract(IsReference = true, Namespace = "http://www.insight-software.com/MRManager")]
    public abstract class BaseDTO<T> //: IIdentifiableEntity where T : IIdentifiableEntity
    {
        //[NotMapped]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[IgnoreDataMember]
        [IgnoreDataMember]
        public virtual string EntityId { get; }   
        
        //[NotMapped]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[IgnoreDataMember]
        [IgnoreDataMember]
        public virtual string EntityName { get;}

    }
}
