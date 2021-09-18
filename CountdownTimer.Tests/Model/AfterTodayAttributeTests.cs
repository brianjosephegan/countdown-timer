using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountdownTimer.Model;
using Xunit;

namespace CountdownTimer.Tests.Model
{
    public class AfterTodayAttributeTests
    {
        private readonly AfterTodayAttribute attribute = new AfterTodayAttribute();

        [Fact]
        public void Class_Should_DeriveFromValidationAttribute()
        {
            Assert.IsAssignableFrom<ValidationAttribute>(attribute);
        }

        [Fact]
        public void ErrorMessage_Should_MatchExpected()
        {
            Assert.Equal("The Date field is required to be after today.", attribute.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(DatesAfterToday))]
        public void GetValidationResult_Should_BeSuccessfulForDatesAfterToday(DateTime dateTime)
        {
            var validationResult = attribute.GetValidationResult(dateTime, new ValidationContext(this));

            Assert.Equal(ValidationResult.Success, validationResult);
        }

        [Theory]
        [MemberData(nameof(DatesOnOrBeforeToday))]
        public void GetValidationResult_Should_BeFailedForDatesOnOrBeforeToday(DateTime dateTime)
        {
            var validationResult = attribute.GetValidationResult(dateTime, new ValidationContext(this));

            Assert.Equal(attribute.ErrorMessage, validationResult.ErrorMessage);
        }

        public static IEnumerable<object[]> DatesAfterToday
        {
            get
            {
                for (int i = 1; i <= 10; i++)
                {
                    yield return new object[] { DateTime.Today.AddDays(i) };
                }
            }
        }

        public static IEnumerable<object[]> DatesOnOrBeforeToday
        {
            get
            {
                for (int i = 0; i < 10; i++)
                {
                    yield return new object[] { DateTime.Today.AddDays(-i) };
                }
            }
        }
    }
}
