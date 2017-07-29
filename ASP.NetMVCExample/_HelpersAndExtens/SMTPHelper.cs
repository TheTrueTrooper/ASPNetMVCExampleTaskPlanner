#region WritersSigniture
//Writer: Angelo Sanches (BitSan)(Git:TheTrueTrooper)
//Date Writen: June 23,2017
//Project Goal: Make a cloud based app to aid in project management 
//File Goal: To add an encapsolated 255 salt and hash encoder
//Link: https://github.com/TheTrueTrooper/AngelASPExtentions
//Sources: 
//  {
//  Name: NA
//  Writer/Publisher: NA
//  Link: NA
//  }
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
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
            while (Loader.Read())
            {
                switch (Loader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (Loader.Name)
                        {
                            case "Host":
                                Return.Host = Loader.ReadElementContentAsString();
                                break;
                            case "Port":
                                Return.Port = Loader.ReadElementContentAsInt();
                                break;
                            case "DefaultFrom":
                                Return.DefaultFrom = new MailAddress(Loader.ReadElementContentAsString());
                                break;
                            case "Account":
                                Return.Account = Loader.ReadElementContentAsString();
                                break;
                            case "Password":
                                Return.Password = Loader.ReadElementContentAsString();
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (Loader.Name == "SMTPClientLoadOpt")
                        {
                            if (Return.Account == null || Return.DefaultFrom == null || Return.Host == null || Return.Password == null || Return.Port == null)
                            {
                                string returnMSG = "";
                                returnMSG += Return.Account == null ? "Account, " : "";
                                returnMSG += Return.DefaultFrom == null ? "DefaultFrom, " : "";
                                returnMSG += Return.Host == null ? "Host, " : "";
                                returnMSG += Return.Password == null ? "Password, " : "";
                                returnMSG += Return.Port == null ? "Port " : "";
                                returnMSG += "nodes were not found in SMTPClientLoadOpt befor its end";
                                throw new Exception(returnMSG);
                            }
                            else
                                return Return;
                        }
                        break;
                }
            }
            throw new Exception("unexpected end config file found before end of SMTPClientLoadOpt");
        }
    }
    #endregion

    /// <summary>
    /// A class for use when sending emails will send emails 
    /// </summary>
    public class SMTPClient : IDisposable
    {
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
        /// A non-File-loaded ver of the last constructor 
        /// </summary>
        /// <param name="DefaultFromIn">The Name to use in the From space</param>
        /// <param name="Host">the Host To Use</param>
        /// <param name="Account">The account to use</param>
        /// <param name="Password">The password to use</param>
        /// <param name="Port">The Server Port to use</param>
        /// <param name="SetDefaultEncoding">Sets the default message encoder to use with message Default = UTF8Encoding.UTF8</param>
        /// <param name="EnableSsl">Are you Using SSL(should be)</param>
        /// <param name="CompleteEvents">Do you want a call back?or 2(prob not as one is more thhan enough)</param>
        public SMTPClient(string DefaultFromIn, string Host, string Account, string Password, int Port, Encoding SetDefaultEncoding = null, bool EnableSsl = true, SendCompletedEventHandler[] CompleteEvents = null)
        {


            DefaultEncoding = SetDefaultEncoding ?? UTF8Encoding.UTF8;

            DefaultFrom = new MailAddress(DefaultFromIn);

            // make a client with all the settings
            Client = new SmtpClient(Host, Port);
            Client.Credentials = new NetworkCredential(Account, Password);
            Client.EnableSsl = EnableSsl;
            if (CompleteEvents != null)
                foreach (SendCompletedEventHandler SCE in CompleteEvents)
                    Client.SendCompleted += SCE;
        }

        /// <summary>
        /// Creates the client
        /// </summary>
        /// <param name="SetFileName">Sets the File Name (or path from excutable with file name) Default = \\ServerOptions.config</param>
        /// <param name="SetDefaultEncoding">Sets the default message encoder to use with message Default = UTF8Encoding.UTF8</param>
        /// <param name="EnableSsl">Are you Using SSL(should be)</param>
        /// <param name="CompleteEvents">Do you want a call back?or 2(prob not as one is more thhan enough)</param>
        public SMTPClient(string SetFileName, Encoding SetDefaultEncoding = null, bool EnableSsl = true, SendCompletedEventHandler[] CompleteEvents = null)
        {

            //if default encoding is not null re set it
            DefaultEncoding = SetDefaultEncoding ?? UTF8Encoding.UTF8;

            //make an options loader and load into it from an xml
            SMTPClientLoadOpt Opts = null;
            using (XmlReader Loader = XmlReader.Create(File.OpenRead(SetFileName)))
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

            DefaultFrom = Opts.DefaultFrom;

            // make a client with all the settings
            Client = new SmtpClient(Opts.Host, Opts.Port.Value);
            Client.Credentials = new NetworkCredential(Opts.Account, Opts.Password);
            Client.EnableSsl = EnableSsl;
            if (CompleteEvents != null)
                foreach(SendCompletedEventHandler SCE in CompleteEvents)
                    Client.SendCompleted += SCE;
        }

        /// <summary>
        /// Sends a message Va Client
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
        public void SendMessageQuick(string To, string Subject, string Message, string[] CC = null, MailPriority MailPriority = MailPriority.Normal, bool IsHTML = true, DeliveryNotificationOptions DeliveryNotificationOptions = DeliveryNotificationOptions.None, string From = null, Encoding MSGEncoding = null)
        {
            MailAddress MAF;
            try
            {
                MAF = new MailAddress(From);
            }
            catch (Exception e)
            {
                throw new Exception(From + " was not a valid Email", e);
            }

            SendMessage(new string[] { To }, Subject, Message, CC, MailPriority, IsHTML, DeliveryNotificationOptions, MAF, MSGEncoding);
        }

        /// <summary>
        /// Sends a message Va Client
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
        public void SendMessage(string To, string Subject, string Message, string[] CC = null, MailPriority MailPriority = MailPriority.Normal, bool IsHTML = true, DeliveryNotificationOptions DeliveryNotificationOptions = DeliveryNotificationOptions.None, MailAddress From = null, Encoding MSGEncoding = null)
        {
            SendMessage(new string[] { To }, Subject, Message, CC, MailPriority, IsHTML, DeliveryNotificationOptions, From, MSGEncoding);
        }

        /// <summary>
        /// Sends a message Va Client
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
        public void SendMessage(string[] To, string Subject, string Message, string[] CC = null, MailPriority MailPriority = MailPriority.Normal, bool IsHTML = true, DeliveryNotificationOptions DeliveryNotificationOptions = DeliveryNotificationOptions.None, MailAddress From = null, Encoding MSGEncoding = null)
        {
            List<MailAddress> ToList = new List<MailAddress>();
            List<MailAddress> CCList = new List<MailAddress>();

            if (To == null)
                throw new ArgumentNullException("To is null");

            if (Subject == null)
                throw new ArgumentNullException("To is null");

            if (Message == null)
                throw new ArgumentNullException("To is null");

            foreach (string s in To)
            {
                try
                {
                    ToList.Add(new MailAddress(s));
                }
                catch (Exception e)
                {
                    throw new Exception(s + " was not a valid Email in To emails on index " + ToList.Count, e);
                }
            }

            if(CC != null)
            {

                foreach (string s in CC)
                {
                    try
                    {
                        CCList.Add(new MailAddress(s));
                    }
                    catch (Exception e)
                    {
                        throw new Exception(s + " was not a valid Email in CC emails on index " + ToList.Count, e);
                    }
                }
            }

            SendMessage(ToList.ToArray(), Subject, Message, CCList.ToArray(), MailPriority, IsHTML, DeliveryNotificationOptions, From, MSGEncoding);
        }

        /// <summary>
        /// Sends a message Va Client
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
            if (To == null)
                throw new ArgumentNullException("To is null");

            if (Subject == null)
                throw new ArgumentNullException("To is null");

            if (Message == null)
                throw new ArgumentNullException("To is null");

            // well compose and send a message
            using (MailMessage MSG = new MailMessage())
            {
                MSG.BodyEncoding = MSGEncoding ?? DefaultEncoding;
                MSG.Priority = MailPriority;
                MSG.IsBodyHtml = IsHTML;
                MSG.Subject = Subject;
                MSG.Body = Message;
                MSG.From = From ?? DefaultFrom;
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