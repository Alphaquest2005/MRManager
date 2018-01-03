using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace PopClient
{
    /// <summary>
    /// This class is used to convert a CDO message instance to a MailMessage object
    /// as well as exposing properties not present in MailMessage.
    /// </summary>
    internal class CDOMessageConverter
    {
        private CDO.Message cdoMessage;

        public CDOMessageConverter(string mailContent)
        {
            cdoMessage = new CDO.Message();
            ADODB.Stream adoStream = cdoMessage.GetStream();
            adoStream.Type = ADODB.StreamTypeEnum.adTypeText;
            adoStream.WriteText(mailContent.Trim(), ADODB.StreamWriteEnum.adWriteLine);
            adoStream.Flush();
            adoStream.Close();
        }

        public DateTime ReceivedTime
        {
            get
            {
                return cdoMessage.ReceivedTime;
            }
        }

        public MailMessage ToMailMessage()
        {
            MailMessage message = new MailMessage();

            if (!String.IsNullOrWhiteSpace(cdoMessage.From))
            {
                message.From = new MailAddress(cdoMessage.From);
            }

            if (!String.IsNullOrWhiteSpace(cdoMessage.Sender))
            {
                message.Sender = new MailAddress(cdoMessage.Sender);
            }

            message.Subject = cdoMessage.Subject;

            if (!String.IsNullOrWhiteSpace(cdoMessage.To))
            {
                message.To.Add(cdoMessage.To);
            }

            if (!String.IsNullOrWhiteSpace(cdoMessage.CC))
            {
                message.CC.Add(cdoMessage.CC);
            }

            if (!String.IsNullOrWhiteSpace(cdoMessage.BCC))
            {
                message.Bcc.Add(cdoMessage.BCC);
            }

            if (String.IsNullOrWhiteSpace(cdoMessage.HTMLBody))
            {
                message.Body = cdoMessage.TextBody;
            }
            else
            {
                message.Body = cdoMessage.HTMLBody;
                message.IsBodyHtml = true;
            }

            if (!String.IsNullOrWhiteSpace(cdoMessage.ReplyTo))
            {
                message.ReplyToList.Add(cdoMessage.ReplyTo);
            }

            foreach (CDO.IBodyPart bodyPart in cdoMessage.Attachments)
            {
                ADODB.Stream stream = bodyPart.GetDecodedContentStream();
                IStream comStream = stream as IStream;
                byte[] bytes = new byte[stream.Size];
                ulong bytesRead = 0;
                var handle = GCHandle.Alloc(bytesRead, GCHandleType.Pinned);
                IntPtr pcbRead = handle.AddrOfPinnedObject();
                comStream.Read(bytes, stream.Size, pcbRead);
                handle.Free();
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    message.Attachments.Add(new Attachment(memoryStream, bodyPart.FileName));
                }
            }

            foreach (ADODB.Field field in cdoMessage.Fields)
            {
                if (field.Type == ADODB.DataTypeEnum.adBSTR || field.Type == ADODB.DataTypeEnum.adDate)
                {
                    string key = field.Name.Split(':').Last();
                    string value = field.Value.ToString();
                    if (key != null && value != null)
                    {
                        message.Headers.Add(key, value);
                    }
                }

            }

            return message;
        }
    }
}

