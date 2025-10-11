using ModelBindingAssignment.Models;
using System.ComponentModel.DataAnnotations;

namespace ModelBindingAssignment.CustomValidators
{
    public class ProductsValidatorAttribute : ValidationAttribute
    {
        public ProductsValidatorAttribute() { }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                List<Product> products = (List<Product>)value;

                if (products.Count == 0)
                {
                    return new ValidationResult("Order should contain at least on product");
                }
                return ValidationResult.Success;

            }
            return null;
        }
    }
}
