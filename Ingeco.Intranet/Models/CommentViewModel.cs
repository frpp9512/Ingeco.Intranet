using Ingeco.Intranet.Data.Models;
using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset Created { get; set; }

        public string Text { get; set; }

        public bool IsReply { get; set; }

        public Guid? RepliedToId { get; set; }

        public CommentViewModel RepliedTo { get; set; }

        public int TotalRepliesCount { get; set; }

        public bool HasReplies => TotalRepliesCount > 0;

        public IEnumerable<CommentViewModel> Replies { get; set; }

        public Guid PostId { get; set; }

        public Post Post { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}