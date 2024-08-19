using Microsoft.AspNetCore.Mvc.Rendering;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = null!;
        public bool IsOpenToEnroll { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();
        public SelectList EligibleStudents { get; set; } = null!;
        public int SelectedStudentId { get; set; }

        public int Section { get; set; }
        public string Term { get; set; } = null!;
        public int Year { get; set; }

    }
}
