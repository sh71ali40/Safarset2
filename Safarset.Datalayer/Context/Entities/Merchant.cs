using System;
using System.Collections.Generic;

#nullable disable

namespace Safarset.Datalayer.Context.Entities
{
    public partial class Merchant
    {
        public Merchant()
        {
            Discounts = new HashSet<Discount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public string Email { get; set; }
        public string PhoneNum1 { get; set; }
        public string PhoneNum2 { get; set; }
        public int DisplayOrder { get; set; }
        public long FromPrice { get; set; }
        public long ToPrice { get; set; }
        public bool ShowInApp { get; set; }
        public string LogoName { get; set; }
        public string MainImageName { get; set; }
        public string ProvideGuide { get; set; }
        public string Longitiude { get; set; }
        public string Latitude { get; set; }

        public virtual Category Category { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
    }
}
