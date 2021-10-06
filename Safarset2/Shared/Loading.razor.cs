using System;
using Microsoft.AspNetCore.Components;
using Safarset.Service.Component;

namespace Safarset2.Shared
{
    public partial class Loading : ComponentBase, IDisposable
    {
        [Inject] LoadingService LoadingService { get; set; }

        protected string Message { get; set; }
        protected bool IsVisible { get; set; }

        protected override void OnInitialized()
        {
            LoadingService.OnShow += ShowLoading;
            LoadingService.OnHide += HideLoaing;
        }

        private void ShowLoading(string message)
        {
            SetMessage(message);
            IsVisible = true;
            InvokeAsync(() => this.StateHasChanged());
        }

        private void SetMessage(string message)
        {
            Message = message;
        }
        private void HideLoaing()
        {
            IsVisible = false;
            InvokeAsync(() => this.StateHasChanged());
        }
        public void Dispose()
        {
            LoadingService.OnShow -= ShowLoading;
        }
    }
}
