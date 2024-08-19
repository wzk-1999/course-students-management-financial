using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models
{
    public class StatementEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be a non-negative number.")]
        public double Value { get; set; }

        // Navigation property for many-to-one relationship with FinancialStatement
        [Required]
        public int FinancialStatementID { get; set; }
        [ForeignKey("FinancialStatementID")]
        public virtual FinancialStatement FinancialStatement { get; set; } = null!;
    }
}
