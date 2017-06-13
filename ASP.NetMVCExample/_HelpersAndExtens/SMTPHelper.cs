using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml;

namespace ASP.NetMVCExample.SMTPHelpers
{
    // simply a set of containers and a loader extention yje XML reader tom make later code easier to read
    #region DataAndLoaderExtention
    /// <summary>
        /// the data conainer for the load only used in constructor
        /// </summary>
    internal class SMTPClientLoadOpt
    {
        public MailAddress DefaultFrom = null;
        public string Host = null;
        public string Account = null;
        public string Password = null;
        public int? Port = null; //465 

    }

    /// <summary>
    /// just a one time use extiontion for the reader makes later code easier to read is all only used in constructor
    /// </summary>
    internal static class SMTPClientLoadOptLoader
    {
        public static SMTPClientLoadOpt ReadContentAsSMTPClientLoadOpt(this XmlReader Loader)
        {
            SMTPClientLoadOpt Return = new SMTPClientLoadOpt();
            while (!Loader.Read())
            {
                switch (Loader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (Loader.Name)
                        {
                            case "Host":
                                Return.Host = Loader.ReadContentAsString();
                                break;
                            case "Port":
                                Return.Port = Loader.ReadContentAsInt();
                                break;
                            case "DefaultFrom":
                                Return.DefaultFrom = new MailAddress(Loader.ReadContentAsString());
                                break;
                            case "Account":
                                Return.Account = Loader.ReadContentAsString();
                                break;
                            case "Password":
                                Return.Password = Loader.ReadContentAsString();
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (Loader.Name == "SMTPClientLoadOpt")
                        {
                            if (Return.Account != null && Return.DefaultFrom != null && Return.Host != null && Return.Password != null && Return.Port != null)
                            {
                                string returnMSG = "";
                                returnMSG += Return.Account == null ? "Account" : "";
                                returnMSG += Return.DefaultFrom == null ? " DefaultFrom" : "";
                                returnMSG += Return.Host == null ? " Host" : "";
                                returnMSG += Return.Password == null ? " Password" : "";
                                returnMSG += Return.Port == null ? " Port" : "";
                                returnMSG += " nodes were not found in SMTPClientLoadOpt befor its end";
                                throw new Exception(returnMSG);
                            }
                            else
                                return Return;
                        }
                        break;
                }
            }
            throw new Exception("unexpected end og file found befor end of SMTPClientLoadOpt");
        }
    }
    #endregion

    /// <summary>
    /// A class for use when sending emails will send emails 
    /// </summary>
    public class SMTPClient : IDisposable
    {
        /// <summary>
        /// The default file to load from
        /// </summary>
        const string DefaultFileName = "\\ServerOptions.config";

        /// <summary>
        /// Our Default encoded message
        /// </summary>
        public Encoding DefaultEncoding = UTF8Encoding.UTF8;

        /// <summary>
        /// the Cliet behind the client?
        /// </summary>
        SmtpClient Client;
        
        /// <summary>
        /// The Default sender to call our selves
        /// </summary>
        public MailAddress DefaultFrom;

        /// <summary>
        /// Creates the client
        /// </summary>
        /// <param name="SetFileName">Sets the File Name (or path from excutable with file name) Default = \\ServerOptions.config</param>
        /// <param name="SetDefaultEncoding">Sets the default message encoder to use with message Default = UTF8Encoding.UTF8</param>
        /// <param name="EnableSsl">Are you Using SSL(should be)</param>
        /// <param name="CompleteEvents">Do you want a call back?or 2(prob not as one is more thhan enough)</param>
        public SMTPClient(string SetFileName = null, Encoding SetDefaultEncoding = null, bool EnableSsl = true, SendCompletedEventHandler[] CompleteEvents = null)
        {
            // asume the value of the static settable value for a default
            string temp = DefaultFileName;
            //if we are manualy setting the locationover write the assumed value and if it doent have a \ tho start assume was fprgoten
            if (SetFileName != null)
                temp = SetFileName.First() == '\\' ? SetFileName : "\\" + SetFileName;
            //if default encoding is not null re set it
            if (SetDefaultEncoding != null)
                DefaultEncoding = SetDefaultEncoding;

            //make an options loader and load into it from an xml
            SMTPClientLoadOpt Opts = null;
            using (XmlReader Loader = XmlReader.Create(File.OpenRead(Environment.CurrentDirectory + temp)))
            {
                // Read The File Till we find the node or the end of the file
                while (Loader.Read() && Opts == null)
                {
                    if (Loader.NodeType == XmlNodeType.Element && Loader.Name == "SMTPClientLoadOpt")
                    {
                        Opts = Loader.ReadContentAsSMTPClientLoadOpt();
                    }
                }
            }

            // of we made it to the end of the file with out making a Options thats a problem
            if (Opts == null)
                throw new Exception("SMTPClientLoadOpt node not found before end of file");

            // make a client with all the settings
            Client = new SmtpClient(Opts.Host, Opts.Port.Value);
            Client.Credentials = new NetworkCredential(Opts.Account, Opts.Password);
            Client.EnableSsl = EnableSsl;
            if (CompleteEvents != null)
                foreach(SendCompletedEventHandler SCE in CompleteEvents)
                    Client.SendCompleted += SCE;
        }

        /// <summary>
        /// Sends a message 
        /// </summary>
        /// <param name="To">Who are we sending to</param>
        /// <param name="Subject">what is the subject</param>
        /// <param name="Message">what is the message</param>
        /// <param name="CC">Ad more people muhahaha know to tag them Default = null</param>
        /// <param name="MailPriority">Is this important to get network priority Default = MailPriority.Normal</param>
        /// <param name="IsHTML">Will it use HTML Default = true(of course you eould)</param>
        /// <param name="DeliveryNotificationOptions">Probably better you dont know if it was sent but would you like to Default = DeliveryNotificationOptions.None</param>
        /// <param name="From">From Default = Client.DefaultFrom (null will do this for you)</param>
        /// <param name="MSGEncoding">The Messages encoding Default = Client.MSGEncoding (null will do this for you)</param>
        public void SendMessage(MailAddress[] To, string Subject, string Message, MailAddress[] CC = null, MailPriority MailPriority = MailPriority.Normal, bool IsHTML = true, DeliveryNotificationOptions DeliveryNotificationOptions = DeliveryNotificationOptions.None, MailAddress From = null, Encoding MSGEncoding = null)
        {
            // well compose and send a message
            using (MailMessage MSG = new MailMessage())
            {
                MSG.BodyEncoding = MSGEncoding == null ? DefaultEncoding : MSGEncoding;
                MSG.Priority = MailPriority;
                MSG.IsBodyHtml = IsHTML;
                MSG.Subject = Subject;
                MSG.Body = Message;
                MSG.From = From == null ? DefaultFrom : From;
                MSG.DeliveryNotificationOptions = DeliveryNotificationOptions;

                foreach (MailAddress MA in To)
                    MSG.To.Add(MA);

                if (CC != null)
                    foreach (MailAddress MA in CC)
                        MSG.CC.Add(MA);

                Client.Send(MSG);
            }
        }

        /// <summary>
        /// just allows smaller stuff to get devorced 
        /// </summary>
        public void Dispose()
        {
            Client.Dispose();
        }

    }
}