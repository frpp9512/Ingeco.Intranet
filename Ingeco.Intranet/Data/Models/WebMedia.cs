using System;

namespace Ingeco.Intranet.Data.Models
{
    /// <summary>
    /// Represents a media used in a post of the website.
    /// </summary>
    public class WebMedia
    {
        /// <summary>
        /// The primary key identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The id used for view reference.
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// The type of the web media.
        /// </summary>
        public WebMediaType MediaType { get; set; }

        /// <summary>
        /// The description of the media, it should be usedd as alt text.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Defines if the media will be showed as cover of the post.
        /// </summary>
        public bool IsCover { get; set; }

        /// <summary>
        /// The identifier of the post owner of this media.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// The post where the media will be showed in.
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        public string Filename { get; set; }
    }

    /// <summary>
    /// Represents the type of a <see cref="WebMedia"/>.
    /// </summary>
    public enum WebMediaType
    {
        /// <summary>
        /// A picture used in a post.
        /// </summary>
        Picture = 0,
        /// <summary>
        /// A video used in a post.
        /// </summary>
        Video = 1
    }
}