using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Core.Common.Contracts;

namespace Core.Common.Client.Repositories
{
    public class BaseRepository<T>:IDisposable
    {

        public void Dispose()
        {
           // this.Dispose();
        }

        
    }
}
