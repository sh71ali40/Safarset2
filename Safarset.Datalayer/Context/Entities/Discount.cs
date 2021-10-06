using System;
using System.Collections.Generic;

#nullable disable

namespace Safarset.Datalayer.Context.Entities
{
    public partial class Discount
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MerchantId { get; set; }
        public long PreviousPrice { get; set; }
        public long CurrentPrice { get; set; }
        public int Count { get; set; }
        public byte DiscountPercent { get; set; }
        public byte NumOfUserCanUse { get; set; }
        public byte UserGainPoint { get; set; }
        public bool IsPresent { get; set; }
        public string Description { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
