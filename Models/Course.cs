using System.ComponentModel.DataAnnotations;
using Zhankui_Wang_Prob_Asst_3_Part_1.Utilities;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models;

public partial class Course
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Code must be a string of 50 characters at most.")]
    public string Code { get; set; } = null!;
    [Required]
    [StringLength(50, ErrorMessage = "Title must be a string of 50 characters at most.")]
    public string Title { get; set; } = null!;

    [Required]
    [Range(1, 9, ErrorMessage = "Section must be a single digit non-zero integer.")]
    public int Section { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Term must be a string of 50 characters at most.")]
    public string Term { get; set; } = null!;
    [Required]
    public int Year { get; set; }
    [Required]
    public int ProgramId { get; set; }
    [Required]
    public virtual Program Program { get; set; } = null!;

    // Navigation property for the many-to-many relationship with students
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public void ValidateYear()
    {
        int currentYear = DateTime.Now.Year;
        if (Year < currentYear || Year > currentYear + 4)
        {
            throw new ValidationException($"Year must be between {currentYear} and {currentYear + 4}.");
        }
    }

    //    Add a readonly property(type DateTime) StartDate on the Course, with
    //display name Start Date, for the first day of classes of the Course.Use method
    //FirstMondayOfSecondWeek, and follow these rules for the start of each term.
    //⦁ FALL: Classes start on the first Monday of the second week of September
    //⦁ WINTER: Classes start on the first Monday of the second week of January
    //⦁ SPRING: Classes start on the first Monday of the second week of May

    [Display(Name = "Start Date")]
    public DateTime StartDate
    {
        get
        {
            return Term.ToUpper() switch
            {
                "FALL" => Utility.FirstMondayOfSecondWeek(Year, 9),    // September
                "WINTER" => Utility.FirstMondayOfSecondWeek(Year, 1),  // January
                "SPRING" => Utility.FirstMondayOfSecondWeek(Year, 5),  // May
                _ => throw new InvalidOperationException("Invalid term."),
            };
        }
    }

    //    Add a readonly property(type Boolean) IsOpenToEnroll on the Course, with
    //display name Open.Property IsOpenToEnroll is assigned the value true, if 
    //the Course is accepting students and is still open for enrollment, and false
    //otherwise.To implement the business logic, follow the rules:
    //⦁ Course starts allowing enrollments, three months before the StartDate.
    //⦁ Course stops allowing enrollments, two weeks after the StartDate.
    [Display(Name = "Open")]
    public Boolean IsOpenToEnroll
    {
        get
        {
            DateTime currentDate = DateTime.Now;
            return currentDate >= StartDate.AddMonths(-3) && currentDate <= StartDate.AddDays(14);
        }
    }
}
