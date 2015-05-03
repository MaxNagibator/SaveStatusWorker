using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using VkNet;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.Attachments;

namespace SaveStatusWorker
{
    public partial class MainForm : Form
    {
        private int _totalRequest;
        private int _successRequest;
        private int _failRequest;
        private List<Post> _gettingWallPosts;

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
            Text = "total:" + _totalRequest + " success:" + _successRequest + " fail:" + _failRequest;
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
            foreach (var fr in frs.OrderBy(f => f.LastName).ThenBy(f => f.FirstName))
            {
                dict.Add(new KeyValuePair<long, string>(fr.Id, fr.LastName + " " + fr.FirstName));
            }
            uiFriendsCombobox.DisplayMember = "Value";
            uiFriendsCombobox.ValueMember = "Key";
            uiFriendsCombobox.DataSource = dict;
        }

        private void uiGetAlbumsButton_Click(object sender, EventArgs e)
        {
            long? selectedUserId = null;
            if (uiFriendsCombobox.SelectedItem != null)
            {
                selectedUserId = ((KeyValuePair<long, string>) uiFriendsCombobox.SelectedItem).Key;
            }
            var albms = VkWorker.GetAlbums(selectedUserId);
            var dict = new List<KeyValuePair<string, string>>();
            dict.Add(new KeyValuePair<string, string>("wall", "фотографии со стены"));
            dict.Add(new KeyValuePair<string, string>("profile", "фотографии профиля"));
            dict.Add(new KeyValuePair<string, string>("saved", "сохраненные фотографии"));
            dict.Add(new KeyValuePair<string, string>("preview", "фотографии из блока над стеной"));
            foreach (var album in albms.OrderBy(f => f.Title))
            {
                dict.Add(new KeyValuePair<string, string>(album.Id.ToString(), album.Title + "(" + album.Size + ")"));
            }
            uiAlbumComboBox.DisplayMember = "Value";
            uiAlbumComboBox.ValueMember = "Key";
            uiAlbumComboBox.DataSource = dict;
        }

        private void uiGetPhotosButton_Click(object sender, EventArgs e)
        {
            long? selectedUserId = null;
            if (uiFriendsCombobox.SelectedItem != null)
            {
                selectedUserId = ((KeyValuePair<long, string>) uiFriendsCombobox.SelectedItem).Key;
            }
            if (uiAlbumComboBox.SelectedItem != null)
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        var albumId = ((KeyValuePair<string, string>) uiAlbumComboBox.SelectedItem).Key;
                        var photos = VkWorker.GetPhotos(selectedUserId, albumId);
                        var webClient = new WebClient();

                        var path = folderDialog.SelectedPath;
                        var userFolderName = GetUserFolderName(selectedUserId, path);
                        var albumFolderPath = GetAlbumFolderPath(albumId, path, userFolderName);
                        for (int index = 0; index < photos.Count; index++)
                        {
                            var photo = photos[index];
                            DownloadImage(webClient, photo, albumFolderPath);
                            Text = "total:" + photos.Count + " current:" + index;
                        }
                    }
                }
            }
        }

        private void uiGetAllAlbumPhotosButton_Click(object sender, EventArgs e)
        {
            long? selectedUserId = null;
            if (uiFriendsCombobox.SelectedItem != null)
            {
                var x = ((KeyValuePair<long, string>) uiFriendsCombobox.SelectedItem);
                selectedUserId = x.Key;
            }
            if (uiAlbumComboBox.SelectedItem != null)
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        var path = folderDialog.SelectedPath;
                        var userFolderName = GetUserFolderName(selectedUserId, path);
                        for (int i = 0; i < uiAlbumComboBox.Items.Count; i++)
                        {
                            var item = uiAlbumComboBox.Items[i];
                            var albumId = ((KeyValuePair<string, string>) item).Key;
                            var photos = VkWorker.GetPhotos(selectedUserId, albumId);

                            var albumFolderPath = GetAlbumFolderPath(albumId, path, userFolderName);
                            var webClient = new WebClient();
                            for (int index = 0; index < photos.Count; index++)
                            {
                                var photo = photos[index];
                                DownloadImage(webClient, photo, albumFolderPath);
                                Text = "totalalbum:" + uiAlbumComboBox.Items.Count + " album:" + i +
                                       " total:" + photos.Count + " current:" + index;
                            }
                        }
                    }
                }
            }
        }

        private static string GetUserFolderName(long? selectedUserId, string path)
        {
            var userFolderName = "user_" + (selectedUserId ?? VkWorker.GetSetttingsUserId());
            var userFolderPath = Path.Combine(path, userFolderName);
            if (!Directory.Exists(userFolderPath))
            {
                Directory.CreateDirectory(userFolderPath);
            }
            return userFolderName;
        }

        private static string GetAlbumFolderPath(string albumId, string path, string userFolderName)
        {
            var albumFolderName = "album_" + albumId;
            var albumFolderPath = Path.Combine(path, userFolderName, albumFolderName);
            if (!Directory.Exists(albumFolderPath))
            {
                Directory.CreateDirectory(albumFolderPath);
            }
            return albumFolderPath;
        }

        private void DownloadImage(WebClient webClient, Photo photo, string path)
        {
            if (photo.Photo2560 != null)
            {
                webClient.DownloadFile(photo.Photo2560, Path.Combine(path, photo.Id + "_2560.jpg"));
            }
            else if (photo.Photo1280 != null)
            {
                webClient.DownloadFile(photo.Photo1280, Path.Combine(path, photo.Id + "_1280.jpg"));
            }
            else if (photo.Photo807 != null)
            {
                webClient.DownloadFile(photo.Photo807, Path.Combine(path, photo.Id + "_807.jpg"));
            }
            else if (photo.Photo604 != null)
            {
                webClient.DownloadFile(photo.Photo604, Path.Combine(path, photo.Id + "_604.jpg"));
            }
            else if (photo.Photo130 != null)
            {
                webClient.DownloadFile(photo.Photo130, Path.Combine(path, photo.Id + "_130.jpg"));
            }
            else if (photo.Photo75 != null)
            {
                webClient.DownloadFile(photo.Photo75, Path.Combine(path, photo.Id + "_75.jpg"));
            }
        }

        private void uiGetGroupContentButton_Click(object sender, EventArgs e)
        {
            var ownerId = Convert.ToInt32(uiFromOwnerIdTextBox.Text);
            var wallPosts = VkWorker.WallGet(ownerId, 10);
            _gettingWallPosts = wallPosts.ToList();
            ConsoleWriteLine("get post count = " + _gettingWallPosts.Count);
        }

        private void ConsoleWriteLine(string text)
        {
            uiOutTextBox.Text += text + Environment.NewLine;
        }

        private void uiPastGroupContentButton_Click(object sender, EventArgs e)
        {
            foreach (var wallPost in _gettingWallPosts)
            {

                var ownerId = Convert.ToInt32(uiToOwnerIdTextBox.Text);
                var mediaAtachments = new List<MediaAttachment>();
                foreach (var a in wallPost.Attachments)
                {
                    mediaAtachments.Add((MediaAttachment) a.Instance);
                }
                var postId = VkWorker.WallPost(ownerId, wallPost.Text, mediaAtachments);
                ConsoleWriteLine(wallPost.OwnerId + "_" + wallPost.Id + "-> " + postId);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void uiNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
            else
            {
                Hide();
                WindowState = FormWindowState.Minimized;
            }
        }
    }

    public class VkWorker
    {
        private static VkApi _vk;

        public static WorkerSettings GetSettings(XDocument xDocument)
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

        public static long WallGetCount(int ownerId)
        {
            var vk = GetVkApi();
            int totalCount;
            var frs = vk.Wall.Get(ownerId, out totalCount, 1);
            return totalCount;
        }

        public static ReadOnlyCollection<Post> WallGet(int ownerId, int count = 100, int offset = 0, WallFilter filter = WallFilter.All)
        {
            var vk = GetVkApi();
            int totalCount;
            var frs = vk.Wall.Get(ownerId,out totalCount,count,offset, filter);
            return frs;
        }

        public static long WallPost(int ownerId, string message,List<MediaAttachment> attachments)
        {
            var vk = GetVkApi();
            var frs = vk.Wall.Post(ownerId, message: message, mediaAttachments: attachments);
            return frs;
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

        public static ReadOnlyCollection<Photo> GetPhotos(long? userId, string albumId)
        {
            var vk = GetVkApi();
            var frs = vk.Photo.Get(userId, albumId);
            return frs;
        }

        private static VkApi GetVkApi()
        {
            if (_vk != null)
            {
                return _vk;
            }

            var settings = GetSettings(XDocument.Load("settings.txt"));
            var vk = new VkApi();
            Settings scope = Settings.All;
            vk.Authorize(settings.AppId, settings.Email, settings.Pass, scope);
            _vk = vk;
            return vk;
        }

        public static long? GetSetttingsUserId()
        {
            var settings = GetSettings(XDocument.Load("settings.txt"));
            return settings.UserId;
        }
    }

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
