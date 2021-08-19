using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Models
{
    /// <summary>
    /// Represents a comment performed to a post.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// The primary key identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The date and time when the comment was created.
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// The comment text body.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Defines if the comment is a reply.
        /// </summary>
        public bool IsReply { get; set; }

        /// <summary>
        /// The identifier of the comment that is replied.
        /// </summary>
        public Guid RepliedToId { get; set; }

        /// <summary>
        /// The comment replied.
        /// </summary>
        public Comment RepliedTo { get; set; }

        /// <summary>
        /// The identifier of the post which the post belongs to.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// The post which the post belongs to.
        /// </summary>
        public Post Post { get; set; }

        /// <summary>
        /// The identifier of the user that makes the comment.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The user that makes the comment.
        /// </summary>
        public User User { get; set; }
    }
}