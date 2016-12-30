using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Interfaces;


namespace Utilities
{
   public static class IMediumExtensions
    {
       public static List<BitmapImage> GetImages(this List<IMedia> lst)
       {
           if (lst != null && lst.Any()) return lst.Select(ConvertByteToImage).ToList();
           return new List<BitmapImage>();
       }

       private static BitmapImage ConvertByteToImage(IMedia itm)
       {
           BitmapImage bi = new BitmapImage();
           bi.BeginInit();
            //TODO: implement this
          // bi.StreamSource = new MemoryStream(itm.Value);
           bi.EndInit();
           bi.Freeze();
           return bi;
       }
    }
}
