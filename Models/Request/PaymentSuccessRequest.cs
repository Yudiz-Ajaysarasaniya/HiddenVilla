using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class PaymentSuccessRequest
    {
        public int Id { get; set; }
        [Required]
        public string? StripeSessionId { get; set; }
        public string? Status { get; set; }
        public bool IsPaymentSuccess { get; set; } = false;
    }
}
