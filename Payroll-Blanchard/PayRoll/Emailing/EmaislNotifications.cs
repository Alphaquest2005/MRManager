using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.IO;

namespace EmailLogger
{

    /// <summary>
    /// Email Notification class that will read emails template files 
    /// </summary>
    public class EmailslNotifications
    {

        #region Variabls
        protected string _subject = null;
        protected string _TemplatesPath = null;
        protected bool _debugmode = false;
        #endregion



        /// <summary>
        /// Email subject
        /// </summary>
        public string EmailSubject
        {
            set { _subject = value; }

        }

        
        /// <summary>
        /// A Switch to toggle Debug Mode or Production Mode. in Debug mode no emails will be sent only the email body will be return to be written in the page.
        /// </summary>
        public bool DebugMode
        {
            set { _debugmode = value; }

        }

        /// <summary>
        /// This function will read the content of a file name
        /// </summary>
        /// <param name="FileName">File Name</param>
        /// <returns>String: Containing the Entire content of the file</returns>
        protected string ReadEmailFile(string FileName)
        {
            string retVal = null;
            try
            {
                //setting the file name path
                string path = _TemplatesPath + FileName;

                //check if the file exists in the location.
                if (!File.Exists(path))
                {
                   return retVal = "";
                }
                    // throw new Exception("Could Not Find the file : " + FileName + " in the location " + _TemplatesPath); // throw an exception here.


                //start reading the file. i have used Encoding 1256 to support arabic text also.
                StreamReader sr = new StreamReader(@path, System.Text.Encoding.GetEncoding(1256));
                retVal = sr.ReadToEnd(); // getting the entire text from the file.
                sr.Close();
            }


            catch (Exception ex)
            {
                throw new Exception("Error Reading File." + ex.Message);


            }
            return retVal;
        }

      

        /// <summary>
        /// This function will return the default email header specified in the "email_header.txt"
        /// </summary>
        /// <returns>String: Contains the entire text of the "email_header.txt"</returns>
        protected string emailheader()
        {
            string retVal = null;
            if (File.Exists(_TemplatesPath + "email_header.txt"))
            {

                retVal = ReadEmailFile("email_header.txt");


            }
            else
               retVal = "";
                //throw new Exception("you should have a file called 'email_header.txt' in the location :" + _TemplatesPath);




            return retVal;
        }


        /// <summary>
        /// This function will return the default email footer specified in the "email_footer.txt"
        /// </summary>
        /// <returns>String: Contains the entire text of the "email_footer.txt"</returns>
        protected string emailfooter()
        {
            string retVal = null;
            if (File.Exists(_TemplatesPath + "email_footer.txt"))
                retVal = ReadEmailFile("email_footer.txt");
            else
                retVal = "";
                //throw new Exception("you should have a file called 'email_footer.txt' in the location :" + _TemplatesPath);


            return retVal;
        }


        /// <summary>
        /// this function will send email. it will read the mail setting from the web.config
        /// </summary>
        /// <param name="SenderEmail">Sender Email ID</param>
        /// <param name="SenderName">Sender Name</param>
        /// <param name="Recep">Recepient Email ID</param>
        /// <param name="cc">CC ids</param>
        /// <param name="email_title">Email Subject</param>
        /// <param name="email_body">Email Body</param>
        protected void SendEmail(string SenderEmail, string SenderName, string Recep, string cc, string email_title, string email_body)
        {
            // creating email message
            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = false;// email body will allow html elements

            // setting the Sender Email ID
            msg.From = new MailAddress(SenderEmail, SenderName);

            // adding the Recepient Email ID
            msg.To.Add(Recep);

            // add CC email ids if supplied.
            if (!string.IsNullOrEmpty(cc))
                msg.CC.Add(cc);

            //setting email subject and body
            msg.Subject = email_title;
            msg.Body = email_body;

            //create a Smtp Mail which will automatically get the smtp server details from web.config mailSettings section
            SmtpClient SmtpMail = new SmtpClient();

            // sending the message.
            SmtpMail.Send(msg);



        }


        /// <summary>
        /// The Constructor Function
        /// </summary>
        /// <param name="EmailHeaderSubject">Email Header Subject</param>
        /// <param name="TemplatesPath">Emails Files Templates</param>
        public EmailslNotifications(string EmailHeaderSubject, string TemplatesPath)
        {
            _subject = EmailHeaderSubject;
            _TemplatesPath = TemplatesPath;

        }
        public EmailslNotifications(string EmailHeaderSubject)
        {
            _subject = EmailHeaderSubject;
            _TemplatesPath = "";

        }



        /// <summary>
        /// This function will send the email notification by reading the email template and substitute the arguments
        /// </summary>
        /// <param name="EmailTemplateFile">Email Template File</param>
        /// <param name="SenderEmail">Sender Email</param>
        /// <param name="SenderName">Sender Name</param>
        /// <param name="RecepientEmail">Recepient Email ID</param>
        /// <param name="CC">CC IDs</param>
        /// <param name="Subject">EMail Subject</param>
        /// <param name="Args">Arguments</param>
        /// <returns>String: Return the body of the email to be send</returns>
        public string SendNotificationEmail(string EmailTemplateFile, string SenderEmail, string SenderName, string RecepientEmail, string CC, string Subject, params string[] Args)
        {
            string retVal = null;

            //reading the file
           // string FileContents = ReadEmailFile(EmailTemplateFile);

            //conactinate the email Header  and Email Body and Email Footer
           // string emailBody = emailheader() + FileContents + emailfooter(); 
                



            //setting formatting the string
            retVal = string.Format("Method Name:{0} \r\n\n Arguments: \r\n\n {1} \r\n\n Exception Type: {2} \r\n\n Message: {3} \r\n\n StackTrace: {4} \r\n\n", Args);



            try
            {
                //check if we are in debug mode or not. to send email
                if (!_debugmode)
                    SendEmail(SenderEmail, SenderName, RecepientEmail, CC, (!string.IsNullOrEmpty(Subject) ? Subject : _subject), retVal);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retVal;


        }







    }

}
