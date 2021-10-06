using System;
using System.Collections.Generic;

#nullable disable

namespace Safarset.Datalayer.Context.Entities
{
    public partial class Category
    {
        public Category()
        {
            Merchants = new HashSet<Merchant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Merchant> Merchants { get; set; }
    }
}
