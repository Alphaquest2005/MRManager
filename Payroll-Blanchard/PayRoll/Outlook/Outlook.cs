

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Outlook;
using System.IO;


namespace MyOutlook
{
    public class Mail
    {
       static Application oApp = new Application();
        public static void CreateDraft(string sender, string subject,string body, string recipient, string attachment)
        {
            try
            {
                if (oApp == null) oApp = new Application();
                // Create the Outlook application by using inline initialization.
               // Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();

                //Create the new message by using the simplest approach.
                MailItem oMsg = (MailItem)oApp.CreateItem(OlItemType.olMailItem);

                //Add a recipient.
                // TODO: Change the following recipient where appropriate.
                Recipient oRecip = (Recipient)oMsg.Recipients.Add(recipient);
                oRecip.Resolve();

                //Set the basic properties.
                oMsg.Subject = subject;
                oMsg.Body = body;

                //Add an attachment.
                // TODO: change file path where appropriate
                Attachment oAttach;
                FileInfo f = new FileInfo(attachment);
                if (f.Exists == true)
                {
                    

                    String sDisplayName = f.Name;
                    int iPosition = (int)oMsg.Body.Length + 1;
                    int iAttachType = (int)OlAttachmentType.olByValue;
                   oAttach = oMsg.Attachments.Add(attachment, iAttachType, iPosition, sDisplayName);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("Attachment dose not exist '{0}'", attachment));
                    return;
                }
                // If you want to, display the message.
                // oMsg.Display(true);  //modal

                //Send the message.
                oMsg.Save();
                //oMsg.Send();

                //Explicitly release objects.
                oRecip = null;
                oAttach = null;
                oMsg = null;
                oApp = null;
            }

                 // Simple error handler.
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("{0} Exception caught: ", e));
               // Console.WriteLine("{0} Exception caught: ", e);
            }

        }

    }
}
