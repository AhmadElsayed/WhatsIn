using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WhatsIn.General
{
    public class Message
    {
        public DateTime Date { set; get; }

        public string Sender { set; get; }

        public int Hour { set; get; }

        public Dictionary<string, int> Content { set; get; }

        public bool IsMedia { set; get; }

        public bool IsNewMessage { set; get; }

        public bool IsMessageValid { set; get; }

        private string Msg { set; get; }

        private static string LastUser { set; get; }

        public Message(string msg)
        {
            Content = new Dictionary<string, int>();
            Msg = msg;
            Handle();
        }

        private void CountWords(string body)
        {
            var words = body.Split(" ").Where(x => !Common.ExcludedKeyWords.Contains(x));
            foreach (var item in words)
            {
                if (!string.IsNullOrEmpty(item))
                    if (!Content.ContainsKey(item))
                    {
                        Content.Add(item, 1);
                    }
                    else
                    {
                        Content[item]++;
                    }
            }
        }

        private void Handle()
        {
            IsMessageValid = true;
            string body;

            if (Msg.Contains(":") && Msg.Contains(",") && Msg.Contains("-") && int.TryParse(Msg[0].ToString(), out int n)) // This is a new Message, Not a new line at the previous
            {
                IsNewMessage = true;
                var splittedMessage = Msg.Split("-");
                var splittedMessage1 = splittedMessage[0].Split(",");
                Date = DateTime.ParseExact(splittedMessage1[0], "M/d/yy"/*"dd/MM/yyyy"*/, CultureInfo.InvariantCulture); // Convert.ToDateTime(splittedMessage1[0], "dd/MM/yyyy");
                Hour = Convert.ToInt32(splittedMessage1[1].Split(":")[0].Trim());
                var splittedMessage2 = splittedMessage[1].Split(":");
                if (splittedMessage2[0].Trim().Contains("added") || splittedMessage2[0].Trim().Contains("left") || splittedMessage2[0].Trim().Contains("are now secured") || splittedMessage2[0].Trim().Contains("changed the subject") || splittedMessage2[0].Trim().Contains("changed this group") || splittedMessage2[0].Trim().Contains("removed"))
                    IsMessageValid = false;

                Sender = splittedMessage2[0].Trim();
                body = string.Join(" ", splittedMessage2.Skip(1));
                Message.LastUser = Sender;
            }
            else
            {
                Sender = Message.LastUser;
                body = Msg;
            }
            if (body.Contains("Mediaomitted"))
            {
                IsMedia = true;
                IsMessageValid = false;
            }
            else
            {
                CountWords(body);

            }
        }
    }
}
