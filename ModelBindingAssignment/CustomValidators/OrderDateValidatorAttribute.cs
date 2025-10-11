using System.ComponentModel.DataAnnotations;

namespace ModelBindingAssignment.CustomValidators
{
    public class OrderDateValidatorAttribute : ValidationAttribute
    {
        public DateTime MinDate { get; set; }
        public OrderDateValidatorAttribute(string dateTime) {
            MinDate = Convert.ToDateTime(dateTime); 
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime orderDate = (DateTime)value;

                if (orderDate < MinDate)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? "Order date should be greater than or equal to {0}", MinDate.ToString("yyyy-MM-dd")), new string[] { nameof(validationContext.MemberName) });
                }

                //No validation error
                return ValidationResult.Success;
            }
            return null;
        }
    }
}
