using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using VkNet;
using VkNet.Enums;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.Attachments;

namespace SaveStatusWorker
{
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
            var frs = vk.Wall.Get(ownerId, out totalCount, count, offset, filter);
            return frs;
        }

        public static long WallPost(int ownerId, string message, List<MediaAttachment> attachments)
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
}
