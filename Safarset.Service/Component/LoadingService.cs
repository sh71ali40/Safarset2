using System;
using System.Timers;

namespace Safarset.Service.Component
{
    public class LoadingService : IDisposable
    {
        public event Action<string> OnShow;
        public event Action OnHide;
        private Timer Countdown;
        private int Time = 5000;
        public void Show(int? time=null,string message="")
        {
            OnShow?.Invoke(message);
            if (time.HasValue)
            {
                Time = time.Value;
                StartCountdown();
            }

        }

        private void StartCountdown()
        {
            SetCountdown();

            if (Countdown.Enabled)
            {
                Countdown.Stop();
                Countdown.Start();
            }
            else
            {
                Countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (Countdown == null)
            {
                Countdown = new Timer(Time);
                Countdown.Elapsed += Hide;
                Countdown.AutoReset = false;
            }
        }

        private void Hide(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            OnHide?.Invoke();
        }

        public void Hide( )
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            Countdown?.Dispose();
        }
    }
}
