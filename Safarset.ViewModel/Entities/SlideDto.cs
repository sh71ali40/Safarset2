using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safarset.ViewModel.Entities
{
    public class SlideDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        
        [DefaultValue(true)]
        public bool IsVisible { get; set; }
    }
}
