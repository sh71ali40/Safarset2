using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Safarset.Datalayer.Context.Entities;
using Safarset.Service.Component;
using Safarset.Service.Services;
using Safarset.ViewModel.Entities;
using Telerik.Blazor.Components;
using Safarset.ViewModel.Entities;

namespace Safarset2.Pages.Manage.Category
{
    public partial class Index
    {
        public ObservableCollection<CategoryDto> Categorys { get; set; }
        [Inject] private IMapper Mapper { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        [Inject] public ICategoryService CategoryService { get; set; }
        [Inject] public ConfirmService ConfirmService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var categories = CategoryService.GetAllAsQueryable().OrderByDescending(s => s.Id).AsEnumerable();
            var mappedCategorys = Mapper.Map<IEnumerable<Safarset.Datalayer.Context.Entities.Category>, IEnumerable<CategoryDto>>(categories);
            Categorys = new ObservableCollection<CategoryDto>(mappedCategorys);
            //  var categories = await CategoryService.GetAllAsNoTrackingAsync();

        }
        private async void RemoveCategory(CategoryDto categoryDto)
        {
            var result = await base.Delete(categoryDto.Id);


            if (result)
            {
                Categorys.Remove(categoryDto);
                ToastService.ShowToast("حذف با موفقیت انجام شد", ToastLevel.Success);
            }
            else
            {
                ToastService.ShowToast("حذف ناموفق بود", ToastLevel.Error);
            }
        }
        private async Task OnRemoveClick(GridCommandEventArgs e)
        {
            var category = (CategoryDto)e.Item;
            ConfirmService.Show("حذف", "آیا از حذف مطمئن هستید؟", () => RemoveCategory(category), null);
        }
        private async Task OnEditClick(GridCommandEventArgs e)
        {
            var category = (CategoryDto)e.Item;
            NavigationManager.NavigateTo("Manage/Category/AddEdit/"+category.Id);
        }

    }
}
