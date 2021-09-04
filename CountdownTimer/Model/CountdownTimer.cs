using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;

namespace CountdownTimer.Model
{
    public class CountdownTimer
    {
        public event EventHandler OnTick;

        public CountdownTimer()
        {
            Date = DateTime.Today.AddDays(1);
        }

        [AfterToday]
        [Required]
        public DateTime Date { get; set; }

        public TimeSpan TimeLeft { get; private set; }

        public void StartCountdown()
        {
            TimeLeft = Date.Subtract(DateTime.Now);
            CreateAndStartTimer();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            OnTick?.Invoke(this, new EventArgs());
        }

        private void CreateAndStartTimer()
        {
            var timer = new Timer();
            timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }
    }
}
