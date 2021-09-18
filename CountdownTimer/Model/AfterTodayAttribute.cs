using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CountdownTimer.Model
{
    public class AfterTodayAttribute : ValidationAttribute
    {
        private const string errorMessage = "The Date field is required to be after today.";

        public AfterTodayAttribute()
        {
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (date.Date <= DateTime.Now.Date)
            {
                return new ValidationResult(errorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
