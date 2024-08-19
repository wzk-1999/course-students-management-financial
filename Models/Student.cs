using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(786280, int.MaxValue)]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = null!;


        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Residential Address")]
        [StringLength(100)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Postal Code is required.")]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"^[ ]*[A-Za-z]\d[A-Za-z][ -]*\d[A-Za-z]\d[ ]*$",
         ErrorMessage = "Invalid Postal Code format. Expected format is A0A0A0.")]
        //[StringLength(6)]
        public string PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([a-z0-9A-Z]+[-_\.]?)+[a-z0-9A-Z]@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?\.)+[a-zA-Z]{2,}$",
        ErrorMessage = "Email format invalid.")]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Type is required.")]


        [StringLength(20)]
        public string Type { get; set; } = null!;

        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Student Status")]
        public StudentStatus Status { get; set; } = StudentStatus.ELIGIBLE;

        // Navigation properties

        // A student must be accepted into exactly one program.
        [Required]
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId")]
        public virtual Program Program { get; set; } = null!;

        // A student can be enrolled in none or multiple courses.
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        // A student must be living in exactly one city.
        [Required]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; } = null!;

        [Display(Name = "Student Name")]
        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }

        [Display(Name = "Course Load")]
        public int CourseLoad
        {
            get
            {
                return Courses.Count;
            }
        }

        [Display(Name = "Full Time")]
        public bool IsFullTime
        {
            get
            {
                return Courses.Count >= 3;
            }
        }

        [Display(Name = "Student Address")]
        public string FullAddress
        {
            get
            {
                if (City == null)
                {
                    return $"{Address}, Unknown City, Unknown Province, {PostalCode}";
                }
                return $"{Address}, {City.Name}, {City.ProvinceId}, {PostalCode}";
            }
        }

        // Navigation property for one-to-one relationship with FinancialStatement
        public virtual FinancialStatement FinancialStatement { get; set; }



        [Display(Name = "Total Amount Owed")]

        
        public double Balance
        {
            get
            {
                if (FinancialStatement != null)
                {
                    // Get the FeePolicy associated with the FinancialStatement
                    var feePolicy = FinancialStatement.FeePolicy;

                    // Sum up all the StatementEntry values
                    double totalValue = FinancialStatement.StatementEntries.Where(e=>e.Description!= "Tax Rate").Sum(entry => entry.Value);

                    //// Calculate the sum of the six specific fees
                    //double feeSum = feePolicy.TuitionFee + feePolicy.RegistrationFee +
                    //                feePolicy.FacilitiesFee + feePolicy.UnionFee +
                    //                feePolicy.AlumniFee + feePolicy.LibraryFee;

                    // Calculate the tax
                    double taxAmount = (totalValue * feePolicy.TaxRate) / 100;

                    // Add the tax amount to the total value
                    return totalValue + taxAmount;
                }

                return 0;
            }
        }
    }
}
