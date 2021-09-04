using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace CountdownTimer.Model
{
    public class CountdownTimer
    {
        public event EventHandler OnTick;

        private readonly Timer timer; 

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

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            OnTick?.Invoke(this, new EventArgs());
        }
    }
}
