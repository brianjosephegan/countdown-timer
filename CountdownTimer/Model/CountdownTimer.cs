using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace CountdownTimer.Model
{
    public sealed class CountdownTimer : IDisposable
    {
        public event EventHandler OnTick;

        private readonly Timer timer;
        private bool disposedValue;

        public CountdownTimer()
        {
            Date = DateTime.Today.AddDays(1);

            timer = new Timer();
            timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
        }

        [AfterToday]
        [Required]
        public DateTime Date { get; set; }

        public TimeSpan TimeLeft { get; private set; }

        public void Start()
        {
            TimeLeft = Date.Subtract(DateTime.Now);
            
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    timer.Dispose();
                }

                disposedValue = true;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            OnTick?.Invoke(this, new EventArgs());
        }
    }
}
