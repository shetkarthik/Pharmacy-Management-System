using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Drug.Models
{
    public class BillingTable
    {
        [Key]
        public int BillingId { get; set; }

        public string DrugIds { get; set; }

        public string Quantities { get; set; }

        public double TotalCost { get; set; }

       
    }
}
