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
        public int? Port = null; 

    }

    /// <summary>
    /// just a one time use extiontion for the reader makes later code easier to read is all only used in constructor
    /// </summary>
    internal static class SMTPClientLoadOptLoader
    {
        public static SMTPClientLoadOpt ReadContentAsSMTPClientLoadOpt(this XmlReader Loader)
        {
            SMTPClientLoadOpt Return = null;
            bool InIt = false;

            do
            {
                switch (Loader.NodeType)
                {
                    //look for all the nodes and grab them
                    case XmlNodeType.Element:
                        switch (Loader.Name)
                        {
                            case "SMTPClientLoadOpt":
                                if (Return != null) // to avoid Ambugis throw if we've seen one
                                {
                                    IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                    throw new XmlException("SMTPClientLoadOpt found already. File can not contain mulitple SMTPClientLoadOpt nodes.", new Exception("SMTPClientLoadOpt found already. File can not contain mulitple SMTPClientLoadOpt nodes."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                                }
                                Return = new SMTPClientLoadOpt();
                                InIt = true;
                                break;
                            case "Host":
                                if (InIt)//if we are not in the the node skip : avoid Ambugis
                                {
                                    if (Return.Host != null)
                                    {
                                        IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                        throw new XmlException("Host already found making this line ambiguous.", new Exception("Host already found making this line ambiguous."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                                    }
                                    Return.Host = Loader.ReadElementContentAsString();
                                }
                                break;
                            case "Port":
                                if (InIt)
                                {
                                    if (Return.Port != null)
                                    {
                                        IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                        throw new XmlException("Port already found making this line ambiguous.", new Exception("Port already found making this line ambiguous."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                                    }
                                    Return.Port = Loader.ReadElementContentAsInt();
                                }
                                break;
                            case "DefaultFrom":
                                if (InIt)
                                {
                                    if (Return.DefaultFrom != null)
                                    {
                                        IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                        throw new XmlException("DefaultFrom already found making this line ambiguous.", new Exception("DefaultFrom already found making this line ambiguous."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                                    }
                                    Return.DefaultFrom = new MailAddress(Loader.ReadElementContentAsString());
                                }
                                break;
                            case "Account":
                                if (InIt)
                                {
                                    if (Return.Account != null)
                                    {
                                        IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                        throw new XmlException("Account already found making this line ambiguous.", new Exception("Account already found making this line ambiguous."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                                    }
                                    Return.Account = Loader.ReadElementContentAsString();
                                }
                                break;
                            case "Password":
                                if (InIt)
                                { 
                                    if (Return.Password != null)
                                    {
                                        IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                        throw new XmlException("Password already found making this line ambiguous.", new Exception("Password already found making this line ambiguous."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                                    }
                                    Return.Password = Loader.ReadElementContentAsString();
                                }
                                break;
                            default:
                                if (InIt) // to avoid bad shit kill it if we find a bad node and in it
                                {
                                    IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                    throw new XmlException("Unexpected node found.", new Exception("SMTPClientLoadOpt found already. File can not contain mulitple SMTPClientLoadOpt nodes."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                                }
                                break;
                        }
                        break;
                        //if we find the  end node do some checkes
                    case XmlNodeType.EndElement:
                        if (Loader.Name == "SMTPClientLoadOpt")
                        {
                            //throw if we find the end first.
                            if (Return == null)
                            {
                                IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                throw new XmlException("SMTPClientLoadOpt end found before start", new Exception("SMTPClientLoadOpt found already. File can not contain mulitple SMTPClientLoadOpt nodes."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                            }
                            //Throw on incomplete data
                            else if (Return.Account == null || Return.DefaultFrom == null || Return.Host == null || Return.Password == null || Return.Port == null)
                            {
                                IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                                string returnMSG = "";
                                returnMSG += Return.Account == null ? " Account " : "";
                                returnMSG += Return.DefaultFrom == null ? " DefaultFrom " : "";
                                returnMSG += Return.Host == null ? " Host " : "";
                                returnMSG += Return.Password == null ? " Password " : "";
                                returnMSG += Return.Port == null ? " Port " : "";
                                returnMSG += "nodes were not found in SMTPClientLoadOpt before its end";
                                throw new XmlException(returnMSG, new Exception("SMTPClientLoadOpt found already. File can not contain mulitple SMTPClientLoadOpt nodes."), xmlInfo.LineNumber, xmlInfo.LinePosition);
                            }
                            else if (InIt)
                                InIt = false;
                        }
                        break;
                }
            }
            while (Loader.Read());

            if (!InIt && Return != null)
                return Return;
            {
                IXmlLineInfo xmlInfo = Loader as IXmlLineInfo;
                throw new XmlException("unexpected end config file found before end of SMTPClientLoadOpt", new Exception("SMTPClientLoadOpt found already. File can not contain mulitple SMTPClientLoadOpt nodes."), xmlInfo.LineNumber, xmlInfo.LinePosition);
            }
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
                try
                {
                    Opts = Loader.ReadContentAsSMTPClientLoadOpt();
                }
                catch(XmlException e)
                {
                    Loader.Close();//ensure a file close any way
                    throw e; // and throw it out again
                }
                catch (Exception e)
                {
                    Loader.Close();//ensure a file close any way
                    throw e; // and throw it out again
                }

                Loader.Close();
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
                MAF = From != null ? new MailAddress(From) : DefaultFrom;
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
            //the system expects a list but often its easier than that with a single person for each so add the person to a an array and leverage the array call
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
            //the system expects a list but often its easier to give an array of strings so add the person to a list and check if stings are valid
            List<MailAddress> ToList = new List<MailAddress>();
            List<MailAddress> CCList = new List<MailAddress>();

            //if any thing is bad just throw
            if (To == null)
                throw new ArgumentNullException("To is null");

            if (Subject == null)
                throw new ArgumentNullException("To is null");

            if (Message == null)
                throw new ArgumentNullException("To is null");

            //try building a list and throw if any one is not a valid email the count should give us the index if it fails as the count will stay at the last count
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
            //try building a list and throw if any one is not a valid email the count should give us the index if it fails as the count will stay at the last count
            if (CC != null)
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
            //leverage the earlier vers
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

            // we'll compose and send a message
            using (MailMessage MSG = new MailMessage())
            {
                MSG.BodyEncoding = MSGEncoding ?? DefaultEncoding;
                MSG.Priority = MailPriority;
                MSG.IsBodyHtml = IsHTML;
                MSG.Subject = Subject;
                MSG.Body = Message;
                MSG.From = From ?? DefaultFrom;
                MSG.DeliveryNotificationOptions = DeliveryNotificationOptions;

                //build a list of Mail tos and CC if that is available
                foreach (MailAddress MA in To)
                    MSG.To.Add(MA);

                if (CC != null)
                    foreach (MailAddress MA in CC)
                        MSG.CC.Add(MA);

                //send the message
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