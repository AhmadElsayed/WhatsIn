using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WhatsIn.Models;

namespace WhatsIn.General
{
    public class MessagesList
    {
        private string[] _data { get; }

        public MessagesList(string[] data)
        {
            _data = data;
        }

        public AnalyticsModel Process()
        {
            AnalyticsModel model = new AnalyticsModel();
            foreach (var item in _data)
            {
                Message message = new Message(item);
                if (message.IsMessageValid)
                {
                    if (message.IsNewMessage)
                    {
                        model.DayofWeekActivity[message.Date.DayOfWeek].Count++;
                        if (!model.UsersMedia.ContainsKey(message.Sender))
                        {
                            model.HoursActivity.Add(message.Sender, new int[24]);
                            //for (int i = 0; i < 24; i++)
                            //    model.HoursActivity[message.Sender].Add(i, 0);
                            model.UsersMedia.Add(message.Sender, 0);
                            model.UsersMessages.Add(message.Sender, 0);
                            model.UserWords.Add(message.Sender, new Dictionary<string, int>());
                        }
                        model.HoursActivity[message.Sender][message.Hour]++;
                        if (message.IsMedia)
                            model.UsersMedia[message.Sender]++;
                        model.UsersMessages[message.Sender]++;
                        if (!model.DateCounts.ContainsKey(message.Date))
                        {
                            model.DateCounts.Add(message.Date, 0);
                        }
                        model.DateCounts[message.Date]++;
                        model.DayOfWeekHours[message.Date.DayOfWeek][message.Hour]++;
                    }

                    foreach (var word in message.Content)
                    {
                        if (!model.UserWords[message.Sender].ContainsKey(word.Key))
                        {
                            model.UserWords[message.Sender].Add(word.Key, 0);
                        }
                        model.UserWords[message.Sender][word.Key] += word.Value;
                    }
                }
            }
            foreach (var item in model.UsersMedia)
            {
                model.UserWords[item.Key] = model.UserWords[item.Key].Where(x => x.Value > 2).OrderByDescending(x => x.Value).Take(10).ToDictionary(x => x.Key, x => x.Value);
            }
            //var sss = string.Join(",", model.UserWords["Assem"].OrderByDescending(x => x.Value).Take(50).Select(x => $"\"{x.Key}\"") );
            return model;
        }
    }
}
