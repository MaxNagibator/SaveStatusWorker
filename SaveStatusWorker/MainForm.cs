using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace SaveStatusWorker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private static WorkerSettings GetSettings(XDocument xDocument)
        {
            var setting = new WorkerSettings();
            if (xDocument.Root == null)
            {
                return setting;
            }
            var appId = xDocument.DescendantNodes().OfType<XElement>().First(x => x.Name == "AppId");
            setting.AppId = Convert.ToInt32(appId.Value);
            var email = xDocument.DescendantNodes().OfType<XElement>().First(x => x.Name == "email");
            if (!String.IsNullOrEmpty(email.Value))
            {
                setting.Email = email.Value;
            }
            var pass = xDocument.DescendantNodes().OfType<XElement>().First(x => x.Name == "pass");
            if (!String.IsNullOrEmpty(pass.Value))
            {
                setting.Pass = pass.Value;
            }
            var token = xDocument.DescendantNodes().OfType<XElement>().First(x => x.Name == "token");
            if (!String.IsNullOrEmpty(token.Value))
            {
                setting.Token = token.Value;
            }
            var userId = xDocument.DescendantNodes().OfType<XElement>().First(x => x.Name == "userId");
            if (!String.IsNullOrEmpty(userId.Value))
            {
                setting.UserId = Convert.ToInt32(userId.Value);
            }
            return setting;
        }

        private void uiStartButton_Click(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(textBox1.Text);
            timer1.Start();
            
        }

        private void uiStopButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetValue();
        }

        private void GetValue()
        {
            Text += "!";
            var settings = GetSettings(XDocument.Load("settings.txt"));

            var vk = new VkApi();
            Settings scope = Settings.All; // Права доступа приложения
            vk.Authorize(settings.AppId, settings.Email, settings.Pass, scope);
            var x = ProfileFields.Status | ProfileFields.LastName | ProfileFields.FirstName;
            var frs = vk.Friends.Get((long)vk.UserId, x);
            Class1.SaveStatuses(frs);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetValue();
            //Text += "!";
            //var settings = GetSettings(XDocument.Load("settings.txt"));

            //var vk = new VkApi();
            //Settings scope = Settings.All; // Права доступа приложения
            //vk.Authorize(settings.AppId, settings.Email, settings.Pass, scope);
            //var x = ProfileFields.Status | ProfileFields.LastName | ProfileFields.FirstName;
            //var frs = vk.Friends.Get((long)vk.UserId, x);
            //var x3 = Class1.SaveStatuses(frs);
            var x3 = Class1.GetStatuses();
            foreach (var text in x3)
            {
                textBox2.Text += text + Environment.NewLine;
            }
        }
    }

    internal class Class1
    {
        public static void SaveStatuses(ReadOnlyCollection<User> frs)
        {
            using (var context = new DbElements.Entities1())
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
            using (var context = new DbElements.Entities1())
             {
                 return context.Status.Select(s => s.Text).Take(10).ToList();
             }
        }
    }
}
