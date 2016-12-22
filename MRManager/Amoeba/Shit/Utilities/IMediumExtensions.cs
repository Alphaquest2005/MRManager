using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Model.Interfaces.MREntitiesQS;

namespace Utilities
{
   public static class IMediumExtensions
    {
       public static List<BitmapImage> GetImages(this List<IMedium> lst)
       {
           if (lst != null && lst.Any()) return lst.Select(ConvertByteToImage).ToList();
           return new List<BitmapImage>();
       }

       private static BitmapImage ConvertByteToImage(IMedium itm)
       {
           BitmapImage bi = new BitmapImage();
           bi.BeginInit();
           bi.StreamSource = new MemoryStream(itm.Media);
           bi.EndInit();
           bi.Freeze();
           return bi;
       }
    }
}
