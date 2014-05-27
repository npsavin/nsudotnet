using System;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Xml;


namespace NsavinRSS
{
    internal class Program
    {
        private static System.Timers.Timer aTimer;

        private static void Main(string[] args)
        {
            var autoEvent = new AutoResetEvent(false);
            TimerCallback tcb = LoadFeeds;
            var timer = new Timer(tcb, autoEvent, 0, 5000);
            while (true)
            {
                autoEvent.WaitOne(5000, false);
            }
            
            
        }


        private static void Send(SyndicationItem item)
        {
            var smtp = new SmtpClient("smtp.rambler.ru", 587) {Credentials = new NetworkCredential("npsavin", "")};

            var message = new MailMessage {From = new MailAddress("npsavin@rambler.ru")};
            message.To.Add(new MailAddress("fsafsa@lackhite.com"));
            message.Subject = item.Title.Text;
            message.Body = item.Summary.Text;
            message.IsBodyHtml = true;
            Console.Write(message.Body + "\n");

            smtp.Send(message);
        }

        private static void LoadFeeds(Object o)
        {
            const string tbUrl = "http://bash.im/rss";
            var feedReader = XmlReader.Create(tbUrl);
            var channel = SyndicationFeed.Load(feedReader);
            
            if (channel != null)
            {
                foreach (var item in channel.Items)
                {
                    Send(item);
                }
            }



        }
    }
}
