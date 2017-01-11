using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;


namespace Utilities
{
   public static class IMediumExtensions
    {
       //public static List<BitmapImage> GetImages(this List<IMedia> lst)
       //{
       //    if (lst != null && lst.Any()) return lst.Select(ConvertByteToImage).ToList();
       //    return new List<BitmapImage>();
       //}

       public static BitmapImage ConvertByteToImage(this Byte[] itm)
       {
           if (itm == null) return null;
           BitmapImage bi = new BitmapImage();
           bi.BeginInit();
            //TODO: implement this
           bi.StreamSource = new MemoryStream(itm);
           bi.EndInit();
           bi.Freeze();
           return bi;
       }
    }
}
