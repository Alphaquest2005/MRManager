using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;

namespace PopClient
{
    /// <summary>
    /// Adds some functionality to the Attachment class
    /// </summary>
    public static class AttachmentExtensions
    {
        public static void Save(this Attachment source, string path)
        {
            byte[] bytes = new byte[65536];

            using (MemoryStream ms = new MemoryStream())
            {
                source.ContentStream.Seek(0, SeekOrigin.Begin);

                int bytesRead;
                
                while ((bytesRead = source.ContentStream.Read(bytes, 0, bytes.Length)) > 0)
                {
                    ms.Write(bytes, 0, bytesRead);
                }

                ms.Flush();

                if (String.IsNullOrEmpty(Path.GetFileName(path)))
                {
                    path = Path.Combine(path, source.Name);
                }

                File.WriteAllBytes(path, ms.GetBuffer());
            } 
        }
    }
}
