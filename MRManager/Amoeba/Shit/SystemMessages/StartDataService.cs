using System;
using System.ComponentModel.Composition;
using StartUp.Messages;

namespace SystemMessages
{
    [Export]
    public class StartDataService : IStartDataService
    {
        static StartDataService()
        {
            try
            {
                //mef
                
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
