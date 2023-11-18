using System.ComponentModel.DataAnnotations;

namespace Drug.Models
{
    public class DrugInfo
    {
        [Key]
        public int DrugId { get; set; }

        public string? DrugName { get; set; }

        public string? DrugGenericName { get; set; }

        public int Quantity { get; set; }

        public string? DrugImage { get; set; }

        public float? Price { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string? DosageForm { get;set; }


    }
}
