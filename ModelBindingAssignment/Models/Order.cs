using ModelBindingAssignment.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace ModelBindingAssignment.Models
{
    public class Order
    {
        [Display(Name = "Order Number")]
        public int? OrderNo { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Order Date")]
        [OrderDateValidatorAttribute("2000-01-01")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Invoice Price")]
        [InvoicePriceValidator]
        public double InvoicePrice { get; set; }

        [ProductsValidator]
        public List<Product> Products { get; set; }

        public Order() {
        }

    }
}
