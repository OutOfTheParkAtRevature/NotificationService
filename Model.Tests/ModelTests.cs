using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Model;

namespace Model.Tests
{
    public class ModelTests
    {
        /// <summary>
        /// Checks the data annotations of Models to make sure they aren't being violated
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            Validator.TryValidateObject(model, validationContext, result, true);
            // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }

        /// <summary>
        /// Validates the Notification Model works with proper data
        /// </summary>
        [Fact]
        public void ValidateNotification()
        {
            var notification = new Notification
            {
                NotificationID= new Guid(),
                Service="Message",
                TransactionType="Post"
            };

            var errorcount = ValidateModel(notification).Count;
            Assert.Equal(0, errorcount);
        }
    }
}
