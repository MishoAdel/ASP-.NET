using ModelBindingAssignment.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ModelBindingAssignment.CustomValidators
{
    public class InvoicePriceValidatorAttribute : ValidationAttribute
    {

        public InvoicePriceValidatorAttribute()
        {
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                PropertyInfo? prodcutsProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (prodcutsProperty != null) { 
                    List<Product> myProducts = (List<Product>)prodcutsProperty.GetValue(validationContext.ObjectInstance)!;
                    double total = 0;
                    if(myProducts != null)
                    {
                        foreach (Product product in myProducts) {
                            total += product.Price * product.Quantity;
                        }
                    }
                    if (total != (double) value)
                    {
                        return new ValidationResult("InvoicePrice doesn't match with the total cost of the specified products in the order.");
                    }
                }
                
                else
                {
                    return ValidationResult.Success;
                }


            }
            return null;
        }
    }
}
