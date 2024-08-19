using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models
{
    public class FinancialStatement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Last Changed")]
        public DateTime LastChanged { get; set; }

        // Navigation property for one-to-many relationship with StatementEntry
        public virtual ICollection<StatementEntry> StatementEntries { get; set; } = new List<StatementEntry>();

        // Navigation property for one-to-one relationship with Student
        [Required]
        public int StudentID { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; } = null!;
        public virtual FeePolicy FeePolicy { get; set; } = null!;

    }
}
