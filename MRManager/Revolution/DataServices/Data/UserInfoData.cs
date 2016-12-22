using System.Collections.Generic;
using SystemInterfaces;
using DesignTimeData;
using Interfaces;
using RevolutionEntities.Process;

namespace DataServices.Actors
{
    public class UserInfoData
    {
        public static List<IUser> Users = new List<IUser>()
        {
            new User(DesignDataContext.SampleData<IPersons>(), "test", "joe")
            
        };

    }





}