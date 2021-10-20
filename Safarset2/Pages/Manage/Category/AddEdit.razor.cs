using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Safarset.Datalayer.Context.Entities;
using Safarset.Service.Component;
using Safarset.Service.Services;
using Safarset.ViewModel.Entities;
using Safarset2.Class;

namespace Safarset2.Pages.Manage.Category
{
    
    public partial class AddEdit
    {
        public CategoryDto CategoryDto { get; set; } = new CategoryDto();
        [Parameter] public int CategoryId { get; set; }
        public string ErrorMessage { get; set; }
        public string Name { get; set; }
        [Inject] public ICategoryService CategoryService { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private LoadingService LoadingService { get; set; }
        [Inject] private ILogger<Category.AddEdit> Logger { get; set; }
        [Inject] public IMapper Mapper { get; set; }
        private async Task SubmitForm()
        {
            try
            {
                LoadingService.Show();
                var cateogory = Mapper.Map<CategoryDto, Safarset.Datalayer.Context.Entities.Category>(CategoryDto);
                cateogory.Name = Name;
                var isSucc = await CategoryService.InsertAsync(cateogory, true);
                if (isSucc)
                {
                    ToastService.ShowToast("ثبت با موفقیت انجام شد", ToastLevel.Success);
                    NavigationManager.NavigateTo("Manage/Category");
                }
                else
                {
                    throw new Exception();
                }
                LoadingService.Hide();
            }
            catch (Exception e)
             {
                ToastService.ShowToast("ثبت ناموفق بود لطفا مجدد امتحان نمایید", ToastLevel.Error);
                Logger.LogError(e, e.Message);
                LoadingService.Hide();
            }
        }

    }
}
