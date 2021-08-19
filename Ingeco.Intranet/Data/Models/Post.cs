using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ingeco.Intranet.Data.Models
{
    /// <summary>
    /// A post made in the website.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// The primary key identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the posts.
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// The description of the post content.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The date and time when the post was created.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// The content body of the post.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// All the media used in the post.
        /// </summary>
        public IEnumerable<WebMedia> Media { get; set; }

        /// <summary>
        /// The media used in the cover of the post.
        /// </summary>
        public WebMedia Cover => Media.FirstOrDefault(m => m.IsCover);

        /// <summary>
        /// The tags of the post content separated by a semicolon (;).
        /// </summary>
        public string TagsLine { get; set; }

        /// <summary>
        /// The identifier of the user that created the post.
        /// </summary>
        public Guid PostedById { get; set; }

        /// <summary>
        /// The user that made the post.
        /// </summary>
        public User PostedBy { get; set; }

        /// <summary>
        /// Defines if the post could be accessed by all users.
        /// </summary>
        public bool Public { get; set; }
        
        /// <summary>
        /// The identifier of the category which the post belongs to.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// The category which the post belongs to.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// The comments of the posts.
        /// </summary>
        public IEnumerable<Comment> Comments { get; set; }

        /// <summary>
        /// The visits that have recivied the post.
        /// </summary>
        public IEnumerable<VisitRecord> Visits { get; set; }

        /// <summary>
        /// Get the array of all the tags of the 
        /// </summary>
        /// <returns>An array with the tags of the post.</returns>
        public string[] GetTagArray() => TagsLine?.Split(";");

        /// <summary>
        /// Add a new tag to the post.
        /// </summary>
        /// <param name="tag">A new tag to add to the post.</param>
        public void AddTag(string tag) 
            => TagsLine = string.IsNullOrEmpty(TagsLine) && !TagsLine.Contains(tag) ? tag : $"{TagsLine};{tag}";

        /// <summary>
        /// Removes a tag from the post.
        /// </summary>
        /// <param name="tag">The tag to remove from the post.</param>
        public void RemoveTag(string tag)
        {
            if (!string.IsNullOrEmpty(TagsLine) && TagsLine.Contains(tag))
            {
                var regex = new Regex($"{tag};*");
                var match = regex.Match(TagsLine);
                if (match.Success)
                {
                    TagsLine = regex.Replace(TagsLine, "");
                }
            }
        }

        /// <summary>
        /// Determines if the post have the given tag.
        /// </summary>
        /// <param name="tag">The tag to determine if is associated with the post.</param>
        /// <returns><see langword="true"/> if the tag is associated with the post.</returns>
        public bool HaveTag(string tag) => !string.IsNullOrEmpty(TagsLine) && TagsLine.Contains(tag);
    }
}