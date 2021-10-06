using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Safarset.Service.Services;
using Safarset.ViewModel.Entities;
using Telerik.Blazor.Components;

namespace Safarset2.Pages.Manage.Category
{
    public partial class Index
    {
        [Inject] public ICategoryService CategoryService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var categories = await CategoryService.GetAllAsNoTrackingAsync();
 
        }
      
    }
}
