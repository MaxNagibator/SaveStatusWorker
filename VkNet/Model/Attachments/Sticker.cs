using System;
using System.Collections.ObjectModel;
using VkNet.Utils;

namespace VkNet.Model.Attachments
{
	/// <summary>
    /// Стикер.
    /// </summary>
    public class Sticker : MediaAttachment
    {
        static Sticker()
      	{
            RegisterType(typeof(Sticker), "sticker");
      	}

        /// <summary>
        /// Url фотографии с максимальным размером 64x64px.
        /// </summary>
        public Uri Photo64 { get; set; }

        /// <summary>
        /// Url фотографии с максимальным размером 128x128px. 
        /// </summary>
        public Uri Photo128 { get; set; }

        /// <summary>
        /// Url фотографии с максимальным размером  256x256px.
        /// </summary>
        public Uri Photo256 { get; set; }

        /// <summary>
        /// Ширина оригинала фотографии в пикселах
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Высота оригинала фотографии в пикселах. 
        /// </summary>
        public int? Height { get; set; }

        #region Методы

        internal static Sticker FromJson(VkResponse response)
        {
            var poll = new Sticker();
            poll.Id = response["id"];
            poll.Photo64 = response["photo_64"];
            poll.Photo128 = response["photo_128"];
            poll.Photo256 = response["photo_256"];
            poll.Width = response["width"];
            poll.Height = response["height"];
            return poll;
        }

        #endregion
    }
}