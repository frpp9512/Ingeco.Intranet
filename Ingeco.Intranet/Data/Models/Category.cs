using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Models
{
    /// <summary>
    /// Represents a category that englobes a set of posts.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// The primary key identifier.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the category. It explains the kind of posts that englobe.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The posts defined in the category.
        /// </summary>
        public IEnumerable<Post> Posts { get; set; }
    }
}
