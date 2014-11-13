using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace SaveStatusWorker
{
    public partial class MainForm : Form
    {
        private int _totalRequest;
        private int _successRequest;
        private int _failRequest;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _totalRequest = 0;
            _successRequest = 0;
            _failRequest = 0;
        }

        private void uiStartButton_Click(object sender, EventArgs e)
        {
            GetStatuses();
            timer1.Interval = Convert.ToInt32(uiTimerIntervalTextBox.Text)*1000;
            timer1.Start();
        }

        private void uiStopButton_Click(object sender, EventArgs e)
        {
            uiMessageLabel.Text = "stoped";
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetStatuses();
        }

        private void GetStatuses()
        {
            _totalRequest++;
            try
            {
                var frs = VkWorker.GetFriends();
                DbStatusWorker.SaveStatuses(frs);
                _successRequest++;
            }
            catch (Exception)
            {
                _failRequest++;
            }
            uiMessageLabel.Text = "total:" + _totalRequest + " success:" + _successRequest + " fail:" + _failRequest;
        }


        private void uiShowStatusesButton_Click(object sender, EventArgs e)
        {
            var statuses = DbStatusWorker.GetStatuses();
            foreach (var text in statuses)
            {
                uiOutTextBox.Text += text + Environment.NewLine;
            }
        }

        private void uiGetFriendsButton_Click(object sender, EventArgs e)
        {
            var frs = VkWorker.GetFriends();
            var dict = new List<KeyValuePair<long, string>>();
            foreach (var fr in frs.OrderBy(f=>f.LastName).ThenBy(f=>f.FirstName))
            {
                dict.Add(new KeyValuePair<long, string>(fr.Id, fr.LastName + " " + fr.FirstName));
            }
            uiFriendsCombobox.DisplayMember = "Value";
            uiFriendsCombobox.ValueMember = "Key";
            uiFriendsCombobox.DataSource = dict;
        }

        private void uiGetAlbumsButton_Click(object sender, EventArgs e)
        {
            long? selectedId = null;
            if (uiFriendsCombobox.SelectedItem != null)
            {
                selectedId = ((User) uiFriendsCombobox.SelectedItem).Id;
            }
                var albms = VkWorker.GetAlbums(selectedId);
            var dict = new List<KeyValuePair<long, string>>();
            foreach (var album in albms.OrderBy(f => f.Title))
            {
                dict.Add(new KeyValuePair<long, string>(album.Id, album.Title + "(" + album.Size+")"));
            }
            uiFriendsCombobox.DisplayMember = "Value";
            uiFriendsCombobox.ValueMember = "Key";
            uiFriendsCombobox.DataSource = dict; 
           
        }
    }
    public class VkWorker
    {
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

        public static ReadOnlyCollection<User> GetFriends()
        {
            var vk = GetVkApi();
            var x = ProfileFields.Status | ProfileFields.LastName | ProfileFields.FirstName;
            var frs = vk.Friends.Get((long)vk.UserId, x);
            return frs;
        }

        public static ReadOnlyCollection<PhotoAlbum> GetAlbums(long? userId)
        {
            var vk = GetVkApi();
            var frs = vk.Photo.GetAlbums(userId);
            return frs;
        }

        private static VkApi GetVkApi()
        {
            var settings = GetSettings(XDocument.Load("settings.txt"));
            var vk = new VkApi();
            Settings scope = Settings.All;
            vk.Authorize(settings.AppId, settings.Email, settings.Pass, scope);
            return vk;
        }
    }

    public class DbStatusWorker
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
                return context.Status
                    .OrderByDescending(s => s.Date)
                    .Select(s => s.Text).Take(10).ToList();
            }
        }
    }
}
