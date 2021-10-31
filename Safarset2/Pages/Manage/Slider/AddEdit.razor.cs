using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Safarset.Datalayer.Context.Entities;
using Safarset.Service.Component;
using Safarset.Service.Services;
using Safarset.ViewModel.Entities;
using Safarset2.Class;

namespace Safarset2.Pages.Manage.Slider
{
    public partial class AddEdit
    {
        [Parameter] public int? SliderId { get; set; }
        public SlideDto SlideDto { get; set; } = new SlideDto();
        [Inject] public IWebHostEnvironment Environment { get; set; }
        
        [Inject] public ToastService ToastService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ILogger<AddEdit> Logger { get; set; }
        [Inject] private LoadingService LoadingService { get; set; }
        public string ErrorMessage { get; set; }
        public IBrowserFile InputImage { get; set; }
        private async Task SubmitForm()
        {
            try
            {
                if (InputImage.ContentType != "image/jpeg")
                {
                    ErrorMessage = "فایل بارگذاری شده باید jpg باشد";
                    return;
                }

                LoadingService.Show();

                var imageName = Guid.NewGuid() + ".jpg";
                //var slide = Mapper.Map<SlideDto, Slide>(SlideDto);

                SlideDto.ImageName = imageName;
                var isSucc = await base.Insert(SlideDto);
                if (isSucc)
                {
                    var path = Path.Combine(Environment.ContentRootPath, "wwwroot\\" + 
                    Variable.UploadAddress,
                    imageName);
                    await using FileStream fs = new(path, FileMode.Create);
                    await InputImage.OpenReadStream().CopyToAsync(fs);

                    ToastService.ShowToast("ثبت با موفقیت انجام شد", ToastLevel.Success);

                    NavigationManager.NavigateTo("Manage/Slider");
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

        private void LoadFiles(InputFileChangeEventArgs e)
        {
            InputImage = e.File;
        }


    }
}
