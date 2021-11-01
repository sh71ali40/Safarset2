using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Safarset.Service.Component;
using Safarset.ViewModel.Entities;

namespace Safarset2.Pages.Manage.Category
{

    public partial class AddEdit
    {
        public CategoryDto CategoryDto { get; set; } = new CategoryDto();
        [Parameter] public int? CategoryId { get; set; }
        public string ErrorMessage { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private LoadingService LoadingService { get; set; }
        [Inject] private ILogger<Category.AddEdit> Logger { get; set; }
    

        protected override async Task OnInitializedAsync()
        {
            if (CategoryId != null)
            {
                 
                CategoryDto = await base.FindAsync(CategoryId.Value);
            }
        }

        private async Task SubmitForm()
        {
            try
            {
                LoadingService.Show();


                var isSucc = CategoryId == null ? await base.Insert(CategoryDto) : await base.Update(CategoryDto, CategoryDto.Id);
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
