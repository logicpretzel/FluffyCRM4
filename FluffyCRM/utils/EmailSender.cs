using FluffyCRM.Properties;
using System;
using System.Net;
using System.Net.Mail;

namespace FluffyCRM.utils
{
    public class EmailSender
    {
        private static string _port;
        private static string _accountName;
        private static string _password;
        private static string _servername;

        public string GetNotifyMsgBody(string sTitle = "", string sBody = "")
        {
            string font = "font-family: Verdana, Arial, sans-serif; font-size: 32px; ";
            string rc = "<body style=\"" 
                + font + " margin: 10px; padding: 5px; min-width: 100%; width: 90%;\">" 
                + sTitle + "<hr/><table style=\"width: 100%; max-width: 600px; background-color: rgb(60, 101, 234); color: white;\"><tr><td width=\"10px\">&nbsp;&nbsp;&nbsp;</td><table style=\""
                + font + " width: 100%; max-width: 600px; background-color: steelblue; color: white;\"  ><tr>"
                + String.Format("<td>\n{0}\n</td>",sBody) 
                + " </tr></table></td></tr></table></body>";
 
            
            return rc;
        }

        //public string Send(string to, string subject, string body, bool isHtml, string[] attachments)
        //{
        //    MailAddressCollection To = new MailAddressCollection();
        //    To.Add(to);
        //    return Send(To,  subject, body, isHtml, attachments);
        //}


        // public string  Send(MailAddressCollection toAddresses, string subject, string body, bool isHtml, string[] attachments)
        public string Send( string to, string subject, string body, bool isHtml, string[] attachments)
        {
            string  rc = "";
            string sBCC = "";
            int port = 0;
            MailMessage mailMessage;

            _accountName = Settings.Default.FLMailUserName.ToString();
            _password = Settings.Default.FLMailPassWord.ToString();
            sBCC = Settings.Default.FLMailBCCAddress.ToString();
            _servername = Settings.Default.FLServerName.ToString();
            _port = Settings.Default.FLServerPort.ToString();

            if (!int.TryParse(_port, out port))
            {
                port = 25;
            }

            try
            {
                mailMessage = new MailMessage(_accountName, to, subject, body);
                mailMessage.IsBodyHtml = isHtml;
                MailAddress bcc;
                bcc = new MailAddress(sBCC);
                mailMessage.Bcc.Add(bcc);
                if (attachments != null)
                {
                    for (int i = 0; i < attachments.Length; i++)
                    {
                        mailMessage.Attachments.Add(new Attachment(attachments[i]));
                    }
                }

                if (mailMessage != null)
                {
                    SmtpClient smtp = new SmtpClient(_servername, port);

                    smtp.Timeout = 90000;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_accountName, _password);
                    smtp.EnableSsl = false;

                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    smtp.Send(mailMessage);
                }
            }
            catch (Exception e)
            {
                rc = e.Message;
            }


            return rc;



        }


    }

}
