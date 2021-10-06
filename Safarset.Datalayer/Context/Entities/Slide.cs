using System;
using System.Collections.Generic;

#nullable disable

namespace Safarset.Datalayer.Context.Entities
{
    public partial class Slide
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public bool IsVisible { get; set; }
    }
}
