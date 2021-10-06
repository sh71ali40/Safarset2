using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Safarset2.Pages.Manage.Category
{
    
    public partial class AddEdit
    {
        [Parameter] public int CategoryId { get; set; }
    }
}
