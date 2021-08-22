using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;

namespace CountdownTimer.Model
{
    public class CountdownTimerEvent
    {
        public event EventHandler SecondCountdownEvent;

        public CountdownTimerEvent()
        {
            Date = DateTime.Today.AddDays(1);
        }

        [Required]
        public string Name { get; set; }

        [AfterToday]
        [Required]
        public DateTime Date { get; set; }

        public TimeSpan TimeLeft { get; private set; }

        public void StartCountdown()
        {
            TimeLeft = Date.Subtract(DateTime.Now);

            var timer = new Timer();
            timer.Interval = TimeSpan.FromSeconds(1).TotalMilliseconds;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimeLeft = TimeLeft.Subtract(TimeSpan.FromSeconds(1));
            SecondCountdownEvent?.Invoke(this, new EventArgs());
        }
    }
}
