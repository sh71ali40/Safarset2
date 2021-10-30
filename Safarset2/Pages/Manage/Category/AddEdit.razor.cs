using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        //public AddEdit(ICategoryService service, IMapper mapper) : base(service, mapper)
        //{
        //}


        public CategoryDto CategoryDto { get; set; } = new CategoryDto();
        [Parameter] public int? CategoryId { get; set; }
        public string ErrorMessage { get; set; }

        [Inject] public ICategoryService CategoryService { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private LoadingService LoadingService { get; set; }
        [Inject] private ILogger<Category.AddEdit> Logger { get; set; }
        [Inject] public IMapper Mapper { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (CategoryId != null)
            {
                var cat = await CategoryService.FindAsNoTrackingAsync(c => c.Id == CategoryId.Value);
                CategoryDto = Mapper.Map<CategoryDto>(cat);

            }
        }

        private async Task SubmitForm()
        {
            try
            {
                LoadingService.Show();


                var isSucc = CategoryId == null ? await base.Insert(CategoryDto) : await base.Update(CategoryDto, CategoryDto.Id);
                //var isSucc = CategoryId == null? await CategoryService.InsertAsync(cateogory, true): await CategoryService.UpdateAsync(cateogory,true);
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
