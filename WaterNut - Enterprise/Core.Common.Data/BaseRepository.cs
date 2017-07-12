//using Core.Common.Contracts;
//using System;
//using System.ComponentModel;






//namespace Core.Common.Data
//{
   
//        public abstract class BaseRepository<U> : IDataRepository, IDisposable
//       where U : new()
//        {
//            public BaseRepository(string conn)
//            {
//                var designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
//                if (designMode)
//                {
//                    conText = (U)Activator.CreateInstance(typeof(U), conn);
//                }
//                else
//                {
//                    conText = (U)Activator.CreateInstance(typeof(U));
//                }
                
               

//            }
//            public BaseRepository()
//            {

//                conText = (U)Activator.CreateInstance(typeof(U));

//            }
//            public U conText;


//            public bool SaveChanges<T>(T obj) 
//            {
//                // conText.AttachTo(EntitySet,emp);

//                try
//                {
//                    if (obj == null) return false;

                   
//                   // obj.AcceptChanges();
//                    return true;

//                }
//                catch (Exception ex)
//                {

//                    throw ex;
//                }
                
//                   // return false;
               

//            }
//            public void Delete<T>(T obj) 
//            {
               
//                try
//                {

//                    if (obj == null) return;
                   
                 
//                    //obj.AcceptChanges();
//                }
//                catch (Exception ex)
//                {

//                    throw ex;
//                }
//            }


            



//            #region IDisposable Members
//            private bool _disposed;
//            public void Dispose()
//            {
//                Dispose(true);

//                // Use SupressFinalize in case a subclass 
//                // of this type implements a finalizer.
//                GC.SuppressFinalize(this);
//            }
//            protected virtual void Dispose(bool disposing)
//            {
//                if (!_disposed)
//                {
//                    if (disposing)
//                    {
//                        // Clear all property values that maybe have been set
//                        // when the class was instantiated
                    
//                    }

//                    // Indicate that the instance has been disposed.
//                    _disposed = true;
//                }
//            }
//            #endregion
//        }

    
//}
