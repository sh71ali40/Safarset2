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

namespace Safarset2.Pages.Manage.Slider
{
    public partial class Index
    {
        public ObservableCollection<SlideDto> Slides { get; set; }
        [Inject] public ISlideService SlideService { get; set; }
        [Inject] private IMapper Mapper { get; set; }
        [Inject] public ConfirmService ConfirmService { get; set; }
        [Inject] public ToastService ToastService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var slides = SlideService.GetAllAsQueryable().OrderByDescending(s=>s.Id).AsEnumerable();
            var mappedSlides = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideDto>>(slides);
            Slides = new ObservableCollection<SlideDto>(mappedSlides);
        }

        private async Task OnRemoveClick(GridCommandEventArgs e)
        {
            var slide = (SlideDto)e.Item;
            ConfirmService.Show("حذف", "آیا از حذف مطمئن هستید؟", () => RemoveSlide(slide), null);
        }

        private async void RemoveSlide(SlideDto slideDto)
        {
            var result = await base.Delete(slideDto.Id);


            if (result)
            {
                Slides.Remove(slideDto);
                ToastService.ShowToast("حذف با موفقیت انجام شد", ToastLevel.Success);
            }
            else
            {
                ToastService.ShowToast("حذف ناموفق بود", ToastLevel.Error);
            }
        }
    }
}
