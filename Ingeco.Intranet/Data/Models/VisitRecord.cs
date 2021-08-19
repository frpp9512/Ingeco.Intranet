using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Models
{
    /// <summary>
    /// Represents a visit performed to a post by a client.
    /// </summary>
    public class VisitRecord
    {
        /// <summary>
        /// The primary key identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// When the visit happend.
        /// </summary>
        public DateTimeOffset Visited { get; set; }

        /// <summary>
        /// The address of the client that makes the visit.
        /// </summary>
        public string HostClient { get; set; }

        /// <summary>
        /// The identifier of the post that recieve the visit.
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// The post that recieve the visit.
        /// </summary>
        public Post Post { get; set; }
    }
}