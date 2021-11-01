using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Safarset.Service.Component;
using Safarset.ViewModel.Entities;

namespace Safarset2.Pages.Manage.City
{
    public partial class AddEdit
    {
        public string ErrorMessage { get; set; }
        public CityDto CityDto{ get; set; } = new CityDto();
        [Parameter] public int? CityId { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private LoadingService LoadingService { get; set; }
        [Inject] private ILogger<City.AddEdit> Logger { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (CityId != null)
            {
                CityDto = await base.FindAsync(CityId.Value);
            }
        }

        private async Task SubmitForm()
        {
            try
            {
                LoadingService.Show();

                var isSucc = CityId == null ? await base.Insert(CityDto) : await base.Update(CityDto, CityDto.Id);
                if (isSucc)
                {
                    ToastService.ShowToast("ثبت با موفقیت انجام شد", ToastLevel.Success);
                    NavigationManager.NavigateTo("Manage/City");
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
