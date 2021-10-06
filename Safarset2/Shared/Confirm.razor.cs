using System;
using Microsoft.AspNetCore.Components;
using Safarset.Service.Component;

namespace Safarset2.Shared
{
    public partial class Confirm
    {
        private string Title { get; set; }
        private string Message { get; set; }
        private Action OnYesClick { get; set; }
        private Action OnNoClick { get; set; }

        [Inject] public ConfirmService ConfirmService { get; set; }
        protected bool IsVisible { get; set; }
        protected override void OnInitialized()
        {
            ConfirmService.OnShow += ShowConfirm;
            
        }

        private void ShowConfirm(string title,string message,Action onYesClick,Action onNoClick)
        {
            IsVisible = true;
            Title = title;
            Message = message;
            OnYesClick = onYesClick;
            OnNoClick = onNoClick;
            this.InvokeAsync(() => this.StateHasChanged());
        }

        private void OnConfirmationChange(bool isOk)
        {
            if (isOk)
            {
                OnYesClick?.Invoke();
            }
            else
            {
                OnNoClick?.Invoke();
            }

            IsVisible = false;
            this.InvokeAsync(() => this.StateHasChanged());
        }
    }
}
