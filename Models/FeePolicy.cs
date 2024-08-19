using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models
{
    public class FeePolicy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Fee Category")]
        public string Category { get; set; } = null!;

        [Required(ErrorMessage = "Tuition Fee is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Tuition Fee must be a non-negative value.")]
        [Display(Name = "Tuition Fee")]
        public double TuitionFee { get; set; }

        [Required(ErrorMessage = "Registration Fee is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Registration Fee must be a non-negative value.")]
        [Display(Name = "Registration Fee")]
        public double RegistrationFee { get; set; }

        [Required(ErrorMessage = "Facilities Fee is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Facilities Fee must be a non-negative value.")]
        [Display(Name = "Facilities Fee")]
        public double FacilitiesFee { get; set; }

        [Required(ErrorMessage = "Union Fee is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Union Fee must be a non-negative value.")]
        [Display(Name = "Union Fee")]
        public double UnionFee { get; set; }

        [Required(ErrorMessage = "Alumni Fee is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Alumni Fee must be a non-negative value.")]
        [Display(Name = "Alumni Fee")]
        public double AlumniFee { get; set; }

        [Required(ErrorMessage = "Library Fee is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Library Fee must be a non-negative value.")]
        [Display(Name = "Library Fee")]
        public double LibraryFee { get; set; }

        [Required(ErrorMessage = "Tax Rate is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Tax Rate must be a non-negative value.")]
        [Display(Name = "Tax Rate")]
        public double TaxRate { get; set; }

        // Navigation Property
        public virtual ICollection<FinancialStatement>? FinancialStatements { get; set; } = new List<FinancialStatement>();
    }
}
