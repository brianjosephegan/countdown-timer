using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CountdownTimer.Model
{
    public class CountdownTimerEvent
    {
        public CountdownTimerEvent()
        {
            Date = DateTime.Today.AddDays(1);
        }

        [Required]
        public string Name { get; set; }

        [AfterToday]
        [Required]
        public DateTime Date { get; set; }
    }
}
