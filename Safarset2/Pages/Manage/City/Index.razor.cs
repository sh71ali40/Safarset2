using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Safarset.Service.Component;
using Safarset.ViewModel.Entities;
using Telerik.Blazor.Components;

namespace Safarset2.Pages.Manage.City
{
    public partial class Index
    {
        public ObservableCollection<CityDto> Cities { get; set; }

        [Inject] public ToastService ToastService { get; set; }
        [Inject] public ConfirmService ConfirmService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var cities = base.GetAll().OrderByDescending(c => c.Id);
            Cities = new ObservableCollection<CityDto>(cities);
            

        }
        private async void RemoveCity(CityDto cityDto)
        {
            var result = await base.Delete(cityDto.Id);


            if (result)
            {
                Cities.Remove(cityDto);
                ToastService.ShowToast("حذف با موفقیت انجام شد", ToastLevel.Success);
            }
            else
            {
                ToastService.ShowToast("حذف ناموفق بود", ToastLevel.Error);
            }
        }
        private async Task OnRemoveClick(GridCommandEventArgs e)
        {
            var city = (CityDto)e.Item;
            ConfirmService.Show("حذف", "آیا از حذف مطمئن هستید؟", () => RemoveCity(city), null);
        }
    }
}
