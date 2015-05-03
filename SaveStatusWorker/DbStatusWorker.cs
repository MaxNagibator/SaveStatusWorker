using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VkNet.Model;

namespace SaveStatusWorker
{
    public class DbStatusWorker
    {
        public static void SaveStatuses(ReadOnlyCollection<User> frs)
        {
            using (var context = new DbElements.VkEntities())
            {
                DateTime date = DateTime.Now;
                foreach (var fr in frs)
                {
                    var us = context.User.FirstOrDefault(u => u.Id == fr.Id);
                    if (us == null)
                    {
                        context.User.Add(new DbElements.User
                        {
                            Id = fr.Id,
                            LastName = fr.LastName,
                            FirstName = fr.FirstName
                        });
                    }
                    var lastStatus =
                        context.Status.Where(s => s.UserId == fr.Id).OrderByDescending(s => s.Date).FirstOrDefault();
                    if (lastStatus == null || lastStatus.Text != fr.Status)
                    {
                        var status = new DbElements.Status();
                        status.Text = fr.Status;
                        status.UserId = fr.Id;
                        status.Date = date;
                        context.Status.Add(status);
                    }
                }
                context.SaveChanges();
            }
        }

        public static List<string> GetStatuses()
        {
            using (var context = new DbElements.VkEntities())
            {
                return context.Status
                    .OrderByDescending(s => s.Date)
                    .Select(s => s.Text).Take(10).ToList();
            }
        }

    }
}
